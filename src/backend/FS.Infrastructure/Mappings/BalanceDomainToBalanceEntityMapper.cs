namespace FS.Data.Mappings
{
    using Entities;

    public static class BalanceEntityToBalanceDomainMapper
    {
        public static FS.Domain.Model.Balance MapFrom(Balance entity)
        {
            if (entity is null) return null;

            return new FS.Domain.Model.Balance
            (
                entity.Id,
                entity.UserId,
                entity.AccountId,
                entity.Value,
                entity.CreatedOn,
                entity.UpdatedOn
            );
        }
    }
}