namespace FS.Domain.Model
{
    using System;
    using FS.Utils.Enums;
    using Newtonsoft.Json;

    public class Moviment : Base
    {
        public decimal Value { get; private set; }
        public string Description { get; private set; }
        public EMovimentCategory Category { get; private set; }
        public EMovimentType Type { get; private set; }
        public Guid AccountId { get; }
        public Guid UserId { get; }

        public Moviment(decimal value, string description, EMovimentCategory category, EMovimentType type,
            Guid accountId, Guid userId)
            : base()
        {
            Type = type;
            Value = Type == EMovimentType.Credit ? value : (value * -1);
            Description = description;
            Category = category;
            AccountId = accountId;
            UserId = userId;
        }

        public Moviment(Guid id, decimal value, string description, EMovimentCategory category, EMovimentType type,
            Guid accountId, Guid userId, DateTime createdOn, DateTime? updatedOn)
            : base(id, createdOn, updatedOn)
        {
            Value = value;
            Description = description;
            Category = category;
            Type = type;
            AccountId = accountId;
            UserId = userId;
        }

        //Ctor only for Hangfire works.
        [JsonConstructor]
        public Moviment(Guid id, decimal value, string description, EMovimentCategory category, EMovimentType type,
            Guid accountId, Guid userId, DateTime createdOn)
            : base(id, createdOn, null)
        {
            Value = value;
            Description = description;
            Category = category;
            Type = type;
            AccountId = accountId;
            UserId = userId;
        }

        public void SetCategory(EMovimentCategory category) => this.Category = category;

        public void SetMovimentType(EMovimentType type)
        {
            this.Type = type;

            if (this.Type != EMovimentType.Credit)
                this.Value *= -1;
        }

        public void SetValue(decimal value)
        {
            if (this.Type != EMovimentType.Credit)
                this.Value *= -1;
            else
                this.Value = value;
        }

        public void SetDescription(string description) => this.Description = description;
    }
}