namespace FS.Data.Mappings
{
    using Entities;

    public static class BalanceDomainToBalanceEntityMapper
    {
        public static Balance MapFrom(FS.Domain.Model.Balance model)
        {
            if (model is null) return null;

            return new Balance()
            {
                Id = model.Id,
                Value = model.Value,
                UserId = model.UserId,
                AccountId = model.AccountId,
                CreatedOn = model.CreatedOn,
                UpdatedOn = model.UpdatedOn
            };
        }
    }
}