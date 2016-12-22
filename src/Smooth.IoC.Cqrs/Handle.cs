using System;

namespace Smooth.IoC.Cqrs
{
    public abstract class Handle :  IHandle
    {
        private readonly IHandlerFactory _handlerFactory;
        protected bool Disposed;

        protected Handle(IHandlerFactory handlerFactory)
        {
            _handlerFactory = handlerFactory;
        }

        ~Handle()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (Disposed || !disposing) return;
            Disposed = true;
            _handlerFactory.Release(this);
        }

        public bool IsHandel<THandle>() where THandle : class
        {
            return typeof(THandle) == GetType();
        }
    }
}
