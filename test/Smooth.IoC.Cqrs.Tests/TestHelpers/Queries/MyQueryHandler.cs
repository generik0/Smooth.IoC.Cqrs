using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smooth.IoC.Cqrs.Query;

namespace Smooth.IoC.Cqrs.Tests.TestHelpers.Queries
{
    public class MyQueryHandler : Handler, 
        IQueryHandler<MyQueryModel, MyResultModel>, 
        IQueryHandler< MyResultModel>, 
        IQuerySingleHandler<MyQueryModel, MyResultModel>,
        IQuerySingleHandler<MyResultModel>
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

        public Task<MyResultModel> QuerySingleOrDefaultAsync()
        {
            return Task.FromResult(new MyResultModel { Actual = 1 });
        }

        public IEnumerable<MyResultModel> Query()
        {
            return QueryAsync().Result;
        }

        public IEnumerable<MyResultModel> Query(MyQueryModel query)
        {
            return QueryAsync(query).Result;
        }

        public MyResultModel QuerySingleOrDefault(MyQueryModel query)
        {
            return QuerySingleOrDefaultAsync(query).Result;
        }

        
        public MyResultModel QuerySingleOrDefault()
        {
            throw new System.NotImplementedException();
        }
    }
}