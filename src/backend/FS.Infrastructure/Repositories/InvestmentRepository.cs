// unset

namespace FS.Data.Repositories
{
    using Dapper;
    using Domain.Core.Interfaces;
    using Domain.Model;
    using Mappings;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Text;
    using System.Threading.Tasks;

    public class InvestmentRepository : IInvestmentRepository
    {
        private string table = "InvestmentMoviments";

        private readonly IConfiguration _configuration;

        public InvestmentRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public Task<Investment> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task Insert(Investment entity)
        {
            var sql = new StringBuilder();
            sql.Append($"INSERT INTO {table}");
            sql.Append(" (Id, Value, Description, [Type], AccountId, UserId, CreatedOn)");
            sql.Append(" VALUES(@id, @value, @description, @type, @accountId, @userId, @createdOn)");

            await using var connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection"));
            connection.Open();

            var dictionary = new Dictionary<string, object>
            {
                {"@id", entity.Id}, 
                {"@value", entity.Value}, 
                {"@description", entity.Description},
                {"@type", entity.Type},
                {"@accountId", entity.AccountId},
                {"@userId", entity.UserId},
                {"@createdOn", entity.CreatedOn},
            };

            var parameters = new DynamicParameters(dictionary);
            
            await connection.ExecuteScalarAsync(sql.ToString(), parameters);

            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Investment>> GetInvestmentsByAccount(Guid userId, Guid accountId, int page, int size)
        {
            var sql = new StringBuilder();
            sql.Append($"SELECT Id, Value, Description, [Type], AccountId, UserId, CreatedOn, UpdatedOn");
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
            
            var investment = await connection.QueryAsync<FS.Data.Entities.Investment>(sql.ToString(), parameters);

            return InvestmentEntityToInvestmentDomainMapper.MapFrom(investment);
        }
    }
}