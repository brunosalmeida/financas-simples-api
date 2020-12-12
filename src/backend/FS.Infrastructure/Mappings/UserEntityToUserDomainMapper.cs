using FS.Data.Entities;

namespace FS.Data.Mappings
{
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
                entity.CreatedOn,
                entity.UpdatedOn.GetValueOrDefault());
        }
    }
}