using FS.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace FS.Data.Mappings
{
    public static class MovimentDomainToMovimentEntityMapper
    {
        public static Moviment MapFrom(FS.Domain.Model.Moviment model)
        {
            if (model is null) return null;

            return new Moviment()
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

        public static IList<Moviment> MapFrom(IEnumerable<FS.Domain.Model.Moviment> models)
        {
            return models?.Select(m => new Moviment()
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