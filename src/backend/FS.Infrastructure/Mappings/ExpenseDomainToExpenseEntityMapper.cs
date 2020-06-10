using System.Collections.Generic;
using System.Linq;

namespace FS.Infrastructure.Mappings
{
    public class ExpenseDomainToExpenseEntityMapper
    {
        public static Expense MapFrom(FS.Domain.Model.Expense model)
        {
            return new Expense()
            {
                Id = model.Id,
                Value = model.Value,
                Description = model.Description,
                CreatedOn = model.CreatedOn,
                UpdatedOn = model.UpdatedOn
            };
        }

        public static IEnumerable<Expense> MapFrom(IEnumerable<FS.Domain.Model.Expense> models)
        {
            return models.Select(m => new Expense()
            {
                Id = m.Id,
                Value = m.Value,
                Description = m.Description,
                CreatedOn = m.CreatedOn,
                UpdatedOn = m.UpdatedOn.GetValueOrDefault()
            });
        }
    }
}