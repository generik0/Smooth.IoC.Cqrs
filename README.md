![Project Icon](https://raw.githubusercontent.com/Generik0/Smooth.IoC.Cqrs/master/logo.jpg) Smooth.IoC.Dapper.Repository.UnitOfWork
===========================================

[![generik0 MyGet Build Status](https://www.myget.org/BuildSource/Badge/smootherioccqrs?identifier=b796f80f-c07d-40cd-ac13-853072214b19)](https://www.myget.org/)
[![NuGet](https://img.shields.io/nuget/v/Smooth.IoC.Cqrs.Tap.svg)](http://www.nuget.org/packages/Smooth.IoC.Cqrs.Tap)


# Smooth.IoC.Cqrs
Smoothest CQRS implementation, made for IoC and DI.

# Why
IoC / Dependancy Injection is one of the best design principles i know, but sometimes it is difficult to implement otehr patterns with it, or packages don't take into consideration that theyh should be IoC/DI friendly.
So far examples have been created for Autofac (e.g. for MVC6) and Castle windsor. If you need help for Ninject, Simplemigrator or any other IoC, let me know :-)

## Packages
1. Smooth.IoC.Cqrs: Base implemenation of Cqrs it can be used with any event mechanis,
2. Smooth.IoC.Cqrs.Tap: Implemenation of CQRS based .net's [Task-based Asynchronous Pattern (TAP)](https://msdn.microsoft.com/en-us/library/hh873175%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396)
3. (more to come-> LiteDB, RabbitMQ, MongoDB etc.)

# That simple. That smooth.#

# Code Examples: IoC registration
## Autofac registration
Autofac does have a factory using delegates but this does not fit the same pattern as all the other IoC. 
So one has to wrap the factory in a concrete implementation. Luckely the concrete implementation can be internal (or even private if you like).
Registration examples:	

Adding the registration:
<pre><code>
builder.RegisterModule<AutofacCqrsRegistrationModule>();
</pre></code>

The regisration model (You can copy paste it):
<pre><code>
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
        public void Release(IDisposable instance)
        {
            instance.Dispose();
        }
    }
}
</code></pre>

## Castle Windsor Installer
You only need to register the factory and the dispatchers. Castle factory will take care of all the rest:

Adding the registration:
<pre><code>
_container.Install(new CastleWindsorCqrsInstaller());
    _container.Kernel.Resolver.AddSubResolver(new CollectionResolver(_container.Kernel, true));
    _container.Register(Classes.FromThisAssembly()
        .Where(t => t.GetInterfaces().Any(x => x != typeof(IDisposable))
        .Unless(t => t.IsAbstract)
        .Configure(c =>
        {
            c.IsFallback();
        })
        .LifestyleTransient()
        .WithServiceAllInterfaces());
</pre></code>

The regisration model (You can copy paste it):
<pre><code>
public class CastleWindsorCqrsInstaller : IWindsorInstaller
{
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
        if (FacilityHelper.DoesKernelNotAlreadyContainFacility<TypedFactoryFacility>(container))
        {
            container.Kernel.AddFacility<TypedFactoryFacility>();
        }
        container.Register(Component.For<IHandlerFactory>().AsFactory());
        container.Register(Component.For<ICommandDispatcher>().ImplementedBy<CommandDispatcher>());
        container.Register(Component.For<IRequestDispatcher>().ImplementedBy<RequestDispatcher>());
        container.Register(Component.For<IQueryDispatcher>().ImplementedBy<QueryDispatcher>());
        container.Register(Component.For<ICqrsDispatcher>().ImplementedBy<CqrsDispatcher>());
    }
}
</pre></code>
