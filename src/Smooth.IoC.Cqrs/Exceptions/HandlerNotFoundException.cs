using System;

namespace Smooth.IoC.Cqrs.Exceptions
{
    public class HandlerNotFoundException : Exception
    {
        public HandlerNotFoundException(string message) : base(message) { }
        public HandlerNotFoundException(Type type) : base($"Could not create request handler of type: {type.Name}") {}
    }
}
