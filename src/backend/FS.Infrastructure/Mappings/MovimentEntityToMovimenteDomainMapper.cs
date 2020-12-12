using FS.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using FS.Utils.Enums;

namespace FS.Data.Mappings
{
    public static class MovimentEntityToMovimenteDomainMapper
    {
        public static FS.Domain.Model.Moviment MapFrom(Moviment entity)
        {
            if (entity is null) return null;

            return new FS.Domain.Model.Moviment
                (entity.Id,
                entity.Value,
                entity.Description,
                entity.Category,
                entity.Type,
                entity.AccountId,
                entity.CreatedOn,
                entity.UpdatedOn.GetValueOrDefault());
        }


        public static IEnumerable<FS.Domain.Model.Moviment> MapFrom(IEnumerable<Moviment> entities)
        {
            return entities?.Select(e => new FS.Domain.Model.Moviment(
                e.Id,
                e.Value,
                e.Description,
                e.Category,
                e.Type,
                e.AccountId,
                e.CreatedOn,
                e.UpdatedOn.GetValueOrDefault()));
        }
    }
}