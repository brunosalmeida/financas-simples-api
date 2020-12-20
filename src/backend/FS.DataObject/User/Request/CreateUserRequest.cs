namespace FS.DataObject.User.Request
{
    using Newtonsoft.Json.Converters;
    using System;
    using System.Text.Json.Serialization;
    using Utils.Enums;

    public class CreateUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public EGender Gender { get; set; }
        public string Password { get; set; }
    }
}