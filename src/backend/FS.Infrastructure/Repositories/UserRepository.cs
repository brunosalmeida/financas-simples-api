namespace FS.Data.Repositories
{
    using FS.Domain.Core.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using Dapper;
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
            throw new NotImplementedException("Method available");
        }

        public async Task<Guid> GetUserByUsernameAndPassword(string email, string password)
        {
            throw new NotImplementedException("Method available");
        }

        public async Task<Domain.Model.User> Get(Guid id)
        {
            throw new NotImplementedException("Method available");
        }

        public async Task<IEnumerable<Domain.Model.User>> GetAll()
        {
            throw new NotImplementedException("Method available");
        }

        public async Task Insert(Domain.Model.User entity)
        {
            var sql = new StringBuilder();
            sql.Append($"INSERT INTO {table}");
            sql.Append(" (Id, Name, Gender, Email, Password, CreatedOn)");
            sql.Append(" VALUES(@id, @name, @gender, @email, @password, @createdOn)");

            await using var connection = new SqlConnection(_configuration.GetConnectionString("DatabaseConnection"));
            connection.Open();

            var dictionary = new Dictionary<string, object>
            {
                {"@id", entity.Id}, 
                {"@name", entity.Name}, 
                {"@gender", entity.Gender},
                {"@email", entity.Email},
                {"@password", entity.Password},
                {"@createdOn", entity.CreatedOn}
            };

            var parameters = new DynamicParameters(dictionary);
            
            await connection.ExecuteScalarAsync(sql.ToString(), parameters);

            await Task.CompletedTask;
        }

        public async Task Update(Guid id, Domain.Model.User model)
        {
            throw new NotImplementedException("Method available");
        }
    }
   
}