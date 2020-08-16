using FizzWare.NBuilder;
using FS.Data.Entities;
using FS.Data.Mappings;
using FS.Data.Repositories;
using FS.Data.Test.Setup;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FS.Data.Test.Repositories
{
    [TestClass]
    public class UserRepositoryTest
    {
        private Mock<DatabaseContext> _contextMock;
        private Mock<DbSet<User>> _dbSetUserMock;
        private IList<User> _entities;

        [TestInitialize]
        public void Initialize()
        {
            _entities = Builder<User>
                .CreateListOfSize(5)
                .All()
                .With(u => u.Id = Guid.NewGuid())
                .With(u => u.CreatedOn = DateTime.UtcNow)
                .Build();

            this._dbSetUserMock = _entities.AsQueryable().BuildMockDbSet();
            this._contextMock = new Mock<DatabaseContext>();
            this._contextMock.Setup(c => c.Users).Returns(this._dbSetUserMock.Object);
        }

        [TestMethod]
        public async Task InsertUserWithValidData_ShouldReturns()
        {
            IEnumerable<User> entities = Builder<User>
               .CreateListOfSize(5)
               .All()
               .With(u => u.Id = Guid.NewGuid())
               .With(u => u.CreatedOn = DateTime.UtcNow)
               .Build();


            var model = Builder<Domain.Model.User>
                .CreateNew()
                .WithFactory(() => new Domain.Model.User("Test", "email@email.com", "Password123"))
                .Build();

            var setUserMock = MockDbSetFactory.Create<User>(entities);
            

            var contextMock = new Mock<DatabaseContext>();
            contextMock.Setup(c => c.Users).Returns(setUserMock.Object);
            contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var repository = new UserRepository(contextMock.Object);
            await repository.Insert(model);

            
            contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task GetUserWithValidId_ShouldReturns()
        {
            var id = _entities.ElementAt(0).Id;
            var expected = UserEntityToUserDomainMapper.MapFrom(_entities.ElementAt(0));

            var repository = new UserRepository(_contextMock.Object);
            var result = await repository.Get(id);

            Assert.AreEqual(expected.Id, result.Id);
        }

        [TestMethod]
        public async Task UpdateUserWithValidId_ShouldReturns()
        {
            var entity = _entities.ElementAt(0);

            var model = UserEntityToUserDomainMapper.MapFrom(entity);
            model.SetName("Updated Test Name");

            var repository = new UserRepository(_contextMock.Object);
            await repository.Update(model.Id, model);

            var result = await repository.Get(model.Id);

            Assert.AreEqual(entity.Id, result.Id);
            Assert.AreEqual("Updated Test Name", result.Name);

            this._contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteUserWithValidId_ShouldReturns()
        {
            var entity = _entities.ElementAt(0);

            this._dbSetUserMock.Setup(u => u.Remove(It.IsAny<User>())).Callback<User>((e) => _entities.Remove(e));

            var repository = new UserRepository(_contextMock.Object);
            await repository.Delete(entity.Id);

            var result = await repository.Get(entity.Id);

            Assert.IsNull(result);

            this._contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            this._dbSetUserMock.Verify(u => u.Remove(It.IsAny<User>()), Times.Once);
        }
    }
}