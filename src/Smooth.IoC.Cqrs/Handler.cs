using System;

namespace Smooth.IoC.Cqrs
{
    public abstract class Handler :  IHandler
    {
        private readonly IHandlerFactory _handlerFactory;
        protected bool Disposed;

        protected Handler(IHandlerFactory handlerFactory)
        {
            _handlerFactory = handlerFactory;
        }

        ~Handler()
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
