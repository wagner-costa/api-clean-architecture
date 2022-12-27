using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Route.Domain.Contracts.Repositories;

namespace Travel.Route.Domain.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query,
            Func<IIncludable<T>, IIncludable> includes)
            where T : class
        {
            if (includes == null)
                return query;

            var includable = (Includable<T>)includes(new Includable<T>(query));
            return includable.Input;
        }
    }
}
