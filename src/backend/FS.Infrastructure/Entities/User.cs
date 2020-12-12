namespace FS.Data.Entities
{
    using Utils.Enums;

    public class User : Entity
    {
        public string Name { get; set; }

        public EGender Gender { get; set; }
        
        public string Email { get; set; }

        public string Password { get; set; }

        public Account Account { get; set; }
    }
}