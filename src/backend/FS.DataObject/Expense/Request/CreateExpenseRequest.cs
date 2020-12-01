namespace FS.DataObject.Expense.Request
{
    using System;

    public class CreateExpenseRequest
    {
        public string Description { get; set; }
        public decimal Value { get; set; }
        public int Category { get; set; }
        public Guid AccountId { get; set; }
    }
}