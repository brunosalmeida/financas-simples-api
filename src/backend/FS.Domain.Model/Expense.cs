using System;

namespace FS.Domain.Model
{
    public class Expense : Base
    {

        public Expense(decimal value, string description) : base()
        {
            Value = value;
            Description = description;
        }
        public Expense(Guid id, decimal value, string description, DateTime createdOn, DateTime? updatedOn)
            : base(id,createdOn, updatedOn)
        {
            Value = value;
            Description = description;
        }

        public decimal Value { get; private set; }
        public string Description { get; private set; }
    }
}