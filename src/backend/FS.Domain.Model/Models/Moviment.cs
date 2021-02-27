namespace FS.Domain.Model
{
    using System;
    using FS.Utils.Enums;
    using Newtonsoft.Json;

    public class Movement : Base
    {
        public decimal Value { get; private set; }
        public string Description { get; private set; }
        public EMovementCategory Category { get; private set; }
        public EMovementType Type { get; private set; }
        public Guid AccountId { get; }
        public Guid UserId { get; }

        public Movement(decimal value, string description, EMovementCategory category, EMovementType type,
            Guid accountId, Guid userId)
            : base()
        {
            Type = type;
            Value = Type == EMovementType.Credit ? value : (value * -1);
            Description = description;
            Category = category;
            AccountId = accountId;
            UserId = userId;
        }

        public Movement(Guid id, decimal value, string description, EMovementCategory category, EMovementType type,
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
        public Movement(Guid id, decimal value, string description, EMovementCategory category, EMovementType type,
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

        public void SetCategory(EMovementCategory category) => this.Category = category;

        public void SetMovementType(EMovementType type)
        {
            this.Type = type;

            if (this.Type != EMovementType.Credit)
                this.Value *= -1;
        }

        public void SetValue(decimal value)
        {
            if (this.Type != EMovementType.Credit)
                this.Value *= -1;
            else
                this.Value = value;
        }

        public void SetDescription(string description) => this.Description = description;
    }
}