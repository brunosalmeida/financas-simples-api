namespace FS.Api.Application.Commands.Command
{
    using System;
    using DataObject.User;
    using MediatR;

    public class CreateUserCommand : IRequest<UserAccount>
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