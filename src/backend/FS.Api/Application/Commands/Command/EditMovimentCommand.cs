namespace FS.Api.Application.Commands.Command
{
    using System;
    using MediatR;

    public class EditMovementCommand: IRequest<Guid>
    {
        public EditMovementCommand(Guid userId, Guid expenseId, decimal value, string description, int category, int type)
        {
            UserId = userId;
            MovementId = expenseId;
            Value = value;
            Description = description;
            Category = category;
            Type = type;
        }
        
        public Guid UserId { get; set; }
        public Guid MovementId { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public int Category { get; set; }
        public int Type { get; set; }
    }
}