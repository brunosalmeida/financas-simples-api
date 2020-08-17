using FS.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using FS.Utils.Enums;

namespace FS.Data.Mappings
{
    public static class ExpenseEntityToExpenseDomainMapper
    {
        public static FS.Domain.Model.Expense MapFrom(Expense entity)
        {
            if (entity is null) return null;

            return new FS.Domain.Model.Expense
                (entity.Id,
                entity.Value,
                entity.Description,
                entity.Category,
                entity.CreatedOn,
                entity.UpdatedOn.GetValueOrDefault());
        }


        public static IEnumerable<FS.Domain.Model.Expense> MapFrom(IEnumerable<Expense> entities)
        {
            if (entities is null) return null;

            return entities.Select(e => new FS.Domain.Model.Expense(
                e.Id,
                e.Value,
                e.Description,
                e.Category,
                e.CreatedOn,
                e.UpdatedOn.GetValueOrDefault()));
        }
    }
}