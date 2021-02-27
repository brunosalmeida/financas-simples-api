namespace FS.Domain.Model
{
    using System;
    using Utils.Enums;

    public class InstallmentMovement : Movement
    {
        public InstallmentMovement(decimal value,  int months, EMonths startMonth, string description, EMovementCategory category, EMovementType type,
            Guid accountId, Guid userId) 
            : base(value, description, category, type, accountId, userId)
        {
            Months = months;
            StartMonth = startMonth;
        }

        public InstallmentMovement(Guid id, decimal value, int months, EMonths startMonth, string description, EMovementCategory category,
            EMovementType type, Guid accountId, Guid userId, DateTime createdOn, DateTime? updatedOn) 
            : base(id, value, description, category, type, accountId, userId, createdOn, updatedOn)
        {
            Months = months;
            StartMonth = startMonth;
        }
        public int Months { get; }
        public EMonths StartMonth { get; }
        public int EndMonth => (int)this.StartMonth + this.Months;
        public decimal InstallmentsValue =>  this.Type == EMovementType.Expense ? (this.Value / this.Months) * -1 : (this.Value / this.Months);
    }
}