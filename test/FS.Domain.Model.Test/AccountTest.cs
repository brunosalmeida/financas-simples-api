using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FS.Domain.Model.Test
{
    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        public void AddNewDeposit_ReturnNewBalance()
        {
            var account = new Account(new User("test", "test@email.com", "12345"));
            account.SetExpense(new Expense(100, "new deposit"));

            var balance = account.GetBalance();

            Assert.AreEqual(100, balance);
        }

        [TestMethod]
        public void AddNewExpense_ReturnNewBalance()
        {
            var account = new Account(new User("test", "test@email.com", "12345"));
            account.SetExpense(new Expense(100, "new deposit"));
            account.SetExpense(new Expense(-50, "new expense"));

            var balance = account.GetBalance();

            Assert.AreEqual(50, balance);
        }

        [TestMethod]
        public void CreateNewAccount_ReturnNewAccountWithZeroValueAsBalance()
        {
            var account = new Account(new User("test", "test@email.com", "12345"));
            var balance = account.GetBalance();

            Assert.AreEqual(0, balance);
        }
    }
}