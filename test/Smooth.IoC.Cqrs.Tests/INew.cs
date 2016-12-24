using Smooth.IoC.Cqrs.Tests.TestHelpers.Requests;

namespace Smooth.IoC.Cqrs.Tests
{
    public interface INew
    {
        MyReplyeEnum DoDecoratorDispatch(MyRequestModel request);
        MyReplyeEnum DoDispatch(MyRequestModel request);
        MyReplyeEnum DoExactHandler(MyRequestModel request);
        MyReplyeEnum DoSpecialDispatch(MyRequestModel request);
    }
}