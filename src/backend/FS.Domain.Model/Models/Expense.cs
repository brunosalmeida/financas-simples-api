namespace FS.Domain.Model
{
    using System;
    using FS.Utils.Enums;

    public class Expense : Base
    {
        public decimal Value { get; private set; }
        public string Description { get; private set; }
        public ECategory Category { get; private set; }
        public Guid AccountId { get; private set; }

        public Expense(decimal value, string description, ECategory category, Guid accountId) : base()
        {
            Value = value;
            Description = description;
            Category = category;
            AccountId = accountId;
        }

        public Expense(Guid id, decimal value, string description, ECategory category, Guid accountId,
            DateTime createdOn, DateTime? updatedOn)
            : base(id, createdOn, updatedOn)
        {
            Value = value;
            Description = description;
            Category = category;
            AccountId = accountId;
        }

        public void SetCategory(ECategory category) => this.Category = category;
        public void SetValue(decimal value) => this.Value = value;
        public void SetDescription(string description) => this.Description = description;
    }
}