using System;
using System.Collections.Generic;

namespace FS.Infrastructure
{
    public class Account : Entity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public IEnumerable<Expense> Expenses { get; set; }
    }
}