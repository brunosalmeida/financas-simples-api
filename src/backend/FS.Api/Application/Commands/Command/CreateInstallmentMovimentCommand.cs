// unset

namespace FS.Api.Application.Commands.Command
{
    using MediatR;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System;
    using Utils.Enums;

    public class CreateInstallmentMovementCommand : IRequest<Guid>
    {
        public CreateInstallmentMovementCommand(Guid userId, Guid accountId, string description, 
            decimal value, int months, EMonths startMonth, EMovementCategory category, EMovementType type)
        {
            UserId = userId;
            AccountId = accountId;
            Description = description;
            Value = value;
            Category = category;
            Type = type;
            StartMonth = startMonth;
            Months = months;
        }
        
        public Guid UserId { get; }
        public Guid AccountId { get; }
        public string Description { get; }
        public decimal Value { get; }
        public int Months { get; }
        
        public EMonths StartMonth { get; }

       
        public EMovementCategory Category { get; }

       
        public EMovementType Type { get; }
    }
}