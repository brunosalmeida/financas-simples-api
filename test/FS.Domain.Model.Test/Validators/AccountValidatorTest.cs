using FS.Domain.Model.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FS.Domain.Model.Test.Validators
{
    [TestClass]
    public class AccountValidatorTest
    {
        [TestMethod]
        public void CreateAccountWithValidData_ShouldReturnsNewAccountInstance()
        {
            var user = new User("TestName", "email@email.com", "myPassword2020");
            var account = new Account(user);
            var validator = new AccountValidator();

            var result = validator.Validate(account);

            Assert.IsTrue(result.IsValid);
        }
    }
}