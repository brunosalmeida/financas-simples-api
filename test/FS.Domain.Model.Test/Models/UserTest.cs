using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FS.Domain.Model.Test
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void UpdateName_ShouldReturnsNewUserWithNewName()
        {
            var user = new User("TestUser", "test@email.com", "12345");
            user.SetName("TestUser2");

            Assert.AreEqual("TestUser2", user.Name);
        }

        [TestMethod]
        public void UpdateEmail_ShouldReturnsNewUserWithNewEmail()
        {
            var user = new User("TestUser", "test2@email.com", "12345");
            user.SetEmail("test2@email.com");

            Assert.AreEqual("test2@email.com", user.Email);
        }

        [TestMethod]
        public void UpdatePassword_ShouldReturnsNewUserWithNewName()
        {
            var user = new User("TestUser", "test@email.com", "12345");
            user.SetPassword("999999");

            Assert.AreEqual("999999", user.Password);
        }
    }
}