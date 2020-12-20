namespace FS.DataObject.Moviment.Response
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System;
    using Utils.Enums;

    public class CreateMovimentResponse
    {
        public Guid AccountId { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public EMovimentCategory Category { get; set; }
    }
}