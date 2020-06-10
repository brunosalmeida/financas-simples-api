using FS.Infrastructure.Mappings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FS.Infrastructure.Test.Mappings
{
    [TestClass]
    public class AccountEntityToAccountDomainMapperTest
    {
        [TestMethod]
        public void ConvertAccountModelToAccountEntity_ShouldReturnsNewInstanceOfAccountModel()
        {
            var user = new FS.Domain.Model.User("Test", "email@email.com", "Password123");
            var model = new FS.Domain.Model.Account(user);

            var entity = AccountDomainToAccountEntityMapper.MapFrom(model);

            Assert.AreEqual(typeof(Account), entity.GetType());
        }

        [TestMethod]
        public void ConvertNullAccountEntityToAccountDomain_ShouldReturnsNull()
        {
            FS.Domain.Model.Account model = null;
            var entity = AccountDomainToAccountEntityMapper.MapFrom(model);

            Assert.IsNull(entity);
        }
    }
}