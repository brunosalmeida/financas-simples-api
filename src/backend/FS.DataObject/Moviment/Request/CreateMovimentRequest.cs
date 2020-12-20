namespace FS.DataObject.Moviment.Request
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Utils.Enums;

    public class CreateMovimentRequest
    {
        public string Description { get; set; }
        public decimal Value { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public EMovimentCategory Category { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public EMovimentType Type { get; set; }
    }
}