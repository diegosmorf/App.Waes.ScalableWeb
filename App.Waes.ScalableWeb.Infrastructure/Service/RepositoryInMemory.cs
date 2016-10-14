using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using App.Waes.ScalableWeb.Core.Domain;
using App.Waes.ScalableWeb.Core.Repository;

namespace App.Waes.ScalableWeb.Infrastructure.Service
{
    public class RepositoryInMemory<TEntity> : IRepository<TEntity> where TEntity : IEntityBase
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

        public TEntity Find(Expression<Func<TEntity, bool>> expressao)
        {
            if (Repository.Any())
                return Repository.AsQueryable().First(expressao);
            return default(TEntity);
        }

        public IEnumerable<TEntity> All()
        {
            return Repository;
        }

        public IEnumerable<TEntity> FindList(Expression<Func<TEntity, bool>> expressao)
        {
            return Repository.AsQueryable().Where(expressao);
        }

        public void Dispose()
        {
        }
    }
}