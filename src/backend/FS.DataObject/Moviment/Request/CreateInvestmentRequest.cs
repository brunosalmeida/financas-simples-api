namespace FS.DataObject.Movement.Request
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Utils.Enums;

    public class CreateInvestmentRequest
    {
        public string Description { get; set; }
        public decimal Value { get; set; }

       
        public EInvestmentType Type { get; set; }
    }
}