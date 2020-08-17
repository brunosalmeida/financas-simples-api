using System;
using FS.Utils.Enums;

namespace FS.Domain.Model
{
    public class Expense : Base
    {
        public Expense(decimal value, string description, ECategory category) : base()
        {
            Value = value;
            Description = description;
            Category = category;
        }

        public Expense(Guid id, decimal value, string description, ECategory category, DateTime createdOn, DateTime? updatedOn)
            : base(id, createdOn, updatedOn)
        {
            Value = value;
            Description = description;
            Category = category;
        }

        public decimal Value { get; private set; }
        public string Description { get; private set; }

        public ECategory Category { get; private set; }
    }
}