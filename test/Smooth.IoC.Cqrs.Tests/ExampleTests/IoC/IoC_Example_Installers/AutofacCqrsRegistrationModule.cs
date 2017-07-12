using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Smooth.IoC.Cqrs.Commanding;
using Smooth.IoC.Cqrs.Query;
using Smooth.IoC.Cqrs.Requests;
using Smooth.IoC.Cqrs.Tap;

namespace Smooth.IoC.Cqrs.Tests.ExampleTests.IoC.IoC_Example_Installers
{
    public class AutofacCqrsRegistrationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new HandlerFactory(c.Resolve<IComponentContext>())).As<IHandlerFactory>().SingleInstance();
            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>().SingleInstance();
            builder.RegisterType<RequestDispatcher>().As<IRequestDispatcher>().SingleInstance();
            builder.RegisterType<QueryDispatcher>().As<IQueryDispatcher>().SingleInstance();
            builder.RegisterType<CqrsDispatcher>().As<ICqrsDispatcher>().SingleInstance();
            builder.RegisterAssemblyTypes(Assembly.GetCallingAssembly())
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                .AsClosedTypesOf(typeof(IRequestHandler<>))
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .AsClosedTypesOf(typeof(IQueryHandler<,>))
                .AsClosedTypesOf(typeof(IQueryHandler<>))
                .AsClosedTypesOf(typeof(IQuerySingleHandler<,>))
                .InstancePerDependency()
                .PreserveExistingDefaults();
        }


        private sealed class HandlerFactory : IHandlerFactory
        {
            private readonly IComponentContext _container;

            public HandlerFactory(IComponentContext container)
            {
                _container = container;
            }

            public IRequestHandler<TRequest, TReply> ResolveRequest<TRequest, TReply>()
                where TRequest : IRequest where TReply : IComparable
            {
                return _container.Resolve<IRequestHandler<TRequest, TReply>>();
            }

            public IRequestHandler<TReply> ResolveRequest<TReply>() where TReply : IComparable
            {
                return _container.Resolve<IRequestHandler<TReply>>();
            }

            public ICommandHandler<TCommand> ResolveCommand<TCommand>() where TCommand : ICommand
            {
                return _container.Resolve<ICommandHandler<TCommand>>();
            }

            public IQueryHandler<TQuery, TResult> ResolveQuery<TQuery, TResult>()
                where TQuery : IQuery where TResult : class
            {
                return _container.Resolve<IQueryHandler<TQuery, TResult>>();
            }

            public IQueryHandler<TResult> ResolveQuery<TResult>() where TResult : class
            {
                return _container.Resolve<IQueryHandler<TResult>>();
            }

            public IQuerySingleHandler<TQuery, TResult> ResolveSingleQuery<TQuery, TResult>()
                where TQuery : IQuery where TResult : class
            {
                return _container.Resolve<IQuerySingleHandler<TQuery, TResult>>();
            }

            public IQuerySingleHandler<TResult> ResolveSingleQuery<TResult>() where TResult : class
            {
                return _container.Resolve<IQuerySingleHandler<TResult>>();
            }

            public void Release(IDisposable instance)
            {
                instance.Dispose();
            }
        }
    }
}
