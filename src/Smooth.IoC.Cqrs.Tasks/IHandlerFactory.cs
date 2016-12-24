using System;
using Smooth.IoC.Cqrs.Tasks.Commanding;
using Smooth.IoC.Cqrs.Tasks.Query;
using Smooth.IoC.Cqrs.Tasks.Requests;

namespace Smooth.IoC.Cqrs.Tasks
{
  
    public interface IHandlerFactory
    {
        IRequestHandler<TRequest, TReply> ResolveRequest<TRequest, TReply>()
            where TRequest : IRequest
            where TReply : class ;

        ICommandHandler<TCommand> Resolve<TCommand>()
            where TCommand : ICommand;

        IQueryHandler<TQuery, TResult> ResolveQuery<TQuery, TResult>()
            where TQuery : IQuery
            where TResult : class;

        void Release(IDisposable instance);
    }
}
