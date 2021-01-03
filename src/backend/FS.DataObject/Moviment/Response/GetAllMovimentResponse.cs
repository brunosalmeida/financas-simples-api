namespace FS.DataObject.Moviment.Response
{
    using System;
    using Utils.Enums;

    public class GetAllMovimentResponse
    {
        public Guid Id { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public EMovimentCategory Category { get; set; }
        public EMovimentType Type { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}