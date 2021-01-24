namespace FS.Data.Mappings
{
    using Entities;
    using System.Collections.Generic;
    using System.Linq;

    public static class InvestmentEntityToInvestmentDomainMapper
    {
        public static FS.Domain.Model.Investment MapFrom(Investment entity)
        {
            if (entity is null) return null;

            return new FS.Domain.Model.Investment
            (entity.Id,
                entity.Value,
                entity.Description,
                entity.Type,
                entity.AccountId,
                entity.UserId,
                entity.MovimentId,
                entity.CreatedOn,
                entity.UpdatedOn.GetValueOrDefault());
        }


        public static IEnumerable<FS.Domain.Model.Investment> MapFrom(IEnumerable<Investment> entities)
        {
            return entities?.Select(e => new FS.Domain.Model.Investment(
                e.Id,
                e.Value,
                e.Description,
                e.Type,
                e.AccountId,
                e.UserId,
                e.MovimentId,
                e.CreatedOn,
                e.UpdatedOn.GetValueOrDefault()));
        }
    }
}