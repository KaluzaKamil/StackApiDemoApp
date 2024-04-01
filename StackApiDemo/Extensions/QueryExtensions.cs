using Microsoft.OpenApi.Extensions;
using StackApiDemo.Enums;
using StackApiDemo.Parameters;
using System.Linq.Expressions;

namespace StackApiDemo.Extensions
{
    public static class QueryExtensions
    {
        public static IQueryable<T> OrderByPropertyName<T>(this IQueryable<T> query, OrderByProperties property, bool ascending)
        {
            var param = Expression.Parameter(typeof(T), "p");
            var prop = Expression.Property(param, property.ToString());
            var exp = Expression.Lambda(prop, param);

            var method = ascending ? "OrderBy" : "OrderByDescending";
            var types = new Type[] { query.ElementType, exp.Body.Type };
            var rs = Expression.Call(typeof(Queryable), method, types, query.Expression, exp);
            return query.Provider.CreateQuery<T>(rs);
        }
    }
}
