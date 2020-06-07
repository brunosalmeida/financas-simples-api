
using System;
using System.Collections.Generic;
using System.Text;

namespace FS.Infrastructure
{
    public class User : Entity
    {
        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }
    }
}
