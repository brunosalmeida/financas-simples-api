namespace FS.DataObject.Movement.Response
{
    using System;
    using Utils.Enums;

    public class GetAllInvestmentResponse
    {
        public Guid Id { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public EInvestmentType Type { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}