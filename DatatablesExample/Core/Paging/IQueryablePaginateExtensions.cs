using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.Paging
{
    public static class IQueryablePaginateExtensions
    {
        public static async Task<IPaginate<T>> ToPaginateAsync<T>(this IQueryable<T> source, int index, int size,
                                                                  int from = 0,
                                                                  CancellationToken cancellationToken = default)
        {
            if (from > index) throw new ArgumentException($"From: {from} > Index: {index}, must from <= Index");

            int count = await source.CountAsync(cancellationToken).ConfigureAwait(false);
            List<T> items = await source.Skip((index - from) * size).Take(size).ToListAsync(cancellationToken)
                                        .ConfigureAwait(false);
            Paginate<T> list = new()
            {
                Index = index,
                Size = size,
                From = from,
                Count = count,
                Items = items,
                Pages = (int)Math.Ceiling(count / (double)size)
            };
            return list;
        }


        public static IPaginate<T> ToPaginate<T>(this IQueryable<T> source, int index, int size,
                                                 int from = 0)
        {
            if (from > index) throw new ArgumentException($"From: {from} > Index: {index}, must from <= Index");

            int count = source.Count();
            List<T> items = source.Skip((index - from) * size).Take(size).ToList();
            Paginate<T> list = new()
            {
                Index = index,
                Size = size,
                From = from,
                Count = count,
                Items = items,
                Pages = (int)Math.Ceiling(count / (double)size)
            };
            return list;
        }

        public static IQueryable<T> OrderByField<T>(this IQueryable<T> q, string SortField, bool Ascending)
        {
            var param = Expression.Parameter(typeof(T), "p");
            var prop = Expression.Property(param, SortField);
            var exp = Expression.Lambda(prop, param);
            string method = Ascending ? "OrderBy" : "OrderByDescending";
            Type[] types = new Type[] { q.ElementType, exp.Body.Type };
            var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
            return q.Provider.CreateQuery<T>(mce);
        }
    }
}
