namespace FS.Api.Application.Commands.Command
{
    using System;
    using MediatR;

    public class CreateUserCommand : IRequest<Guid?>
    {
        public CreateUserCommand(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
    }
}