using FS.Data.Entities;
using FS.Data.Mappings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace FS.Data.Test.Mappings
{
    [TestClass]
    public class AccountDomainToAccountEntityMapperTest
    {
        [TestMethod]
        public void ConvertAccountEntityToAccountModel_ShouldReturnsNewInstanceOfAccountEntity()
        {
            var entity = new Account()
            {
                Id = Guid.NewGuid(),
                User = new User()
                { 
                    Id = Guid.NewGuid(),
                    Name = "Test",
                    Email = "email@email.com",
                    Password = "Password123",
                    CreatedOn = DateTime.UtcNow
                },
                CreatedOn = DateTime.UtcNow,
                Expenses = new List<Expense>()
            };

            var model = AccountEntityToAccountDomainMapper.MapFrom(entity);

            Assert.AreEqual(typeof(Domain.Model.Account), model.GetType());
        }

        [TestMethod]
        public void ConvertNullAccountEntityToAccountDomain_ShouldReturnsNull()
        {
            Account entity = null;
            var model = AccountEntityToAccountDomainMapper.MapFrom(entity);

            Assert.IsNull(model);
        }
    }
}