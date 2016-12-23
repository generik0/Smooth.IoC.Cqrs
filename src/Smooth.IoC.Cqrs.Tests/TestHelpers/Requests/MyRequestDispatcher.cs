using System.Threading.Tasks;
using Smooth.IoC.Cqrs.Requests;

namespace Smooth.IoC.Cqrs.Tests.TestHelpers.Requests
{
    //Special Dispatcher
    public class MyRequestDispatcher:IRequestDispatcher<MyRequestModel, MyReplyModel>
    {
        private readonly IRequestDispatcher _dispatcher;

        public MyRequestDispatcher(IRequestDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public Task<MyReplyModel> ExecuteAsync(MyRequestModel request)
        {
            return _dispatcher.ExecuteAsync<MyRequestModel, MyReplyModel>(request);
        }
    }
}
