using System;

namespace FS.Domain.Model
{
    using Utils.Enums;

    public class User : Base
    {
        public User(Guid id, EGender gender, DateTime birthDate) : base(id)
        {
            Gender = gender;
            BirthDate = birthDate;
        }

        public User(Guid id, string name, string email, EGender gender, DateTime birthDate) : base(id)
        {
            Name = name;
            Email = email;
            Gender = gender;
            BirthDate = birthDate;
        }

        public User(string name, string email, string password, EGender gender, DateTime birthDate)
            : base()
        {
            Name = name;
            Email = email;
            Password = password;
            Gender = gender;
            BirthDate = birthDate;
        }

        public User(Guid id, string name, string email, string password, EGender gender, DateTime birthDate,
            DateTime createdOn, DateTime? updatedOn)
            : base(id, createdOn, updatedOn)
        {
            Name = name;
            Email = email;
            Password = password;
            Gender = gender;
            BirthDate = birthDate;
        }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public EGender Gender { get; private set; }

        public DateTime BirthDate { get; private set; }

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