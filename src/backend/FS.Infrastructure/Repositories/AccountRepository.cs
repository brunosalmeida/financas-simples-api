namespace FS.Data.Repositories
{
    using System.Data.SqlClient;
    using Dapper;
    using System;
    using System.Collections.Generic;
    using System.Security.AccessControl;
    using System.Text;
    using System.Threading.Tasks;
    using FS.Data.Mappings;
    using FS.Domain.Core.Interfaces;
    using FS.Domain.Model;
    using Microsoft.AspNetCore.Mvc.ApplicationModels;
    using Microsoft.Extensions.Configuration;

    public class AccountRepository : IAccountRepository
    {
        private const string table = "dbo.Accounts";

        private readonly IConfiguration _configuration;

        public AccountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Account> Get(Guid id)
        {
            var sql = $"SELECT * FROM {table} WHERE ID = @id";

            await using var connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection"));
            connection.Open();
            var entity = await connection.QueryFirstAsync(sql, new {id});

            return AccountEntityToAccountDomainMapper.MapFrom(entity);
        }

        public async Task Insert(Account entity)
        {
            var sql = new StringBuilder();
            sql.Append($"INSERT INTO {table}");
            sql.Append(" (Id, UserId, CreatedOn)");
            sql.Append(" VALUES(@id, @userId, @createdOn)");

            await using var connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection"));
            connection.Open();

            var dictionary = new Dictionary<string, object>
            {
                {"@id", entity.Id}, 
                {"@userId", entity.User.Id}, 
                {"@createdOn", entity.CreatedOn}
            };

            var parameters = new DynamicParameters(dictionary);
            
            await connection.ExecuteScalarAsync(sql.ToString(), parameters);

            await Task.CompletedTask;
        }

        public Task Update(Guid id, Account model)
        {
            throw new NotImplementedException("Method available");
        }

        public async Task Delete(Guid id)
        {
            throw new NotImplementedException("Method available");
        }

        public async Task<Account> GetAccountByUserId(Guid userId)
        {
            throw new NotImplementedException("Method available");
        }
    }
}