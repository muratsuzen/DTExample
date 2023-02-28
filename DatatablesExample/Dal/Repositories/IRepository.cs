using Core.Paging;
using Entities;
using System.Linq.Expressions;

namespace Dal.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Get(Expression<Func<T, bool>> filter);
        IPaginate<T> GetList(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, int index = 0, int size = 10,string sortColumn = "", bool sortDirection = true);
        T Add(T entity);
        T Update(T entity);
    }
}
