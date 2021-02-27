using System;

namespace FS.DataObject.User.Responses
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Utils.Enums;

    public class GetUserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

       
        public EGender Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public GetUserResponse(Guid id, string name, string email, string password, EGender gender, DateTime birthDate)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            BirthDate = birthDate;
            Gender = gender;
        }
    }
}