using FS.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace FS.Data.Mappings
{
    public static class MovementDomainToMovementEntityMapper
    {
        public static Movement MapFrom(FS.Domain.Model.Movement model)
        {
            if (model is null) return null;

            return new Movement()
            {
                Id = model.Id,
                Value = model.Value,
                Description = model.Description,
                Category = model.Category,
                Type = model.Type,
                AccountId = model.AccountId,
                CreatedOn = model.CreatedOn,
                UpdatedOn = model.UpdatedOn
            };
        }

        public static IList<Movement> MapFrom(IEnumerable<FS.Domain.Model.Movement> models)
        {
            return models?.Select(m => new Movement()
            {
                Id = m.Id,
                Value = m.Value,
                Description = m.Description,
                Category = m.Category,
                Type = m.Type,
                AccountId = m.AccountId,
                CreatedOn = m.CreatedOn,
                UpdatedOn = m.UpdatedOn.GetValueOrDefault()
            }).ToList();
        }
    }
}