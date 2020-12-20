namespace FS.Data.Repositories
{
    using FS.Domain.Core.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Data.SqlClient;
    using System.Text;
    using Dapper;
    using Entities;
    using Mappings;
    using Microsoft.Extensions.Configuration;

    public class UserRepository : IUserRepository
    {
        private const string table = "dbo.Users";

        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
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

        public async Task<Guid> GetUserByUsernameAndPassword(string email, string password)
        {
            var sql = new StringBuilder();
            sql.Append($"SELECT Id FROM {table}");
            sql.Append(" WHERE Email = @email AND Password = @password");
            
            await using var connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection"));
            connection.Open();

            var dictionary = new Dictionary<string, object>
            {
                {"@email", email},
                {"@password", password},
            };

            var parameters = new DynamicParameters(dictionary);
            
            return await connection.QueryFirstAsync<Guid>(sql.ToString(), parameters);
        }

        public async Task<Domain.Model.User> Get(Guid id)
        {
            var sql = $"SELECT Id, Name, Gender, Email, Password, CreatedOn, UpdatedOn FROM {table} WHERE ID = @id";
            
            await using var connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection"));
            connection.Open();

            var dictionary = new Dictionary<string, object> {{"@id", id}};

            var parameters = new DynamicParameters(dictionary);
            
            var user = await connection.QueryFirstAsync<User>(sql, parameters);

            return UserEntityToUserDomainMapper.MapFrom(user);
        }

        public async Task<IEnumerable<Domain.Model.User>> GetAll()
        {
            var sql = $"SELECT Id, Name, Gender, Email, Password, CreatedOn, UpdatedOn FROM {table}";

            await using var connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection"));
            connection.Open();

            var users = await connection.QueryAsync<User>(sql);

            return UserEntityToUserDomainMapper.MapFrom(users);
        }

        public async Task Insert(Domain.Model.User model)
        {
            var sql = new StringBuilder();
            sql.Append($"INSERT INTO {table}");
            sql.Append(" (Id, Name, Gender,  BirthDate, Email, Password, CreatedOn)");
            sql.Append(" VALUES(@id, @name, @gender, @birthDate, @email, @password, @createdOn)");

            await using var connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection"));
            connection.Open();

            var dictionary = new Dictionary<string, object>
            {
                {"@id", model.Id},
                {"@name", model.Name},
                {"@gender", model.Gender},
                {"@birthDate", model.BirthDate},
                {"@email", model.Email},
                {"@password", model.Password},
                {"@createdOn", model.CreatedOn}
            };

            var parameters = new DynamicParameters(dictionary);

            await connection.ExecuteScalarAsync(sql.ToString(), parameters);

            await Task.CompletedTask;
        }

        public async Task Update(Guid id, Domain.Model.User model)
        {
            var sql = new StringBuilder();
            sql.Append($"UPDATED {table}");
            sql.Append(" SET Name = @name, Gender = @gender, BirthDate = @birthDate, Email = @email, Password = @password, UpdateOn = UpdateOn");
            sql.Append(" WHERE Id = @id");

            await using var connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection"));
            connection.Open();

            var dictionary = new Dictionary<string, object>
            {
                {"@id", model.Id},
                {"@name", model.Name},
                {"@gender", model.Gender},
                {"@birthDate", model.BirthDate},
                {"@email", model.Email},
                {"@password", model.Password},
                {"@updateOn", model.UpdatedOn}
            };

            var parameters = new DynamicParameters(dictionary);

            await connection.ExecuteScalarAsync(sql.ToString(), parameters);

            await Task.CompletedTask;
        }
    }
}