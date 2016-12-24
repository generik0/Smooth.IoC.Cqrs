using Smooth.IoC.Cqrs.Requests;

namespace Smooth.IoC.Cqrs.Tests.TestHelpers.Requests
{
    public class MyRequestModel : Request
    {
        public int Value { get; set; }
    }
}