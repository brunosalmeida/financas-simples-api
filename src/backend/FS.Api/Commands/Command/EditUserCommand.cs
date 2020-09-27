using System;
using MediatR;

namespace FS.Api.Commands.Command
{
    public class EditUserCommand : IRequest<Guid>
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }

        public EditUserCommand(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
    }
}