using System;
using Smooth.IoC.Cqrs.Commanding;
using Smooth.IoC.Cqrs.Query;
using Smooth.IoC.Cqrs.Requests;

namespace Smooth.IoC.Cqrs
{
  
    public interface IHandlerFactory
    {
        IRequestHandler<TRequest, TReply> ResolveRequest<TRequest, TReply>()
            where TRequest : IRequest
            where TReply : IComparable;

        ICommandHandler<TCommand> ResolveCommand<TCommand>()
            where TCommand : ICommand;

        IQueryHandler<TQuery, TResult> ResolveQuery<TQuery, TResult>()
            where TQuery : IQuery
            where TResult : class;

        IQueryHandler<TResult> ResolveQuery<TResult>()
            where TResult : class;

        IQuerySingleHandler<TQuery, TResult> ResolveSingleQuery<TQuery, TResult>()
            where TQuery : IQuery
            where TResult : class;

        void Release(IDisposable instance);
    }
}
