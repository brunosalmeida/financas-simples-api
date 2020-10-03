using System;

namespace FS.DataObject.User.Request
{
    public class ChangePassword
    {
        public Guid Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}