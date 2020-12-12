namespace FS.Api.Application.Commands.Command
{
    using System;
    using MediatR;

    public class CreateExpenseCommand : IRequest<Guid>
    {
        public CreateExpenseCommand(Guid userId, Guid accountId, string description, decimal value, int category, int type)
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
        public int Category { get; }
        public int Type { get; }
    }
}