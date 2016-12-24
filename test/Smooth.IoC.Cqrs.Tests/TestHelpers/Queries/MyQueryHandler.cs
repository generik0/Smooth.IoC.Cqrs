using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smooth.IoC.Cqrs.Query;

namespace Smooth.IoC.Cqrs.Tests.TestHelpers.Queries
{
    public class MyQueryHandler : Handler, IQueryHandler<MyQueryModel, MyResultModel>
    {
        public MyQueryHandler(IHandlerFactory handlerFactory) : base(handlerFactory)
        {
        }

        public Task<IEnumerable<MyResultModel>> QueryAsync()
        {
            return Task.FromResult(new[] {new MyResultModel {Actual = 1},}.AsEnumerable());
        }

        public Task<IEnumerable<MyResultModel>> QueryAsync(MyQueryModel query)
        {
            var myResultModels = new[] {new MyResultModel {Actual = query.Value + 1}};
            return Task.FromResult(myResultModels.AsEnumerable());
        }

        public Task<MyResultModel> QuerySingleOrDefaultAsync(MyQueryModel query)
        {
            return Task.FromResult(new MyResultModel {Actual = query.Value + 1});
        }
    }
}