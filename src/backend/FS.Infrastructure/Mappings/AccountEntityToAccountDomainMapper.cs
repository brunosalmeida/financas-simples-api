namespace FS.Infrastructure.Mappings
{
    public class AccountEntityToAccountDomainMapper
    {
        public static FS.Domain.Model.Account MapFrom(Account entity)
        {
            return new FS.Domain.Model.Account
                (
                 entity.Id,
                 UserEntityToUserDomainMapper.MapFrom(entity.User),
                 ExpenseEntityToExpenseDomainMapper.MapFrom(entity.Expenses),
                 entity.CreatedOn,
                 entity.UpdatedOn.GetValueOrDefault()
                );
        }
    }
}