using FS.Data.Entities;
using FS.Data.Mappings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FS.Data.Test.Mappings
{
    [TestClass]
    public class ExpenseDomainToExpenseEntityMapperTest
    {
        [TestMethod]
        public void ConvertExpenseDomainToExpenseEnitity_ShouldReturnsNewInstanceOfExpenseEntity()
        {
            var model = new Domain.Model.Expense(100, "Some description");
            var entity = ExpenseDomainToExpenseEntityMapper.MapFrom(model);

            Assert.AreEqual(typeof(Expense), entity.GetType());
        }

        [TestMethod]
        public void ConvertNullExpenseDomainToExpenseEnitity_ShouldReturnsNull()
        {
            Domain.Model.Expense model = null;
            var entity = ExpenseDomainToExpenseEntityMapper.MapFrom(model);

            Assert.IsNull(entity);
        }

        [TestMethod]
        public void ConvertListOfExpenseDomainToExpenseEnitity_ShouldReturnsNewInstanceOfListOfExpenseEntity()
        {
            var models = new List<Domain.Model.Expense>();
            var model = new Domain.Model.Expense(100, "Some description");

            models.Add(model);

            var entity = ExpenseDomainToExpenseEntityMapper.MapFrom(models);

            Assert.AreEqual(typeof(List<Expense>), entity.GetType());
        }

        [TestMethod]
        public void ConvertNullListOfExpenseDomainToListOfExpenseEnitity_ShouldReturnsNull()
        {
            List<Domain.Model.Expense> model = null;
            var entity = ExpenseDomainToExpenseEntityMapper.MapFrom(model);

            Assert.IsNull(entity);
        }
    }
}