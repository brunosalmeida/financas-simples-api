using System;
using System.Collections.Generic;

namespace FS.Data.Entities
{
    public class Account : Entity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public IEnumerable<Movement> Movements { get; set; }
    }
}