using System.Collections.Generic;
using App.Waes.ScalableWeb.Core.Domain;
using App.Waes.ScalableWeb.Core.Repository;

namespace App.Waes.ScalableWeb.Infrastructure.Service
{
    public class PersistenceServiceInMemory<TEntity> : IRepositoryPersistenceService<TEntity> where TEntity : IEntityBase
    {
        public readonly List<TEntity> Repository = new List<TEntity>();

        public bool Update(TEntity instancia)
        {
            Delete(instancia.Id);
            return Insert(instancia);
        }

        public bool Delete(int id)
        {
            Repository.RemoveAll(x => x.Id == id);
            return true;
        }

        public bool Insert(TEntity instancia)
        {
            Repository.Add(instancia);

            return true;
        }

        public void Dispose()
        {
        }
    }
}