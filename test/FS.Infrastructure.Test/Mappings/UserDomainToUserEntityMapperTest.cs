using FS.Data.Entities;
using FS.Data.Mappings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FS.Data.Test.Mappings
{
    [TestClass]
    public class UserDomainToUserEntityMapperTest
    {
        [TestMethod]
        public void ConvertUserDomainToUserEnitity_ShouldReturnsNewInstanceOfUserEntity()
        {
            var model = new Domain.Model.User("Test", "Email", "Password");
            var entity = UserDomainToUserEntityMapper.MapFrom(model);

            Assert.AreEqual(typeof(User), entity.GetType());
        }

        [TestMethod]
        public void ConvertNullUserDomainToUserEnitity_ShouldReturnsNull()
        {
            Domain.Model.User model = null;
            var entity = UserDomainToUserEntityMapper.MapFrom(model);

            Assert.IsNull(entity);
        }
    }
}