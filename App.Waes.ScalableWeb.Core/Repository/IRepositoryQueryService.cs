using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using App.Waes.ScalableWeb.Core.Domain;

namespace App.Waes.ScalableWeb.Core.Repository
{
    public interface IRepositoryQueryService<TEntity> where TEntity : IEntityBase
    {
        TEntity Find(Expression<Func<TEntity, bool>> expression);

        IEnumerable<TEntity> All();

        IEnumerable<TEntity> FindList(Expression<Func<TEntity, bool>> expression);
    }
}