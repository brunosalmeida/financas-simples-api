using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FS.Data.Mappings;
using FS.Data.Entities;

namespace FS.Data.Test.Mappings
{
    [TestClass]
    public class UserEntityToUserModelMapperTest
    {
        [TestMethod]
        public void ConvertUserEntityToUserModel_ShouldReturnsNewInstanceOfUserModel()
        {
            var entity = new User()
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Email = "Email",
                Password = "Password",
                CreatedOn = DateTime.UtcNow
            };

            var model = UserEntityToUserDomainMapper.MapFrom(entity);

            Assert.AreEqual(typeof(Domain.Model.User), model.GetType());
        }

        [TestMethod]
        public void ConvertNullUserEntityToUserDomain_ShouldReturnsNull()
        {
            User entity = null;
            var model = UserEntityToUserDomainMapper.MapFrom(entity);

            Assert.IsNull(model);
        }
    }
}
