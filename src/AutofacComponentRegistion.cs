using Autofac;
using System;
using System.Reflection;

namespace Reface.AppStarter
{
    public class AutofacComponentRegistion : IComponentRegistion
    {
        private readonly ContainerBuilder containerBuilder;

        public AutofacComponentRegistion(ContainerBuilder containerBuilder)
        {
            this.containerBuilder = containerBuilder;
        }

        public bool TryRegisterByComponentAttribute(Type type, out TypeAndComponentInfo typeAndComponentInfo)
        {
            typeAndComponentInfo = null;
            if (!type.IsClass) return false;
            if (type.IsAbstract) return false;
            var att = type.GetCustomAttribute<ComponentAttribute>();
            if (att == null) return false;
            if (!type.IsGenericType)
            {
                var r = this.containerBuilder.RegisterType(type)
                        .InstancePerLifetimeScope();
                if (this.RegisterModeForInterfaces(att))
                    r.AsImplementedInterfaces();
                if (this.RegisterModeForSelf(att))
                    r.AsSelf();
            }
            else
            {
                var r = this.containerBuilder.RegisterGeneric(type)
                        .InstancePerLifetimeScope();
                if (this.RegisterModeForInterfaces(att))
                    r.AsImplementedInterfaces();
                if (this.RegisterModeForSelf(att))
                    r.AsSelf();
            }
            typeAndComponentInfo = new TypeAndComponentInfo(type, att);
            return true;
        }

        public void RegisterType<T>()
        {
            containerBuilder.RegisterType<T>().AsImplementedInterfaces().InstancePerLifetimeScope();
        }

        public void RegisterType(Type type)
        {
            if (type.IsGenericType)
                containerBuilder.RegisterGeneric(type).AsImplementedInterfaces().InstancePerLifetimeScope();
            else
                containerBuilder.RegisterType(type).AsImplementedInterfaces().InstancePerLifetimeScope();
        }



        private bool RegisterModeForInterfaces(ComponentAttribute componentAttribute)
        {
            return (componentAttribute.RegisterMode & ComponentRegisterMode.AsInterfaces) == ComponentRegisterMode.AsInterfaces;
        }

        private bool RegisterModeForSelf(ComponentAttribute componentAttribute)
        {
            return (componentAttribute.RegisterMode & ComponentRegisterMode.AsSelf) == ComponentRegisterMode.AsSelf;
        }

    }
}
