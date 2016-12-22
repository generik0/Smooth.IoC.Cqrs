using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Smooth.IoC.Cqrs.Query
{
    public interface IQueryHandler<in TQuery, TResult> : IDisposable 
        where TQuery : IQuery
        where TResult : class
    {
        /// <summary>
        /// Execute a query asynchronously.
        /// </summary>
        /// <returns>Task which will be completed once the query has been executed.</returns>
        Task<IReadOnlyCollection<TResult>> QueryAsync();

        /// <summary>
        /// Execute a query asynchronously.
        /// </summary>
        /// <param name="query">Command to execute.</param>
        /// <returns>Task which will be completed once the query has been executed.</returns>
        Task<IReadOnlyCollection<TResult>> QueryAsync(TQuery query);

        /// <summary>
        /// Execute a query asynchronously.
        /// </summary>
        /// <param name="query">Command to execute.</param>
        /// <returns>Task which will be completed once the query has been executed.</returns>
        Task<TResult> QuerySingleOrDefaultAsync(TQuery query);
    }
}
