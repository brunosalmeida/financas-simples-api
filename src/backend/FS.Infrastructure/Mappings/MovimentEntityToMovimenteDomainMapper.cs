using FS.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using FS.Utils.Enums;

namespace FS.Data.Mappings
{
    public static class MovementEntityToMovementeDomainMapper
    {
        public static FS.Domain.Model.Movement MapFrom(Movement entity)
        {
            if (entity is null) return null;

            return new FS.Domain.Model.Movement
                (entity.Id,
                entity.Value,
                entity.Description,
                entity.Category,
                entity.Type,
                entity.AccountId,
                entity.UserId,
                entity.CreatedOn,
                entity.UpdatedOn.GetValueOrDefault());
        }


        public static IEnumerable<FS.Domain.Model.Movement> MapFrom(IEnumerable<Movement> entities)
        {
            return entities?.Select(e => new FS.Domain.Model.Movement(
                e.Id,
                e.Value,
                e.Description,
                e.Category,
                e.Type,
                e.AccountId,
                e.UserId,
                e.CreatedOn,
                e.UpdatedOn.GetValueOrDefault()));
        }
    }
}