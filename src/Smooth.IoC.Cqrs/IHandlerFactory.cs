using System;
using Smooth.IoC.Cqrs.Commanding;
using Smooth.IoC.Cqrs.Requests;

namespace Smooth.IoC.Cqrs
{
  
    public interface IHandlerFactory
    {
        IRequestHandler<TRequest, TReply> Resolve<TRequest, TReply>()
            where TRequest : IRequest
            where TReply : class ;

        ICommandHandler<TCommand> Resolve<TCommand>()
            where TCommand : ICommand;

        void Release(IDisposable instance);
    }
}
