namespace FS.Infrastructure.Mappings
{
    public class UserEntityToUserDomainMapper
    {
        public static FS.Domain.Model.User MapFrom(User entity)
        {
            return new FS.Domain.Model.User(
                entity.Id,
                entity.Name,
                entity.Email,
                entity.Password,
                entity.CreatedOn,
                entity.UpdatedOn.GetValueOrDefault());
        }
    }
}
