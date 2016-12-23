using System.Collections.Generic;
using System.Threading.Tasks;
using Smooth.IoC.Cqrs.Query;
using Smooth.IoC.Cqrs.Requests;
using Smooth.IoC.Cqrs.Tests.TestHelpers.Queries;

namespace Smooth.IoC.Cqrs.Tests.TestHelpers.Requests
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
            return _dispatcher.QueryAsync<MyQueryModel, MyResultModel>();
        }

        public Task<IEnumerable<MyResultModel>> QueryAsync(MyQueryModel query)
        {
            return _dispatcher.QueryAsync<MyQueryModel, MyResultModel>(query);
        }

        public Task<MyResultModel> QuerySingleOrDefaultAsync(MyQueryModel query)
        {
            return _dispatcher.QuerySingleOrDefaultAsync<MyQueryModel, MyResultModel>(query);
        }
    }
}
