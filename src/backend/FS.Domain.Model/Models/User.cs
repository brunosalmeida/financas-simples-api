using System;

namespace FS.Domain.Model
{
    using Utils.Enums;

    public class User : Base
    {
        public User(Guid id, EGender gender) : base(id)
        {
            Gender = gender;
        }

        public User(Guid id, string name, string email, EGender gender) : base(id)
        {
            Name = name;
            Email = email;
            Gender = gender;
        }

        public User(string name, string email, string password, EGender gender)
            : base()
        {
            Name = name;
            Email = email;
            Password = password;
            Gender = gender;
        }

        public User(Guid id, string name, string email, string password, EGender gender, DateTime createdOn,
            DateTime? updatedOn)
            : base(id, createdOn, updatedOn)
        {
            Name = name;
            Email = email;
            Password = password;
            Gender = gender;
        }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public EGender Gender { get; private set; }

        public void SetName(string name)
        {
            this.Name = name;
            this.SetUpdateDate();
        }

        public void SetEmail(string email)
        {
            this.Email = email;
            this.SetUpdateDate();
        }

        public void SetPassword(string password)
        {
            this.Password = password;
            this.SetUpdateDate();
        }

        public void SetGender(EGender gender)
        {
            this.Gender = gender;
            this.SetUpdateDate();
        }
    }
}