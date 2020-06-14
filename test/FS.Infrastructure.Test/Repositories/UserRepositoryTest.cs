using FizzWare.NBuilder;
using FS.Infrastructure.Mappings;
using FS.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FS.Infrastructure.Test.Repositories
{
    [TestClass]
    public class UserRepositoryTest
    {
        private Mock<DatabaseContext> contextMock;
        private Mock<DbSet<User>> dbSetUserMock;
        private IEnumerable<User> entities;

        [TestInitialize]
        public void Initialize()
        {
            entities = Builder<User>
                .CreateListOfSize(5)
                .All()
                .With(u => u.Id = Guid.NewGuid())
                .With(u => u.CreatedOn = DateTime.UtcNow)
                .Build();

            this.dbSetUserMock = entities.AsQueryable().BuildMockDbSet();

            this.contextMock = new Mock<DatabaseContext>();
            this.contextMock.Setup(c => c.Users).Returns(this.dbSetUserMock.Object);
        }

        [TestMethod]
        public void InsertUserWithValidData_ShouldReturns()
        {
            var model = Builder<Domain.Model.User>
                .CreateNew()
                .WithFactory(() => new Domain.Model.User("Test", "email@email.com", "Password123"))
                .Build();

            this.contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var repository = new UserRepository(contextMock.Object);
            Task.Run(() => repository.Insert(model));

            this.dbSetUserMock.Verify(u => u.Add(It.IsAny<User>()), Times.Once);
            this.contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task GetUserWithValidId_ShouldReturns()
        {
            var id = entities.ElementAt(0).Id;
            var expected = UserEntityToUserDomainMapper.MapFrom(entities.ElementAt(0));

            var repository = new UserRepository(contextMock.Object);
            var result = await repository.Get(id);

            Assert.AreEqual(expected.Id, result.Id);
        }
    }
}