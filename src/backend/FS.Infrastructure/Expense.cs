using System;

namespace FS.Infrastructure
{
    public class Expense : Entity
    {        
        public Guid UserId { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }

        public virtual User User { get; set; }
    }
}