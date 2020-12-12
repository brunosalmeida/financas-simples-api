namespace FS.Api.Application.Commands.Command
{
    using System;
    using MediatR;
    using Utils.Enums;

    public class EditUserCommand : IRequest<Guid>
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        
        public EGender Gender { get; private set; }

        public EditUserCommand(Guid id, string name, string email, EGender gender)
        {
            Id = id;
            Name = name;
            Email = email;
            Gender = gender;
        }
    }
}