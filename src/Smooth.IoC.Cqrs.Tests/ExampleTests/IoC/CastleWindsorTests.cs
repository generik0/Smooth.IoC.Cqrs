using System;
using System.Linq;
using Castle.Core.Internal;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using FluentAssertions;
using NUnit.Framework;
using Smooth.IoC.Cqrs.Commanding;
using Smooth.IoC.Cqrs.Tests.ExampleTests.IoC.IoC_Example_Installers;
using Smooth.IoC.Cqrs.Tests.TestHelpers;
using Smooth.IoC.Cqrs.Tests.TestHelpers.Commands;

namespace Smooth.IoC.Cqrs.Tests.ExampleTests.IoC
{
    [TestFixture]
    public class CastleWindsorTests
    {
        private static IWindsorContainer _container;

        [SetUp]
        public void TestSetup()
        {
            if (_container != null) return;

            _container = new WindsorContainer();
            Assert.DoesNotThrow(() =>
            {
                _container.Install(new CastleWindsorInstaller());
                _container.Kernel.Resolver.AddSubResolver(new CollectionResolver(_container.Kernel, true));
                _container.Register(Classes.FromThisAssembly()
                    .Where(t => t.GetInterfaces().Any(x => x != typeof(IDisposable))
                                && !t.HasAttribute<NoIoCFluentRegistration>())
                    .Unless(t => t.IsAbstract)
                    .Configure(c =>
                    {
                        c.IsFallback();
                    })
                    .LifestyleTransient()
                    .WithServiceAllInterfaces());
            });
            
        }


        [Test]
        public void ICommandDispatcher_Resolves_MyCommandHandler_ExecuteAsync_Executes_Correctly()
        {
            var actual = new MyCommandModel
            {
                Value = 1
            };
            var commandHandler = _container.Resolve<ICommandDispatcher>();
            Assert.DoesNotThrowAsync(async ()=>  await commandHandler.ExecuteAsync(actual) );
            actual.Value.Should().Be(2);
        }

        [Test]
        public void ICommandDispatcher_MyCommandModel_MyCommandHandler_ExecuteAsync_Executes_Correctly()
        {
            var actual = new MyCommandModel
            {
                Value = 2
            };
            var commandHandler = _container.Resolve<ICommandDispatcher<MyCommandModel>>();
            Assert.DoesNotThrowAsync(async () => await commandHandler.ExecuteAsync(actual));
            actual.Value.Should().Be(3);
        }

        [Test]
        public void IHandle_OpenClose_ExecuteAsync_Executes_Correctly()
        {
            var actual = new MyCommandModel
            {
                Value = 3
            };
            var handles = _container.ResolveAll<IHandle>();
            var handle = handles.FirstOrDefault(x => x.IsHandel<MyCommandHandler>()) as MyCommandHandler;
            Assert.DoesNotThrowAsync(async () => await handle.ExecuteAsync(actual));
            actual.Value.Should().Be(4);
        }

        [Test]
        public void ICommandHandler_OpenClose_ExecuteAsync_Executes_Correctly()
        {
            var actual = new MyCommandModel
            {
                Value = 4
            };
            var handle = _container.Resolve<ICommandHandler<MyCommandModel>>();
            Assert.DoesNotThrowAsync(async () => await handle.ExecuteAsync(actual));
            actual.Value.Should().Be(5);
        }

        [Test]
        public void Resolve_IBrave_AndExecuteAllMethodsCorrectly()
        {
            var actual = new MyCommandModel
            {
                Value = 10
            };
            var brave = _container.Resolve<IBrave>();
            brave.DoDispatch(actual);
            actual.Value.Should().Be(11);
            brave.DoSpecialDispatch(actual);
            actual.Value.Should().Be(12);
            brave.DoDecoratorDispatch(actual);
            actual.Value.Should().Be(13);
            brave.DoExactHandler(actual);
            actual.Value.Should().Be(14);
        }
    }
}
