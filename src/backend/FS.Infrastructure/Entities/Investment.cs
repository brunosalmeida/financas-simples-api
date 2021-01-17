namespace FS.Data.Entities
{
    using System;
    using Utils.Enums;

    public sealed class Investment : Entity
    {        
        public Guid AccountId { get; set; }
        public Guid UserId { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public Account Account { get; set; }
        public EInvestmentType Type { get;  set; }
    }
}