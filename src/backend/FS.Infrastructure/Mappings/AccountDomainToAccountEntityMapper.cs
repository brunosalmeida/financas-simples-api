using FS.Data.Entities;

namespace FS.Data.Mappings
{
    public static class AccountDomainToAccountEntityMapper
    {
        public static Account MapFrom(FS.Domain.Model.Account model)
        {
            if (model is null) return null;

            return new Account()
            {
                Id = model.Id,
                UserId = model.User.Id,
                CreatedOn = model.CreatedOn,
                UpdatedOn = model.UpdatedOn
            };
        }
    }
}