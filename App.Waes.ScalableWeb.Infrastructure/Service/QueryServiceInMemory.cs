using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using App.Waes.ScalableWeb.Core.Domain;
using App.Waes.ScalableWeb.Core.Repository;

namespace App.Waes.ScalableWeb.Infrastructure.Service
{
    public class QueryServiceInMemory<TEntity> : IRepositoryQueryService<TEntity> where TEntity : IEntityBase
    {
        protected readonly List<TEntity> Repository = new List<TEntity>();

        public TEntity Find(Expression<Func<TEntity, bool>> expressao)
        {
            return Repository.Any() ? Repository.AsQueryable().First(expressao) : default(TEntity);
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