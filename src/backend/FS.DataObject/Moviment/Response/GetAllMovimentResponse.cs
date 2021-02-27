namespace FS.DataObject.Movement.Response
{
    using System;
    using Utils.Enums;

    public class GetAllMovementResponse
    {
        public Guid Id { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public EMovementCategory Category { get; set; }
        public EMovementType Type { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}