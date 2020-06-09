using FS.Domain.Model.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FS.Domain.Model.Test.Validators
{
    [TestClass]
    public class UserValidatorTest
    {
        [TestMethod]
        public void CreateUSerWithValidData_ShouldReturnsNewUserInstance()
        {
            var user = new User("TestName", "email@email.com", "myPassword2020");
            var validator = new UserValidator();

            var result = validator.Validate(user);

            Assert.IsTrue(result.IsValid);
        }

        #region Property name

        [TestMethod]
        public void CreateUSerWithNameLenghtGreaterThan100_ShouldReturnsError()
        {
            var user = new User(new string('C', 101), "email@email.com", "myPassword2020");
            var validator = new UserValidator();

            var result = validator.Validate(user);

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 1);
            Assert.AreEqual("User's name must have less than 100 characters", result.Errors.First().ErrorMessage);
            Assert.AreEqual("MaximumLengthValidator", result.Errors.First().ErrorCode);
        }

        [TestMethod]
        public void CreateUSerWithNameLenghtLessThan2_ShouldReturnsError()
        {
            var user = new User(new string('C', 2), "email@email.com", "myPassword2020");
            var validator = new UserValidator();

            var result = validator.Validate(user);

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 1);
            Assert.AreEqual("User's name must have more than 2 characters", result.Errors.First().ErrorMessage);
            Assert.AreEqual("MinimumLengthValidator", result.Errors.First().ErrorCode);
        }

        [TestMethod]
        public void CreateUSerWithoutName_ShouldReturnsError()
        {
            var user = new User(null, "email@email.com", "myPassword2020");
            var validator = new UserValidator();

            var result = validator.Validate(user);

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 2);
            Assert.IsTrue(result.Errors.Any(e => e.ErrorMessage == "User's name can not be null"));
            Assert.IsTrue(result.Errors.Any(e => e.ErrorMessage == "User's name can not be empty"));
            Assert.IsTrue(result.Errors.Any(e => e.ErrorCode == "NotNullValidator"));
            Assert.IsTrue(result.Errors.Any(e => e.ErrorCode == "NotEmptyValidator"));
        }

        #endregion Property name

        [TestMethod]
        public void CreateUSerWithEmailLenghtGreaterThan50_ShouldReturnsError()
        {
            var user = new User("Test", $"{new string('C', 45)}@tt.com", "myPassword2020");
            var validator = new UserValidator();

            var result = validator.Validate(user);

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 1);
            Assert.AreEqual("User's email must have less than 50 characters", result.Errors.First().ErrorMessage);
            Assert.AreEqual("MaximumLengthValidator", result.Errors.First().ErrorCode);
        }

        [TestMethod]
        public void CreateUSerWithEmailLenghtLessThan2_ShouldReturnsError()
        {
            var user = new User("Test", $"{new string('C', 2)}@tt.com", "myPassword2020");
            var validator = new UserValidator();

            var result = validator.Validate(user);

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 1);
            Assert.AreEqual("User's email must have more than 10 characters", result.Errors.First().ErrorMessage);
            Assert.AreEqual("MinimumLengthValidator", result.Errors.First().ErrorCode);
        }

        [TestMethod]
        public void CreateUSerWithoutEmail_ShouldReturnsError()
        {
            var user = new User("Test", null, "myPassword2020");
            var validator = new UserValidator();

            var result = validator.Validate(user);

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 2);
            Assert.IsTrue(result.Errors.Any(e => e.ErrorMessage == "User's email can not be null"));
            Assert.IsTrue(result.Errors.Any(e => e.ErrorMessage == "User's email can not be empty"));
            Assert.IsTrue(result.Errors.Any(e => e.ErrorCode == "NotNullValidator"));
            Assert.IsTrue(result.Errors.Any(e => e.ErrorCode == "NotEmptyValidator"));
        }

        [TestMethod]
        public void CreateUSerWithInvalidFormatEmail_ShouldReturnsError()
        {
            var user = new User("Test", "teste.com.br", "myPassword2020");
            var validator = new UserValidator();

            var result = validator.Validate(user);

            Assert.IsFalse(result.IsValid);
            Assert.IsTrue(result.Errors.Count == 1);
            Assert.IsTrue(result.Errors.Any(e => e.ErrorMessage == "User's email must have a valid format"));
            Assert.IsTrue(result.Errors.Any(e => e.ErrorCode == "EmailValidator"));
        }
    }
}