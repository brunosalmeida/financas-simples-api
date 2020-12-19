using System;
using FS.Utils.Enums;

namespace FS.Data.Entities
{
    public sealed class Moviment : Entity
    {        
        public Guid AccountId { get; set; }
        public Guid UserId { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public Account Account { get; set; }
        public EMovimentCategory Category { get; set; }
        public EMovimentType Type { get;  set; }
    }
}