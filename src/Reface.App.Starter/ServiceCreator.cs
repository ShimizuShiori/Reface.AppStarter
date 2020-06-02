using System;

namespace Reface.AppStarter
{
    /// <summary>
    /// 创建 Reface 内部服务的组件
    /// </summary>
    public class ServiceCreator
    {
        public Type ImplementServiceType { get; private set; }
        public Func<Object> Creator { get; private set; }

        private ServiceCreator(Type implementServiceType, Func<object> creator)
        {
            ImplementServiceType = implementServiceType;
            Creator = creator;
        }

        public static ServiceCreator Create<T>(Func<T> creator)
        {
            return new ServiceCreator(typeof(T), () => creator());
        }

        public static ServiceCreator Create<T>() where T : new()
        {
            return ServiceCreator.Create<T>(() => new T());
        }

        public static ServiceCreator Create(Type type, Func<object> creator)
        {
            return new ServiceCreator(type, creator);
        }

        public static ServiceCreator Create(Type type)
        {
            return ServiceCreator.Create(type, () => Activator.CreateInstance(type));
        }
    }
}
