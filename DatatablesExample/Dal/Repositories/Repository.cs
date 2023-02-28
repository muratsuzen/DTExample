using Core.Paging;
using Dal.Context;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dal.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected ApplicationDbContext context;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return context.Set<TEntity>().FirstOrDefault(filter);
        }

        public IPaginate<TEntity> GetList(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, int index = 0, int size = 10, string sortColumn = "",bool sortDirection = true)
        {
            IQueryable<TEntity> queryable = context.Set<TEntity>();
            if (filter != null) queryable = queryable.Where(filter);
            if (orderBy != null) return orderBy(queryable).ToPaginate(index, size);

            if (!string.IsNullOrEmpty(sortColumn))
                queryable = queryable.OrderByField(sortColumn, sortDirection);

            return queryable.ToPaginate(index, size);
        }

        

        public TEntity Add(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
            context.Entry(entity).State = EntityState.Added;
            context.SaveChanges();
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
            return entity;
        }
    }
}
