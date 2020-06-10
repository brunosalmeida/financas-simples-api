using System.Collections.Generic;
using System.Linq;

namespace FS.Infrastructure.Mappings
{
    public class ExpenseEntityToExpenseDomainMapper
    {
        public static FS.Domain.Model.Expense MapFrom(Expense entity)
        {
            return new FS.Domain.Model.Expense
                (entity.Id,
                entity.Value,
                entity.Description,
                entity.CreatedOn,
                entity.UpdatedOn.GetValueOrDefault());
        }


        public static IEnumerable<FS.Domain.Model.Expense> MapFrom(IEnumerable<Expense> entities)
        {           
            return entities.Select(e => new FS.Domain.Model.Expense(
                e.Id,
                e.Value,
                e.Description,
                e.CreatedOn,
                e.UpdatedOn.GetValueOrDefault()));
        }
    }
}