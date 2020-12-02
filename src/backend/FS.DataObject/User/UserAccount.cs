namespace FS.DataObject.User
{
    using System;

    public class UserAccount
    {
        public Guid User { get; private set; }
        public Guid Account { get; private set; }

        public UserAccount(Guid user, Guid account)
        {
            User = user;
            Account = account;
        }
    }
}