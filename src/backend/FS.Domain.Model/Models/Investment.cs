namespace FS.Domain.Model
{
    using System;
    using Utils.Enums;

    public class Investment : Base
    {
        public decimal Value { get; private set; }
        public string Description { get; private set; }
        public EInvestmentType Type { get; private set; }
        public Guid MovimentId { get; private set; }
        public Guid AccountId { get; }
        public Guid UserId { get; }

        public Investment(decimal value, string description, EInvestmentType type,
            Guid accountId, Guid userId)
            : base()
        {
            Type = type;
            Value = value;
            Description = description;
            AccountId = accountId;
            UserId = userId;
        }

        public Investment(Guid id, decimal value, string description, EInvestmentType type,
            Guid accountId, Guid userId, Guid movimentId, DateTime createdOn, DateTime? updatedOn) 
            : base(id, createdOn, updatedOn)
        {
            Value = value;
            Description = description;
            Type = type;
            MovimentId = movimentId;
            AccountId = accountId;
            UserId = userId;
            MovimentId = movimentId;
        }
        public void SetInvestmentType(EInvestmentType type)
        {
            this.Type = type;
        }
        public void SetValue(decimal value) => this.Value = value;
        public void SetDescription(string description) => this.Description = description;
        public void SetMoviment(Guid movimentId) => this.MovimentId = movimentId;
    }
}