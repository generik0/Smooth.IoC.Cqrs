using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Smooth.IoC.Cqrs.Commanding;
using Smooth.IoC.Cqrs.Query;
using Smooth.IoC.Cqrs.Requests;

namespace Smooth.IoC.Cqrs
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

        public void Execute<TCommand>(TCommand command) where TCommand : ICommand
        {
            _commandDispatcher.Execute(command); 
        }

        public Task<TReply> ExecuteAsync<TRequest, TReply>(TRequest request) where TRequest : IRequest where TReply : IComparable
        {
            return _requestDispatcher.ExecuteAsync<TRequest, TReply>(request);
        }
        public TReply Execute<TRequest, TReply>(TRequest request) where TRequest : IRequest where TReply : IComparable
        {
            return _requestDispatcher.Execute<TRequest, TReply>(request);
        }
        public Task<IEnumerable<TResult>> QueryAsync<TResult>() where TResult : class
        {
            return _queryDispatcher.QueryAsync<TResult>();
        }
        
        public Task<IEnumerable<TResult>> QueryAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery where TResult : class
        {
            return  _queryDispatcher.QueryAsync<TQuery, TResult>(query);
        }

        public Task<TResult> QuerySingleOrDefaultAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery where TResult : class
        {
            return  _queryDispatcher.QuerySingleOrDefaultAsync<TQuery, TResult>(query);
        }

        public IEnumerable<TResult> Query<TResult>() where TResult : class
        {
            return _queryDispatcher.Query<TResult>();
        }

        public IEnumerable<TResult> Query<TQuery, TResult>(TQuery query) where TQuery : IQuery where TResult : class
        {
            return _queryDispatcher.Query<TQuery, TResult>(query);
        }

        public TResult QuerySingleOrDefault<TQuery, TResult>(TQuery query) where TQuery : IQuery where TResult : class
        {
            return _queryDispatcher.QuerySingleOrDefault<TQuery, TResult>(query);
        }
    }
}
