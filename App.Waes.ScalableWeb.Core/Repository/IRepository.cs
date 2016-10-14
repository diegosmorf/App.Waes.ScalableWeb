using System;
using App.Waes.ScalableWeb.Core.Domain;

namespace App.Waes.ScalableWeb.Core.Repository
{
    public interface IRepository<TEntity> :
        IRepositoryPersistenceService<TEntity>,
        IRepositoryQueryService<TEntity>, IDisposable where TEntity : IEntityBase
    {
    }
}