namespace FS.DataObject.Expense.Response
{
    using System;

    public class CreateExpenseResponse
    {
        public Guid AccountId { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public int Category { get; set; }
    }
}