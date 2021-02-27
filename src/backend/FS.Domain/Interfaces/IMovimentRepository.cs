namespace FS.Domain.Core.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FS.Domain.Model;

    public interface IMovementRepository : IGet<Movement>, ICreate<Movement>, IDelete<Movement>, IUpdate<Movement>
    {
        Task<IEnumerable<Movement>> GetMovementsByAccount(Guid userId, Guid accountId, int page, int size);
    }
}