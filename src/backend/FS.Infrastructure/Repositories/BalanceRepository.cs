namespace FS.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Threading.Tasks;
    using Dapper;
    using Domain.Core.Interfaces;
    using Entities;
    using Mappings;
    using Microsoft.Extensions.Configuration;

    public class BalanceRepository : IBalanceRepository 
    {
        private const string table = "dbo.Balance";

        private readonly IConfiguration _configuration;

        public BalanceRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<FS.Domain.Model.Balance> Get(Guid userId, Guid accountId)
        {
            var sql = $"SELECT Id, UserId, AccountId, Value, CreatedOn, UpdatedOn FROM {table} WHERE UserId = @userId AND AccountId = @accountId";
            
            await using var connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection"));
            connection.Open();

            var dictionary = new Dictionary<string, object>
            {
                {"@userId", userId},
                {"@accountId", accountId}
            };

            var parameters = new DynamicParameters(dictionary);
            
            var balance = await connection.QueryFirstOrDefaultAsync<Balance>(sql, parameters);

            return BalanceEntityToBalanceDomainMapper.MapFrom(balance);
        }

        public async Task Update(Guid id, FS.Domain.Model.Balance model)
        {
            var sql = $"UPDATE {table} SET Value = @value, UpdatedOn = @updatedOn WHERE ID = @id";
            
            await using var connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection"));
            connection.Open();

            var dictionary = new Dictionary<string, object>
            {
                {"@id", id},
                {"@value", model.Value},
                {"@updatedOn", model.UpdatedOn},
            };

            var parameters = new DynamicParameters(dictionary);
            
            await connection.ExecuteScalarAsync<Balance>(sql, parameters);

            await Task.CompletedTask;
        }

        public async Task Insert(FS.Domain.Model.Balance model)
        {
            var sql = $"INSERT INTO {table} (Id, AccountId, UserId, Value, CreatedOn)" +
                      $" VALUES (@id, @accountId, @userId, @value, @createdOn)";
            
            await using var connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection"));
            connection.Open();

            var dictionary = new Dictionary<string, object>
            {
                {"@id", model.Id},
                {"@accountId", model.AccountId},
                {"@userId", model.UserId},
                {"@value", model.Value},
                {"@createdOn", model.CreatedOn},
            };

            var parameters = new DynamicParameters(dictionary);
            
            await connection.ExecuteScalarAsync<Balance>(sql, parameters);

            await Task.CompletedTask;
        }
    }
}