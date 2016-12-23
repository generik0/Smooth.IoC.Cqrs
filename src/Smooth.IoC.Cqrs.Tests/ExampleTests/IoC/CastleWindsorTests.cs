using System;
using System.Linq;
using Castle.Core.Internal;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using FluentAssertions;
using NUnit.Framework;
using Smooth.IoC.Cqrs.Commanding;
using Smooth.IoC.Cqrs.Requests;
using Smooth.IoC.Cqrs.Tests.ExampleTests.IoC.IoC_Example_Installers;
using Smooth.IoC.Cqrs.Tests.TestHelpers;
using Smooth.IoC.Cqrs.Tests.TestHelpers.Commands;
using Smooth.IoC.Cqrs.Tests.TestHelpers.Requests;

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
        public void ICommandDispatcher_Resolves_MyCommandModel_ExecuteAsync_Executes_Correctly()
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
        public void IHandle_Resolves_OpenClose_ExecuteAsync_Executes_Correctly()
        {
            var actual = new MyCommandModel
            {
                Value = 3
            };
            var handles = _container.ResolveAll<IHandler>();
            var handle = handles.FirstOrDefault(x => x.IsHandel<MyCommandHandler>()) as MyCommandHandler;
            Assert.DoesNotThrowAsync(async () => await handle.ExecuteAsync(actual));
            actual.Value.Should().Be(4);
        }

        [Test]
        public void ICommandHandler_Resolves_OpenClose_ExecuteAsync_Executes_Correctly()
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



        [Test]
        public void IRequestDispatcher_Resolves_MyRequestHandler_ExecuteAsync_Executes_Correctly()
        {
            var request = new MyRequestModel
            {
                Value = 1
            };
            var handler = _container.Resolve<IRequestDispatcher>();
            MyReplyModel result = null;
            Assert.DoesNotThrowAsync(async () =>  result = await handler.ExecuteAsync<MyRequestModel, MyReplyModel>(request));
            result.Actual.Should().Be(2);
        }

        [Test]
        public void IRequestDispatcher_Resolves_MyRequestModel_ExecuteAsync_Executes_Correctly()
        {
            var request = new MyRequestModel
            {
                Value = 2
            };
            var commandHandler = _container.Resolve<IRequestDispatcher<MyRequestModel, MyReplyModel>>();
            MyReplyModel result = null;
            Assert.DoesNotThrowAsync(async () =>  result = await commandHandler.ExecuteAsync(request));
            result.Actual.Should().Be(3);
        }

        [Test]
        public void IHandle_Resolves_MyRequestHandler_OpenClose_ExecuteAsync_Executes_Correctly()
        {
            var request = new MyRequestModel
            {
                Value = 3
            };
            var handles = _container.ResolveAll<IHandler>();
            var handle = handles.FirstOrDefault(x => x.IsHandel<MyRequestHandler>()) as MyRequestHandler;
            MyReplyModel result = null;
            Assert.DoesNotThrowAsync(async () => result = await handle.ExecuteAsync(request));
            result.Actual.Should().Be(4);
        }

        [Test]
        public void IRequestHandler_Reolves_OpenClose_ExecuteAsync_Executes_Correctly()
        {
            var request = new MyRequestModel
            {
                Value = 4
            };
            var handle = _container.Resolve<IRequestHandler<MyRequestModel, MyReplyModel>>();
            MyReplyModel result = null;
            Assert.DoesNotThrowAsync(async () => result = await handle.ExecuteAsync(request));
            result.Actual.Should().Be(5);
        }

        [Test]
        public void Resolve_INew_AndExecuteAllMethodsCorrectly()
        {
            var request = new MyRequestModel
            {
                Value = 10
            };
            var news = _container.Resolve<INew>();
            news.DoDispatch(request).Actual.Should().Be(11);
            news.DoSpecialDispatch(request).Actual.Should().Be(11);
            news.DoDecoratorDispatch(request).Actual.Should().Be(11);
            news.DoExactHandler(request).Actual.Should().Be(11);
        }


    }
}
