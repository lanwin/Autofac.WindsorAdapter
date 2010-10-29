using System;
using System.Reflection;
using Castle.MicroKernel;
using Castle.Windsor;
using Castle.Windsor.Configuration;
using Castle.Windsor.Configuration.Interpreters;

namespace Autofac.WindsorAdapter
{
    public class WindsorAdapter
    {
        private readonly ComponentModelAdapter _componentModelAdapter;
        private readonly IContainer _container;
        private readonly IKernel _kernel;

        public WindsorAdapter(IKernel kernel, IContainer container)
        {
            if(kernel == null)
                throw new ArgumentNullException("kernel");
            if(container == null)
                throw new ArgumentNullException("container");

            _container = container;
            _componentModelAdapter = new ComponentModelAdapter(_container);
            _kernel = kernel;
            _kernel.ComponentRegistered += (key, handler) => _componentModelAdapter.Register(handler.ComponentModel, key);

            SetHandlerFactory();

            RegisterKernel();
        }

        private void RegisterKernel()
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(_kernel);
            builder.Update(_container);
        }

        private void SetHandlerFactory()
        {
            var handlerFactoryField = _kernel.GetType().GetField("handlerFactory", BindingFlags.Instance | BindingFlags.NonPublic);

            if(handlerFactoryField == null)
                throw new InvalidOperationException("Could not get handlerFactory field");

            handlerFactoryField.SetValue(_kernel, new AutofacHandlerFactory(_container));
        }

        public static void Adapt(IWindsorContainer windsor, IContainer container)
        {
            if(windsor == null)
                throw new ArgumentNullException("windsor");
            if(container == null)
                throw new ArgumentNullException("container");

            new WindsorAdapter(windsor.Kernel, container);
        }

        public static IWindsorContainer Create(IContainer container)
        {
            if(container == null)
                throw new ArgumentNullException("container");

            var windsor = new WindsorContainer();

            Adapt(windsor, container);

            return windsor;
        }

        public static IWindsorContainer Create(IContainer container, IConfigurationInterpreter interpreter)
        {
            if(container == null)
                throw new ArgumentNullException("container");
            if(interpreter == null)
                throw new ArgumentNullException("interpreter");

            var windsor = new WindsorContainer(interpreter);

            Adapt(windsor, container);

            return windsor;
        }
    }
}