namespace FS.DataObject.Movement.Request
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Utils.Enums;

    public class CreateInstallmentMovementRequest
    {
        public string Description { get; set; }
        public decimal Value { get; set; }
        public int Months { get; set; }
        public EMonths StartMonth { get; set; }
        public EMovementCategory Category { get; set; }
        public EMovementType Type { get; set; }
    }
}