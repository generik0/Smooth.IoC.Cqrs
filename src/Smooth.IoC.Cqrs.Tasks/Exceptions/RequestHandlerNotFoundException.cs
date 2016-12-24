using System;

namespace Smooth.IoC.Cqrs.Tasks.Exceptions
{
    public class RequestHandlerNotFoundException : Exception
    {
        public RequestHandlerNotFoundException(Type type) : base($"Could not create request handler of type: {type.Name}") {}
    }
}
