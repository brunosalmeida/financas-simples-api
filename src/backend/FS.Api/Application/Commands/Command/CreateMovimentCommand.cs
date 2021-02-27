namespace FS.Api.Application.Commands.Command
{
    using System;
    using MediatR;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Utils.Enums;

    public class CreateMovementCommand : IRequest<Guid>
    {
        public CreateMovementCommand(Guid userId, Guid accountId, string description, decimal value, EMovementCategory category, EMovementType type)
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
        
       
        public EMovementCategory Category { get; }
        
       
        public EMovementType Type { get; }
    }
}