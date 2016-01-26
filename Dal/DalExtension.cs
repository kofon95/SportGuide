using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Dal
{
    public static class DalExtensions
    {
        public static IQueryable<T> AsNoTracking<T>(this IQueryable<T> source) where T : class
        {
            return QueryableExtensions.AsNoTracking(source);
        }
        public static IQueryable<T> Include<T>(this IQueryable<T> source, string path) where T : class
        {
            return QueryableExtensions.Include(source, path);
        }
        public static IQueryable<T> Include<T, TProperty>(this IQueryable<T> source, Expression<Func<T, TProperty>> path) where T : class
        {
            return QueryableExtensions.Include(source, path);
        }
    }
}
