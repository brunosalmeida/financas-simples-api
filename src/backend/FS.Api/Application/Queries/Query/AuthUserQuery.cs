namespace FS.Api.Application.Queries.Query
{
    using MediatR;

    public class AuthUserQuery : IRequest<string>
    {
        public AuthUserQuery(string username, string password)
        {
            Username = username;
            Password = password;
        }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}