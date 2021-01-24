namespace FS.DataObject.Moviment.Request
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System;
    using Utils.Enums;

    public class EditInvestmentRequest
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EInvestmentType Type { get; set; }
    }
}