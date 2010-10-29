using System;
using System.Collections;
using Castle.Core;

namespace Autofac.WindsorAdapter
{
    public class ComponentModelAdapter
    {
        private readonly IContainer _container;

        public ComponentModelAdapter(IContainer container)
        {
            if(container == null)
                throw new ArgumentNullException("container");
            _container = container;
        }

        public void Register(ComponentModel componentModel, string key)
        {
            if(componentModel == null)
                throw new ArgumentNullException("componentModel");
            if(key == null)
                throw new ArgumentNullException("key");

            var builder = new ContainerBuilder();

            var registration = builder.RegisterType(componentModel.Implementation)
                .As(componentModel.Service)
                .Named(key, componentModel.Service);

            foreach(DictionaryEntry property in componentModel.CustomDependencies)
            {
                var name = Convert.ToString(property.Key);
                registration.WithParameter(new NamedParameter(name, property.Value));
            }

            foreach(DictionaryEntry property in componentModel.ExtendedProperties)
            {
                var name = Convert.ToString(property.Key);
                registration.WithProperty(name, property.Value);
            }

            switch(componentModel.LifestyleType)
            {
                case LifestyleType.Transient:
                    registration.InstancePerDependency();
                    break;
                case LifestyleType.Undefined:
                case LifestyleType.Singleton:
                    registration.SingleInstance();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            builder.Update(_container);
        }
    }
}