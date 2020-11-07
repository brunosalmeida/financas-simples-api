namespace FS.Api.Commands.Command
{
    using System;
    using MediatR;

    public class EditExpenseCommand: IRequest<Guid>
    {
        public EditExpenseCommand(Guid userId, Guid expenseId, decimal value, string description, int category)
        {
            UserId = userId;
            ExpenseId = expenseId;
            Value = value;
            Description = description;
            Category = category;
        }
        
        public Guid UserId { get; set; }
        public Guid ExpenseId { get; set; }
        public decimal Value { get; set; }
        public string Description { get; set; }
        public int Category { get; set; }
        
    }
}