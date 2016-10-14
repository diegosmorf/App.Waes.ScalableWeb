using App.Waes.ScalableWeb.Core.Domain;

namespace App.Waes.ScalableWeb.Core.Repository
{
    public interface IRepositoryPersistenceService<in TEntity> where TEntity : IEntityBase
    {
        bool Insert(TEntity instance);

        bool Delete(int id);

        bool Update(TEntity instance);
    }
}