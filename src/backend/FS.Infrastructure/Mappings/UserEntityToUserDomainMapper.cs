using FS.Data.Entities;

namespace FS.Data.Mappings
{
    using System.Collections.Generic;
    using System.Linq;

    public static class UserEntityToUserDomainMapper
    {
        public static FS.Domain.Model.User MapFrom(User entity)
        {
            if (entity is null) return null;

            return new FS.Domain.Model.User(
                entity.Id,
                entity.Name,
                entity.Email,
                entity.Password,
                entity.Gender,
                entity.BirthDate,
                entity.CreatedOn,
                entity.UpdatedOn.GetValueOrDefault());
        }
        
        public static IEnumerable<FS.Domain.Model.User> MapFrom(IEnumerable<User> entities)
        {
            return entities.Select(e => new FS.Domain.Model.User(
                e.Id,
                e.Name,
                e.Email,
                e.Password,
                e.Gender,
                e.BirthDate,
                e.CreatedOn,
                e.UpdatedOn.GetValueOrDefault()));
        }
    }
}