namespace FS.DataObject.Expense.Request
{
    using System;

    public class CreateMovimentRequest
    {
        public string Description { get; set; }
        public decimal Value { get; set; }
        public int Category { get; set; }
        
        public int Type { get; set; }
    }
}