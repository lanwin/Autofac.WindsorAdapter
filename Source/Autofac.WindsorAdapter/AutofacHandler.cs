using System;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;

namespace Autofac.WindsorAdapter
{
    internal class AutofacHandler : IHandler
    {
        private readonly ComponentModel _model;
        private readonly IContainer _container;

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

        public bool CanResolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
        {
            throw new NotImplementedException();
        }

        public void Init(IKernel kernel)
        {
            throw new NotImplementedException();
        }

        public object Resolve(CreationContext context)
        {
            return _container.Resolve(_model.Service);
        }

        public object TryResolve(CreationContext context)
        {
            object service;
            _container.TryResolve(_model.Service, out service);
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
    }
}