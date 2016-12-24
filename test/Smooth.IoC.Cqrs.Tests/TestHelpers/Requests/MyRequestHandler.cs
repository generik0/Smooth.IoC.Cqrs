using System.Threading.Tasks;
using Smooth.IoC.Cqrs.Requests;

namespace Smooth.IoC.Cqrs.Tests.TestHelpers.Requests
{
    public class MyRequestHandler : Handler, IRequestHandler<MyRequestModel,MyReplyeEnum>
    {
        public MyRequestHandler(IHandlerFactory handlerFactory) : base(handlerFactory)
        {
        }

        public Task<MyReplyeEnum> ExecuteAsync(MyRequestModel request)
        {            
            return Task.FromResult(MyReplyeEnum.Great);
        }

        public MyReplyeEnum Execute(MyRequestModel request)
        {
            return ExecuteAsync(request).Result;
        }
    }
}