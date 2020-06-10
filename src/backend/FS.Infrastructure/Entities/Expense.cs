using System;

namespace FS.Infrastructure
{
    public class Expense : Entity
    {        
        public Guid AccountId { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }

        public virtual Account Account { get; set; }
    }
}