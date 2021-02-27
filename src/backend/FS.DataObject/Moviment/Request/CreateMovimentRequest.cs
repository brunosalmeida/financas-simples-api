namespace FS.DataObject.Movement.Request
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Utils.Enums;

    public class CreateMovementRequest
    {
        public string Description { get; set; }
        public decimal Value { get; set; }
        public EMovementCategory Category { get; set; }
        public EMovementType Type { get; set; }
    }
}