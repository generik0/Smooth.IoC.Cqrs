using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using FluentAssertions;
using NUnit.Framework;
using Smooth.IoC.Cqrs.Commanding;
using Smooth.IoC.Cqrs.Query;
using Smooth.IoC.Cqrs.Requests;
using Smooth.IoC.Cqrs.Tests.ExampleTests.IoC.IoC_Example_Installers;
using Smooth.IoC.Cqrs.Tests.TestHelpers;
using Smooth.IoC.Cqrs.Tests.TestHelpers.Commands;
using Smooth.IoC.Cqrs.Tests.TestHelpers.Queries;
using Smooth.IoC.Cqrs.Tests.TestHelpers.Requests;

namespace Smooth.IoC.Cqrs.Tests.ExampleTests.IoC
{
    [TestFixture]
    public class CastleWindsorTasksTests
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
            var actual = MyReplyeEnum.Good;
            Assert.DoesNotThrowAsync(async () =>  actual = await handler.ExecuteAsync<MyRequestModel, MyReplyeEnum>(request));
            actual.Should().Be(MyReplyeEnum.Great);
        }

        [Test]
        public void IRequestDispatcher_Resolves_MyRequestModel_ExecuteAsync_Executes_Correctly()
        {
            var request = new MyRequestModel
            {
                Value = 2
            };
            var commandHandler = _container.Resolve<IRequestDispatcher<MyRequestModel, MyReplyeEnum>>();
            MyReplyeEnum actual = MyReplyeEnum.Good;
            Assert.DoesNotThrowAsync(async () =>  actual = await commandHandler.ExecuteAsync(request));
            actual.Should().Be(MyReplyeEnum.Great);
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
            var actual = MyReplyeEnum.Good;
            Assert.DoesNotThrowAsync(async () => actual = await handle.ExecuteAsync(request));
            actual.Should().Be(MyReplyeEnum.Great);
        }

        [Test]
        public void IRequestHandler_Reolves_OpenClose_ExecuteAsync_Executes_Correctly()
        {
            var request = new MyRequestModel
            {
                Value = 4
            };
            var handle = _container.Resolve<IRequestHandler<MyRequestModel, MyReplyeEnum>>();
            MyReplyeEnum actual = MyReplyeEnum.Good;
            Assert.DoesNotThrowAsync(async () => actual = await handle.ExecuteAsync(request));
            actual.Should().Be(MyReplyeEnum.Great);
        }

        [Test]
        public void Resolve_INew_AndExecuteAllMethodsCorrectly()
        {
            var request = new MyRequestModel
            {
                Value = 10
            };
            var news = _container.Resolve<INew>();
            news.DoDispatch(request).Should().Be(MyReplyeEnum.Great);
            news.DoSpecialDispatch(request).Should().Be(MyReplyeEnum.Great);
            news.DoDecoratorDispatch(request).Should().Be(MyReplyeEnum.Great);
            news.DoExactHandler(request).Should().Be(MyReplyeEnum.Great);
        }

        [Test]
        public void IQueryDispatcher_Resolves_Queries_Executes_Correctly()
        {
            var query = new MyQueryModel
            {
                Value = 1
            };
            var handler = _container.Resolve<IQueryDispatcher>();
            IEnumerable<MyResultModel> results = null;
            Assert.DoesNotThrowAsync(async () => results = await handler.QueryAsync<MyQueryModel, MyResultModel>(query));
            results.First().Actual.Should().Be(2);
            MyResultModel result = null;
            Assert.DoesNotThrowAsync(async () => result = await handler.QuerySingleOrDefaultAsync<MyQueryModel, MyResultModel>(query));
            result.Actual.Should().Be(2);
        }

        [Test]
        public void IQueryDispatcher_Resolves_MyQueryModel_Queries_Executes_Correctly()
        {
            var query = new MyQueryModel
            {
                Value = 2
            };
            var commandHandler = _container.Resolve<IQueryDispatcher<MyQueryModel, MyResultModel>>();
            IEnumerable<MyResultModel> results = null;
            Assert.DoesNotThrowAsync(async () => results = await commandHandler.QueryAsync(query));
            results.First().Actual.Should().Be(3);
            MyResultModel result = null;
            Assert.DoesNotThrowAsync(async () => result = await commandHandler.QuerySingleOrDefaultAsync(query));
            result.Actual.Should().Be(3);
        }

        [Test]
        public void IHandle_Resolves_MyQueryHandler_OpenClose_Queries_Executes_Correctly()
        {
            var query = new MyQueryModel
            {
                Value = 3
            };
            var handles = _container.ResolveAll<IHandler>();
            var handle = handles.FirstOrDefault(x => x.IsHandel<MyQueryHandler>()) as MyQueryHandler;
            IEnumerable<MyResultModel> results = null;
            Assert.DoesNotThrowAsync(async () => results = await handle.QueryAsync());
            results.First().Actual.Should().Be(1);
            Assert.DoesNotThrowAsync(async () => results = await handle.QueryAsync(query));
            results.First().Actual.Should().Be(4);
            MyResultModel result = null;
            Assert.DoesNotThrowAsync(async () => result = await handle.QuerySingleOrDefaultAsync(query));
            result.Actual.Should().Be(4);
        }

        [Test]
        public void IQueryHandler_Resolves_OpenClose_ExecuteAsync_Executes_Correctly()
        {
            var query = new MyQueryModel
            {
                Value = 4
            };
            var handle = _container.Resolve<IQueryHandler<MyQueryModel, MyResultModel>>();
            IEnumerable<MyResultModel> results = null;
            Assert.DoesNotThrowAsync(async () => results = await handle.QueryAsync(query));
            results.First().Actual.Should().Be(5);
            var singleHandle = _container.Resolve<IQuerySingleHandler<MyQueryModel, MyResultModel>>();
            MyResultModel result = null;
            Assert.DoesNotThrowAsync(async () => result = await singleHandle.QuerySingleOrDefaultAsync(query));
            result.Actual.Should().Be(5);
        }

        [Test]
        public void Resolve_IWorld_AndExecuteAllMethodsCorrectly()
        {
            var query = new MyQueryModel
            {
                Value = 10
            };
            var news = _container.Resolve<IWorld>();
            MyResultModel result1 = null;
            Assert.DoesNotThrowAsync(async ()=> result1 = await news.DoDispatch(query));
            MyResultModel result2 = null;
            Assert.DoesNotThrowAsync(async () => result2 = await news.DoSpecialDispatch(query));
            MyResultModel result3 = null;
            Assert.DoesNotThrowAsync(async () => result3 = await news.DoDecoratorDispatch(query));
            
            result1.Actual.Should().Be(12);
            result2.Actual.Should().Be(13);
            result3.Actual.Should().Be(12);
        }

        [Test]
        public void Resolve_ICqrsDispatcher_AndExecuteAllMethodsCorrectly()
        {
            var query = new MyQueryModel
            {
                Value = 10
            };
            var dispatcher = _container.Resolve<ICqrsDispatcher>();
            IEnumerable<MyResultModel> queryResult2 = null;
            Assert.DoesNotThrowAsync(async () => queryResult2 = await dispatcher.QueryAsync<MyQueryModel, MyResultModel>(query));
            MyResultModel queryResult3 = null;
            Assert.DoesNotThrowAsync(async () => queryResult3 = await dispatcher.QuerySingleOrDefaultAsync<MyQueryModel, MyResultModel>(query));
            queryResult2.First().Actual.Should().Be(11);
            queryResult3.Actual.Should().Be(11);

            var request = new MyRequestModel
            {
                Value = 20
            };
            MyReplyeEnum requestResult = MyReplyeEnum.Good;
            Assert.DoesNotThrowAsync(async () => requestResult = await dispatcher.ExecuteAsync<MyRequestModel, MyReplyeEnum>(request));
            requestResult.Should().Be(MyReplyeEnum.Great);

            var command = new MyCommandModel
            {
                Value = 30
            };
            Assert.DoesNotThrowAsync(async () => await dispatcher.ExecuteAsync(command));
            
        }
    }
}
