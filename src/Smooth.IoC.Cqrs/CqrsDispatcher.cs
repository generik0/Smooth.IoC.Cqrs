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
        public Task<TReply> ExecuteAsync<TReply>() where TReply : IComparable
        {
            return _requestDispatcher.ExecuteAsync<TReply>();
        }
        public TReply Execute<TReply>() where TReply : IComparable
        {
            return _requestDispatcher.Execute<TReply>();
        }

        public IRequestHandler<TRequest, TReply> GetRequestHandler<TRequest, TReply>() where TRequest : IRequest where TReply : IComparable
        {
            return _requestDispatcher.GetRequestHandler<TRequest, TReply>();
        }

        public IRequestHandler<TReply> GetRequestHandler<TReply>() where TReply : IComparable
        {
            return _requestDispatcher.GetRequestHandler<TReply>();
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

        public TResult QuerySingleOrDefault<TResult>()  where TResult : class
        {
            return _queryDispatcher.QuerySingleOrDefault<TResult>();
        }

        public IQueryHandler<TResult> GetQueryHandler<TResult>() where TResult : class
        {
            return _queryDispatcher.GetQueryHandler<TResult>();
        }

        public IQueryHandler<TQuery, TResult> GetQueryHandler<TQuery, TResult>() where TQuery : IQuery where TResult : class
        {
            return _queryDispatcher.GetQueryHandler<TQuery, TResult>();
        }

        public IQuerySingleHandler<TResult> GetSingleOrDefaultQueryHandler<TResult>() where TResult : class
        {
            return _queryDispatcher.GetSingleOrDefaultQueryHandler<TResult>();
        }

        public IQuerySingleHandler<TQuery, TResult> GetSingleOrDefaultQueryHandler<TQuery, TResult>() where TQuery : IQuery where TResult : class
        {
            return _queryDispatcher.GetSingleOrDefaultQueryHandler<TQuery, TResult>();
        }

        public ICommandHandler<TCommand> GetCommandHandler<TCommand>() where TCommand : ICommand
        {
            return _commandDispatcher.GetCommandHandler<TCommand>();
        }
    }
}
