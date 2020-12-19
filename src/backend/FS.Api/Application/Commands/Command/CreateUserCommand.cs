namespace FS.Api.Application.Commands.Command
{
    using System;
    using DataObject.User;
    using MediatR;
    using Utils.Enums;

    public class CreateUserCommand : IRequest<UserAccount>
    {
        public CreateUserCommand(string name, string email, string password, EGender gender, DateTime birthDate)
        {
            Name = name;
            Email = email;
            Password = password;
            Gender = gender;
            BirthDate = birthDate;
        }

        public string Name { get; private set; }
        public EGender Gender { get; private set; }
        
        public DateTime BirthDate { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
    }
}