// unset

namespace FS.DataObject.Balance
{
    using System;

    public class GetBalanceResponse
    {
        public Guid Id { get; set; }

        public Guid AccountId { get; set; }

        public Guid UserId { get; set; }

        public Decimal Balance { get; set; }
    }
}