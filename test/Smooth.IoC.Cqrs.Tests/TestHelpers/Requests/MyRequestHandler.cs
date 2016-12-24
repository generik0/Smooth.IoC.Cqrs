using System.Threading.Tasks;
using Smooth.IoC.Cqrs.Requests;
using Smooth.IoC.Cqrs.Tests.TestHelpers.Requests;

namespace Smooth.IoC.Cqrs.Tests
{
    public class MyRequestHandler : Handler, IRequestHandler<MyRequestModel,MyReplyModel>
    {
        public MyRequestHandler(IHandlerFactory handlerFactory) : base(handlerFactory)
        {
        }

        public Task<MyReplyModel> ExecuteAsync(MyRequestModel request)
        {            
            return Task.FromResult(new MyReplyModel
            {
                Actual = request.Value +1
            });
        }

        public MyReplyModel Execute(MyRequestModel request)
        {
            return ExecuteAsync(request).Result;
        }
    }
}