using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Smooth.IoC.Cqrs.Commanding;
using Smooth.IoC.Cqrs.Query;
using Smooth.IoC.Cqrs.Requests;
using Smooth.IoC.Cqrs.Tap;
using Smooth.IoC.Cqrs.Tests.TestHelpers;

namespace Smooth.IoC.Cqrs.Tests.ExampleTests.IoC.IoC_Example_Installers
{
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
}
