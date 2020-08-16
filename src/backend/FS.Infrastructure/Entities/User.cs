namespace FS.Data.Entities
{
    public class User : Entity
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public Account Account { get; set; }
    }
}