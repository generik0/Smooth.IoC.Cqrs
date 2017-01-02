using System;

namespace Smooth.IoC.Cqrs.Exceptions
{
    public class QueryHandlerNotFoundException : Exception
    {
        public QueryHandlerNotFoundException(Type type) : base($"Could not create command handler of type: {type.Name}") {}

        public QueryHandlerNotFoundException(string message) : base(message) { }
    }
}
