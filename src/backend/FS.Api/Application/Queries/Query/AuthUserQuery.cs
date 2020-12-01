namespace FS.Api.Application.Queries.Query
{
    using DataObject.Authentication;
    using MediatR;

    public class AuthUserQuery : IRequest<AuthenticationResponse>
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