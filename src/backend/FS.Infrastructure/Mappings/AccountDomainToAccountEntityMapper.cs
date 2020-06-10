namespace FS.Infrastructure.Mappings
{
    public class AccountDomainToAccountEntityMapper
    {
        public static Account MapFrom(FS.Domain.Model.Account model)
        {
            if (model is null) return null;

            return new Account()
            {
                Id = model.Id,
                User = UserDomainToUserEntityMapper.MapFrom(model.User),
                Expenses = ExpenseDomainToExpenseEntityMapper.MapFrom(model.Expenses),
                CreatedOn = model.CreatedOn,
                UpdatedOn = model.UpdatedOn
            };
        }
    }
}