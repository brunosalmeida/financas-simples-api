namespace FS.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FS.Domain.Core.Interfaces;
    using FS.Domain.Model;
    using System.Data.SqlClient;
    using System.Text;
    using Dapper;
    using Mappings;
    using Microsoft.Extensions.Configuration;

    public class MovimentRepository : IMovimentRepository
    {
        private const string table = "dbo.Moviments";

        private readonly IConfiguration _configuration;

        public MovimentRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Moviment> Get(Guid id)
        {
            var sql = new StringBuilder();
            sql.Append($"SELECT Id, Value, Description, Category, [Type], AccountId, UserId, CreatedOn, UpdatedOn");
            sql.Append($" FROM {table} WHERE ID = @id");

            await using var connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection"));
            connection.Open();
            
            var dictionary = new Dictionary<string, object>
            {
                {"@id", id}
            };

            var parameters = new DynamicParameters(dictionary);
            
            var moviment = await connection.QueryFirstAsync<FS.Data.Entities.Moviment>(sql.ToString(), parameters);

            return MovimentEntityToMovimenteDomainMapper.MapFrom(moviment);
        }

        public async Task<IEnumerable<Moviment>> GetMovimentsByAccount(Guid userId, Guid accountId, int page, int size)
        {
            var sql = new StringBuilder();
            sql.Append($"SELECT Id, Value, Description, Category, [Type], AccountId, UserId, CreatedOn, UpdatedOn");
            sql.Append($" FROM {table} WHERE AccountId = @accountId AND UserId = @userId");
            sql.Append($" ORDER BY CreatedOn DESC");
            sql.Append($" OFFSET @offset ROWS FETCH NEXT @next ROWS ONLY");

            await using var connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection"));
            connection.Open();
            
            var dictionary = new Dictionary<string, object>
            {
                {"@userId", userId},
                {"@accountId", accountId},
                {"@offset", (page - 1) * size},
                {"@next", size}
            };

            var parameters = new DynamicParameters(dictionary);
            
            var moviment = await connection.QueryAsync<FS.Data.Entities.Moviment>(sql.ToString(), parameters);

            return MovimentEntityToMovimenteDomainMapper.MapFrom(moviment);
        }

        public async Task Insert(Moviment entity)
        {
            var sql = new StringBuilder();
            sql.Append($"INSERT INTO {table}");
            sql.Append(" (Id, Value, Description, Category, [Type], AccountId, UserId, CreatedOn)");
            sql.Append(" VALUES(@id, @value, @description, @category, @type, @accountId, @userId, @createdOn)");

            await using var connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection"));
            connection.Open();

            var dictionary = new Dictionary<string, object>
            {
                {"@id", entity.Id}, 
                {"@value", entity.Value}, 
                {"@description", entity.Description},
                {"@category", entity.Category},
                {"@type", entity.Type},
                {"@accountId", entity.AccountId},
                {"@userId", entity.UserId},
                {"@createdOn", entity.CreatedOn},
            };

            var parameters = new DynamicParameters(dictionary);
            
            await connection.ExecuteScalarAsync(sql.ToString(), parameters);

            await Task.CompletedTask;
        }

        public async Task Update(Guid id, Moviment model)
        {
            var sql = new StringBuilder();
            sql.Append($"UPDATE {table}");
            sql.Append(" SET Value = @value, Description = @description, Category = @category, [Type] = @type, UpdatedOn = @updatedOn");
            sql.Append(" WHERE Id = @id and AccountId = @accountId AND UserId = @userId");

            await using var connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection"));
            connection.Open();

            var dictionary = new Dictionary<string, object>
            {
                {"@id", model.Id}, 
                {"@value", model.Value}, 
                {"@description", model.Description},
                {"@category", model.Category},
                {"@type", model.Type},
                {"@accountId", model.AccountId},
                {"@userId", model.UserId},
                {"@updatedOn", model.UpdatedOn},
            };

            var parameters = new DynamicParameters(dictionary);
            
            await connection.ExecuteScalarAsync(sql.ToString(), parameters);

            await Task.CompletedTask;
        }

        public async Task Delete(Guid id)
        {
            var sql = $"DELETE FROM {table} WHERE ID = @id";

            await using var connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection"));
            connection.Open();

            var dictionary = new Dictionary<string, object> {{"@id", id}};

            var parameters = new DynamicParameters(dictionary);

            await connection.ExecuteScalarAsync(sql, parameters);

            await Task.CompletedTask;
        }
    }
}