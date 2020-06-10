namespace FS.Infrastructure.Mappings
{
    public class UserDomainToUserEntityMapper
    {
        public static User MapFrom(FS.Domain.Model.User model)
        {
            return new User()
            {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                CreatedOn = model.CreatedOn,
                UpdatedOn = model.UpdatedOn,
                Password = model.Password
            };
        }
    }
}