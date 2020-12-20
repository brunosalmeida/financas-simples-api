namespace FS.Api.Application.Commands.Command
{
    using System;
    using MediatR;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Utils.Enums;

    public class CreateMovimentCommand : IRequest<Guid>
    {
        public CreateMovimentCommand(Guid userId, Guid accountId, string description, decimal value, EMovimentCategory category, EMovimentType type)
        {
            UserId = userId;
            AccountId = accountId;
            Description = description;
            Value = value;
            Type = type;
            Category = category;
        }

        public Guid UserId { get; }
        public Guid AccountId { get; }
        public string Description { get; }
        public decimal Value { get; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public EMovimentCategory Category { get; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public EMovimentType Type { get; }
    }
}