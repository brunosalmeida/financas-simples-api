namespace FS.DataObject.User.Request
{
    using Utils.Enums;

    public class CreateUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        
        public EGender Gender { get; set; }
        public string Password { get; set; }
    }
}