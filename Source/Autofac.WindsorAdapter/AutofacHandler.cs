using System;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;

namespace Autofac.WindsorAdapter
{
    internal class AutofacHandler : IHandler
    {
        private readonly IContainer _container;
        private readonly ComponentModel _model;

        public AutofacHandler(ComponentModel model, IContainer container)
        {
            if(model == null)
                throw new ArgumentNullException("model");
            if(container == null)
                throw new ArgumentNullException("container");
            _model = model;
            _container = container;
            Service = _model.Service;
        }

        public AutofacHandler(ComponentModel model, IContainer container, Type service)
        {
            if(model == null)
                throw new ArgumentNullException("model");
            if(container == null)
                throw new ArgumentNullException("container");
            _model = model;
            _container = container;
            Service = service;
        }

        public object Resolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
        {
            throw new NotImplementedException();
        }

        public bool CanResolve(CreationContext context,
            ISubDependencyResolver contextHandlerResolver,
            ComponentModel model,
            DependencyModel dependency)
        {
            throw new NotImplementedException();
        }

        public void Init(IKernel kernel)
        {
            throw new NotImplementedException();
        }

        public object Resolve(CreationContext context)
        {
            return _container.Resolve(PrepareServiceType(context));
        }

        public object TryResolve(CreationContext context)
        {
            object service;
            _container.TryResolve(PrepareServiceType(context), out service);
            return service;
        }

        public bool Release(object instance)
        {
            throw new NotImplementedException();
        }

        public void AddCustomDependencyValue(object key, object value)
        {
            throw new NotImplementedException();
        }

        public void RemoveCustomDependencyValue(object key)
        {
            throw new NotImplementedException();
        }

        public bool HasCustomParameter(object key)
        {
            throw new NotImplementedException();
        }

        public bool IsBeingResolvedInContext(CreationContext context)
        {
            throw new NotImplementedException();
        }

        public HandlerState CurrentState
        {
            get { throw new NotImplementedException(); }
        }

        public ComponentModel ComponentModel
        {
            get { return _model; }
        }

        public Type Service { get; private set; }

        public event HandlerStateDelegate OnHandlerStateChanged;

        private Type PrepareServiceType(CreationContext context)
        {
            var service = _model.Service;
            if(context.GenericArguments != null && context.GenericArguments.Length > 0)
                service = service.MakeGenericType(context.GenericArguments);
            return service;
        }
    }
}