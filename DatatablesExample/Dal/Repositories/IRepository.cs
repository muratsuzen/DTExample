using Core.Paging;
using Entities;
using System.Linq.Expressions;

namespace Dal.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Get(Expression<Func<T, bool>> filter);
        PagedList<T> GetList(Expression<Func<T, bool>> filter = null, int index = 0, int size = 10);
        T Add(T entity);
        T Update(T entity);
    }
}
