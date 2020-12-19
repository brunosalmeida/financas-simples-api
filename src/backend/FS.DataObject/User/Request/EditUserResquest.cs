namespace FS.DataObject.User.Request
{
    using System;
    using Utils.Enums;

    public class EditUserResquest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        
        public DateTime BirthDate { get; set; }
        public EGender Gender { get; set; }
    }
}