using Smooth.IoC.Cqrs.Tests.TestHelpers.Requests;

namespace Smooth.IoC.Cqrs.Tests
{
    public interface INew
    {
        MyReplyModel DoDecoratorDispatch(MyRequestModel request);
        MyReplyModel DoDispatch(MyRequestModel request);
        MyReplyModel DoExactHandler(MyRequestModel request);
        MyReplyModel DoSpecialDispatch(MyRequestModel request);
    }
}