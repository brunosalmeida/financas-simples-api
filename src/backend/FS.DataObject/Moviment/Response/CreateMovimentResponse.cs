namespace FS.DataObject.Movement.Response
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System;
    using Utils.Enums;

    public class CreateMovementResponse
    {
        public Guid AccountId { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        
       
        public EMovementCategory Category { get; set; }
    }
}