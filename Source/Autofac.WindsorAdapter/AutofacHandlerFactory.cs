using System;
using Castle.Core;
using Castle.MicroKernel;

namespace Autofac.WindsorAdapter
{
    internal class AutofacHandlerFactory : IHandlerFactory
    {
        private readonly IContainer _container;

        public AutofacHandlerFactory(IContainer container)
        {
            _container = container;
        }

        public IHandler Create(ComponentModel model)
        {
            return new AutofacHandler(model, _container);
        }

        public IHandler CreateForwarding(IHandler target, Type forwardedType)
        {
            return new AutofacHandler(target.ComponentModel, _container, forwardedType);
        }
    }
}