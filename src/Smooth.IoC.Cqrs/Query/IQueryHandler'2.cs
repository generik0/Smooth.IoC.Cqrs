using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Smooth.IoC.Cqrs.Query
{
    public interface IQueryHandler<TResult> : IDisposable 
        where TResult : class
    {
        /// <summary>
        /// Execute a query asynchronously.
        /// </summary>
        /// <returns>Task which will be completed once the query has been executed.</returns>
        Task<IEnumerable<TResult>> QueryAsync();

        /// <summary>
        /// Execute a query synchronously.
        /// </summary>
        /// <returns>Task which will be completed once the query has been executed.</returns>
        IEnumerable<TResult> Query();
    }
}
