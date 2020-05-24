using System;
using System.Collections.Generic;
using System.Linq;

namespace FS.Domain.Model
{
    public class Account : Base
    {
        public Account(User user)
           : base()
        {
            User = user;
            Expenses = new List<Expense>().AsEnumerable();
        }
        public Account(Guid id, User user, IEnumerable<Expense> expenses, DateTime createdOn, DateTime? updatedOn)
            : base(id, createdOn, updatedOn)
        {
            User = user;
            Expenses = expenses;
        }

        public User User { get; private set; }
        public IEnumerable<Expense> Expenses { get; private set; }

        public decimal GetBalance()
        {
            return this.Expenses.Sum(e => e.Value);
        }

        public void SetExpense(Expense expense)
        {
            var expenses = this.Expenses.ToList();
            expenses.Add(expense);

            this.Expenses = expenses;
        }
    }
}