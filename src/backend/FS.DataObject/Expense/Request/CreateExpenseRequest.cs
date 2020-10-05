namespace FS.DataObject.Expense.Request
{
    using System;

    public class CreateExpenseRequest
    {
        public Guid AccountId { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public int Category { get; set; }
    }
}