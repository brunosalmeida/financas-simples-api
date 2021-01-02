namespace FS.Domain.Model
{
    using System;
    using Utils.Enums;

    public class InstallmentMoviment : Moviment
    {
        public InstallmentMoviment(decimal value,  int months, int startMonth, string description, EMovimentCategory category, EMovimentType type,
            Guid accountId, Guid userId) 
            : base(value, description, category, type, accountId, userId)
        {
            Months = months;
            StartMonth = startMonth;
        }

        public InstallmentMoviment(Guid id, decimal value, int months, int startMonth, string description, EMovimentCategory category,
            EMovimentType type, Guid accountId, Guid userId, DateTime createdOn, DateTime? updatedOn) 
            : base(id, value, description, category, type, accountId, userId, createdOn, updatedOn)
        {
            Months = months;
            StartMonth = startMonth;
        }
        public int Months { get; }
        public int StartMonth { get; }
        public int EndMonth => this.StartMonth + this.Months;
        public decimal InstallmentsValue =>  this.Type == EMovimentType.Expense ? (this.Value / this.Months) * -1 : (this.Value / this.Months);
    }
}