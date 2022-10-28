![Project Icon](https://raw.githubusercontent.com/Generik0/Smooth.IoC.Cqrs/master/logo.jpg) Smooth.IoC
===========================================

[![generik0 VSTS Build Status](https://generik0.visualstudio.com/_apis/public/build/definitions/97e62cdf-8c46-48a2-bf7a-d40bf05a53eb/5/badge)
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
builder.RegisterModule&lt;AutofacCqrsRegistrationModule&gt;();
</pre></code>

The regisration model (You can copy paste it):
<pre><code>
public class AutofacCqrsRegistrationModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(c =&gt; new HandlerFactory(c.Resolve&lt;IComponentContext&gt;())).As&lt;IHandlerFactory&gt;().SingleInstance();
        builder.RegisterType&lt;CommandDispatcher&gt;().As&lt;ICommandDispatcher&gt;().SingleInstance();
        builder.RegisterType&lt;RequestDispatcher&gt;().As&lt;IRequestDispatcher&gt;().SingleInstance();
        builder.RegisterType&lt;QueryDispatcher&gt;().As&lt;IQueryDispatcher&gt;().SingleInstance();
        builder.RegisterType&lt;CqrsDispatcher&gt;().As&lt;ICqrsDispatcher&gt;().SingleInstance();
        var assemblies=AppDomain.CurrentDomain.GetAssemblies();
        builder.RegisterAssemblyTypes(assemblies)
            .AsClosedTypesOf(typeof(IRequestHandler&lt;,&gt;))
            .InstancePerDependency()
            .PreserveExistingDefaults();
        builder.RegisterAssemblyTypes(assemblies)
            .AsClosedTypesOf(typeof(IRequestHandler&lt;&gt;))
            .InstancePerDependency()
            .PreserveExistingDefaults();
        builder.RegisterAssemblyTypes(assemblies)
            .AsClosedTypesOf(typeof(ICommandHandler&lt;&gt;))
            .InstancePerDependency()
            .PreserveExistingDefaults();
         builder.RegisterAssemblyTypes(assemblies)
            .AsClosedTypesOf(typeof(IQueryHandler&lt;,&gt;))
            .InstancePerDependency()
            .PreserveExistingDefaults();
          builder.RegisterAssemblyTypes(assemblies)
            .AsClosedTypesOf(typeof(IQueryHandler&lt;&gt;))
            .InstancePerDependency()
            .PreserveExistingDefaults();
          builder.RegisterAssemblyTypes(assemblies)
            .AsClosedTypesOf(typeof(IQuerySingleHandler&lt;,&gt;))
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
        public IRequestHandler&lt;TRequest, TReply&gt; ResolveRequest&lt;TRequest, TReply&gt;()
            where TRequest : IRequest where TReply : IComparable
        {
            return _container.Resolve&lt;IRequestHandler&lt;TRequest, TReply&gt;&gt;();
        }
        public IRequestHandler&lt;TReply&gt; ResolveRequest&lt;TReply&gt;() where TReply : IComparable
        {
            return _container.Resolve&lt;IRequestHandler&lt;TReply&gt;&gt;();
        }
        public ICommandHandler&lt;TCommand&gt; ResolveCommand&lt;TCommand&gt;() where TCommand : ICommand
        {
            return _container.Resolve&lt;ICommandHandler&lt;TCommand&gt;&gt;();
        }
        public IQueryHandler&lt;TQuery, TResult&gt; ResolveQuery&lt;TQuery, TResult&gt;()
            where TQuery : IQuery where TResult : class
        {
            return _container.Resolve&lt;IQueryHandler&lt;TQuery, TResult&gt;&gt;();
        }
        public IQueryHandler&lt;TResult&gt; ResolveQuery&lt;TResult&gt;() where TResult : class
        {
            return _container.Resolve&lt;IQueryHandler&lt;TResult&gt;&gt;();
        }
        public IQuerySingleHandler&lt;TQuery, TResult&gt; ResolveSingleQuery&lt;TQuery, TResult&gt;()
            where TQuery : IQuery where TResult : class
        {
            return _container.Resolve&lt;IQuerySingleHandler&lt;TQuery, TResult&gt;&gt;();
        }
        public void Release(IDisposable instance)
        {
            instance.Dispose();
        }
    }
}


## Castle Windsor Installer
You only need to register the factory and the dispatchers. Castle factory will take care of all the rest:

Adding the registration:
<pre><code>
_container.Install(new CastleWindsorCqrsInstaller());
    _container.Kernel.Resolver.AddSubResolver(new CollectionResolver(_container.Kernel, true));
    _container.Register(Classes.FromThisAssembly()
        .Where(t =&gt; t.GetInterfaces().Any(x =&gt; x != typeof(IDisposable))
        .Unless(t =&gt; t.IsAbstract)
        .Configure(c =&gt;
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
        if (FacilityHelper.DoesKernelNotAlreadyContainFacility&lt;TypedFactoryFacility&gt;(container))
        {
            container.Kernel.AddFacility&lt;TypedFactoryFacility&gt;();
        }
        container.Register(Component.For&lt;IHandlerFactory&gt;().AsFactory());
        container.Register(Component.For&lt;ICommandDispatcher&gt;().ImplementedBy&lt;CommandDispatcher&gt;());
        container.Register(Component.For&lt;IRequestDispatcher&gt;().ImplementedBy&lt;RequestDispatcher&gt;());
        container.Register(Component.For&lt;IQueryDispatcher&gt;().ImplementedBy&lt;QueryDispatcher&gt;());
        container.Register(Component.For&lt;ICqrsDispatcher&gt;().ImplementedBy&lt;CqrsDispatcher&gt;());
    }
}
</pre></code>
