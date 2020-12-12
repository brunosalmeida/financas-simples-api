namespace FS.Api.Application.Commands.Command
{
    using DataObject.User;
    using MediatR;
    using Utils.Enums;

    public class CreateUserCommand : IRequest<UserAccount>
    {
        public CreateUserCommand(string name, string email, string password, EGender gender)
        {
            Name = name;
            Email = email;
            Password = password;
            Gender = gender;
        }

        public string Name { get; private set; }
        public EGender Gender { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
    }
}