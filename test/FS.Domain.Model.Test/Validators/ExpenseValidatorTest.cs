using FS.Domain.Model.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FS.Domain.Model.Test.Validators
{
    [TestClass]
    public class ExpenseValidatorTest
    {
        [TestMethod]
        public void CreateExpenseWithValidData_ShouldReturnsNewExpenseInstance()
        {
            var expense = new Expense(100, "Firs deposit");
            var validator = new ExpenseValidator();

            var result = validator.Validate(expense);

            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void CreateUSerWithoutDescription_ShouldReturnsError()
        {
            var expense = new Expense(100, null);
            var validator = new ExpenseValidator();

            var result = validator.Validate(expense);

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 2);
            Assert.IsTrue(result.Errors.Any(e => e.ErrorMessage == "Expense's description can not be null"));
            Assert.IsTrue(result.Errors.Any(e => e.ErrorMessage == "Expense's description can not be empty"));
            Assert.IsTrue(result.Errors.Any(e => e.ErrorCode == "NotNullValidator"));
            Assert.IsTrue(result.Errors.Any(e => e.ErrorCode == "NotEmptyValidator"));
        }
    }
}