using System.Collections.Generic;
using System.Threading.Tasks;
using Smooth.IoC.Cqrs.Query;

namespace Smooth.IoC.Cqrs.Tests.TestHelpers.Queries
{
    //Special Dispatcher
    public class MyQueryDispatcher : IQueryDispatcher<MyQueryModel, MyResultModel>
    {
        private readonly IQueryDispatcher _dispatcher;

        public MyQueryDispatcher(IQueryDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public Task<IEnumerable<MyResultModel>> QueryAsync()
        {
            return _dispatcher.QueryAsync<MyResultModel>();
        }

        public Task<IEnumerable<MyResultModel>> QueryAsync(MyQueryModel query)
        {
            return _dispatcher.QueryAsync<MyQueryModel, MyResultModel>(query);
        }

        public Task<MyResultModel> QuerySingleOrDefaultAsync(MyQueryModel query)
        {
            return _dispatcher.QuerySingleOrDefaultAsync<MyQueryModel, MyResultModel>(query);
        }

        public IEnumerable<MyResultModel> Query()
        {
            return _dispatcher.QueryAsync<MyResultModel>().Result;
        }

        public IEnumerable<MyResultModel> Query(MyQueryModel query)
        {
            return _dispatcher.QueryAsync<MyQueryModel, MyResultModel>(query).Result;
        }

        public MyResultModel QuerySingleOrDefault(MyQueryModel query)
        {
            return _dispatcher.QuerySingleOrDefaultAsync<MyQueryModel, MyResultModel>(query).Result;
        }
    }
}
