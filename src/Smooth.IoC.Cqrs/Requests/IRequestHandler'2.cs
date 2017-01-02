using System;
using System.Threading.Tasks;

namespace Smooth.IoC.Cqrs.Requests
{
    public interface IRequestHandler<TReply> : IDisposable 
        where TReply : IComparable
    {
        /// <summary>
        /// Execute a request asynchronously.
        /// </summary>
        /// <param name="request">Command to execute.</param>
        /// <returns>Task which will be completed once the command has been executed.</returns>
        Task<TReply> ExecuteAsync();

        /// <summary>
        /// Execute a request synchronously.
        /// </summary>
        /// <param name="request">Command to execute.</param>
        /// <returns>Task which will be completed once the command has been executed.</returns>
        TReply Execute();
    }
}
