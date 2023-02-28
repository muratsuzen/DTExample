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

        public PagedList<TEntity> GetList(Expression<Func<TEntity, bool>>? filter = null, int index = 0, int size = 10)
        {
            IQueryable<TEntity> queryable = context.Set<TEntity>();
            if (filter != null) queryable = queryable.Where(filter);

            return PagedList<TEntity>.ToPagedList(queryable, index, size);
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
