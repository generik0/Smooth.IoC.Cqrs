using System;

namespace Smooth.IoC.Cqrs
{
    public interface IHandle : IDisposable
    {
        bool IsHandel<THandle>() where THandle : class;
    }
}