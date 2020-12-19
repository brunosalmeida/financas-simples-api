namespace FS.Domain.Model
{
    using System;

    public class Balance : Base
    {
        public Decimal Value { get; private set; }

        public Guid UserId { get; private set; }

        public Guid AccountId { get; private set; }

        public Balance(Guid userId, Guid accountId, decimal value)
            : base()
        {
            Value = value;
            UserId = userId;
            AccountId = accountId;
        }
        
        public Balance(Guid id, Guid userId, Guid accountId, decimal value, DateTime createdOn, DateTime? updatedOn)
            : base(id, createdOn, updatedOn)
        {
            Value = value;
            UserId = userId;
            AccountId = accountId;
        }

        public void UpdateBalance(decimal value)
        {
            this.Value += value;
        }
    }
}