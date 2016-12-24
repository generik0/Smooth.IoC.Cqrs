using System.Collections.Generic;
using System.Threading.Tasks;
using Smooth.IoC.Cqrs.Tasks.Commanding;
using Smooth.IoC.Cqrs.Tasks.Query;
using Smooth.IoC.Cqrs.Tasks.Requests;

namespace Smooth.IoC.Cqrs.Tasks
{
    public class CqrsDispatcher : ICqrsDispatcher
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IRequestDispatcher _requestDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public CqrsDispatcher(ICommandDispatcher commandDispatcher, IRequestDispatcher requestDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _requestDispatcher = requestDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        public Task ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            return _commandDispatcher.ExecuteAsync(command);
        }

        public Task<TReply> ExecuteAsync<TRequest, TReply>(TRequest request) where TRequest : IRequest where TReply : class
        {
            return _requestDispatcher.ExecuteAsync<TRequest, TReply>(request);
        }

        public Task<IEnumerable<TResult>> QueryAsync<TQuery, TResult>() where TQuery : IQuery where TResult : class
        {
            return  _queryDispatcher.QueryAsync<TQuery, TResult>();
        }

        public Task<IEnumerable<TResult>> QueryAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery where TResult : class
        {
            return  _queryDispatcher.QueryAsync<TQuery, TResult>(query);
        }

        public Task<TResult> QuerySingleOrDefaultAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery where TResult : class
        {
            return  _queryDispatcher.QuerySingleOrDefaultAsync<TQuery, TResult>(query);
        }
    }
}
