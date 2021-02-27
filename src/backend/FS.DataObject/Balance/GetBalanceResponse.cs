// unset

namespace FS.DataObject.Balance
{
    using System;

    public class GetBalanceResponse
    {
        public Guid AccountId { get; set; } 

        public Guid UserId { get; set; }

        public Balances Balances { get; set; }
    }

    public class Balances
    {
        public decimal MovementBalance { get; set; }
        public decimal MovementInvestment { get; set; }
    }
}