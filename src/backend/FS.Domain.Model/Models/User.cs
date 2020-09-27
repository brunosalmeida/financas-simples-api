using System;

namespace FS.Domain.Model
{
    public class User : Base
    {
        public User(Guid id, string name, string email):base(id)
        {
           
            Name = name;
            Email = email;  
        }
        
        public User(string name, string email, string password)
            : base()
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public User(Guid id, string name, string email, string password, DateTime createdOn, DateTime? updatedOn)
            : base(id, createdOn, updatedOn)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

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
    }
}