namespace FS.Data.Entities
{
    using System;

    public class Balance : Entity
    {
        public Decimal Value { get; set; }
        
        public Guid UserId { get; set; }
        
        public Guid AccountId { get; set; }
    }
}