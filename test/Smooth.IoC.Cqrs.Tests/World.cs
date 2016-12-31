using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smooth.IoC.Cqrs.Query;
using Smooth.IoC.Cqrs.Tests.TestHelpers.Queries;

namespace Smooth.IoC.Cqrs.Tests
{
    public class World : IWorld
    {
        private readonly IQueryDispatcher _dispatcher;
        private readonly IQueryDispatcher<MyQueryModel, MyResultModel> _specialDispatcher;
        private readonly IEnumerable<IHandler> _handlers;
        private readonly IQueryHandler<MyQueryModel, MyResultModel> _handler;
        private readonly IQuerySingleHandler<MyQueryModel, MyResultModel> _singleHandler;

        public World(IQueryDispatcher dispatcher, IQueryDispatcher<MyQueryModel, MyResultModel> specialDispatcher, IEnumerable<IHandler> handlers,
            IQueryHandler<MyQueryModel, MyResultModel> handler, IQuerySingleHandler<MyQueryModel, MyResultModel> singleHandler)
        {
            _dispatcher = dispatcher;
            _specialDispatcher = specialDispatcher;
            _handlers = handlers;
            _handler = handler;
            _singleHandler = singleHandler;
        }
        public async Task<MyResultModel> DoDispatch(MyQueryModel query)
        {
            var result2 = await _dispatcher.QueryAsync<MyQueryModel, MyResultModel>(query);
            return _dispatcher.QuerySingleOrDefaultAsync<MyQueryModel, MyResultModel>(new MyQueryModel
            {
                Value = result2.FirstOrDefault().Actual
            }).Result;
        }
        public async Task<MyResultModel> DoExactHandler(MyQueryModel query)
        {
            var result1 = await _specialDispatcher.QueryAsync();
            var result2 = await _specialDispatcher.QueryAsync(query);
            return _specialDispatcher.QuerySingleOrDefaultAsync(new MyQueryModel
            {
                Value = result1.FirstOrDefault().Actual + result2.FirstOrDefault().Actual
            }).Result;
        }
        public async Task<MyResultModel> DoSpecialDispatch(MyQueryModel query)
        {
            var decorate = _handlers.FirstOrDefault(x => x.IsHandel<MyQueryHandler>()) as MyQueryHandler;
            if (decorate == null) throw new NullReferenceException("MyqueryHandler is null");
            var result1 = await decorate.QueryAsync();
            var result2 = await decorate.QueryAsync(query);
            return decorate.QuerySingleOrDefaultAsync(new MyQueryModel
            {
                Value = result1.FirstOrDefault().Actual + result2.FirstOrDefault().Actual
            }).Result;
        }
        public async Task<MyResultModel> DoDecoratorDispatch(MyQueryModel query)
        {
            var result2 = await _handler.QueryAsync(query);
            return _singleHandler.QuerySingleOrDefaultAsync(new MyQueryModel
            {
                Value = result2.FirstOrDefault().Actual
            }).Result;
        }
    }
}

