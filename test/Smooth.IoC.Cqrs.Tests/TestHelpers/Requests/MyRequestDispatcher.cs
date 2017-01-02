using System.Threading.Tasks;
using Smooth.IoC.Cqrs.Requests;

namespace Smooth.IoC.Cqrs.Tests.TestHelpers.Requests
{
    //Special Dispatcher
    public class MyRequestDispatcher:IRequestDispatcher<MyRequestModel, MyReplyeEnum>
    {
        private readonly IRequestDispatcher _dispatcher;

        public MyRequestDispatcher(IRequestDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public Task<MyReplyeEnum> ExecuteAsync(MyRequestModel request)
        {
            return _dispatcher.ExecuteAsync<MyRequestModel, MyReplyeEnum>(request);
        }

        public MyReplyeEnum Execute(MyRequestModel request)
        {
            return ExecuteAsync(request).Result;
        }

        public Task<MyReplyeEnum> ExecuteAsync()
        {
            return _dispatcher.ExecuteAsync<MyReplyeEnum>();
        }

        public MyReplyeEnum Execute()
        {
            return ExecuteAsync().Result;
        }
    }
}
