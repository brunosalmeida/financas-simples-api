namespace FS.DataObject.User.Request
{
    using Utils.Enums;

    public class EditUserResquest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        
        public EGender Gender { get; set; }
    }
}