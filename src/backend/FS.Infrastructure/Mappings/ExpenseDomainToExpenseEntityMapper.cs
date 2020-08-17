using FS.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace FS.Data.Mappings
{
    public static class ExpenseDomainToExpenseEntityMapper
    {
        public static Expense MapFrom(FS.Domain.Model.Expense model)
        {
            if (model is null) return null;

            return new Expense()
            {
                Id = model.Id,
                Value = model.Value,
                Description = model.Description,
                CreatedOn = model.CreatedOn,
                UpdatedOn = model.UpdatedOn
            };
        }

        public static IList<Expense> MapFrom(IEnumerable<FS.Domain.Model.Expense> models)
        {
            if (models is null) return null;

            return models.Select(m => new Expense()
            {
                Id = m.Id,
                Value = m.Value,
                Description = m.Description,
                CreatedOn = m.CreatedOn,
                UpdatedOn = m.UpdatedOn.GetValueOrDefault()
            }).ToList();
        }
    }
}