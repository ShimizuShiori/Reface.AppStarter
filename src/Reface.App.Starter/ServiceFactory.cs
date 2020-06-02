using Reface.AppStarter.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reface.AppStarter
{
    /// <summary>
    /// 为 Reface.AppStarter 中各种组件提供服务的工厂
    /// </summary>
    public static class ServiceFactory
    {

        private readonly static Dictionary<Type, List<ServiceCreator>> factories = new Dictionary<Type, List<ServiceCreator>>();

        static ServiceFactory()
        {
            AutoRegister();
        }

        static void AutoRegister()
        {
            Assembly assembly = typeof(ServiceFactory).Assembly;
            foreach (var type in assembly.GetTypes())
            {
                RegisterAsAttribute attribute = type.GetCustomAttribute<RegisterAsAttribute>();
                if (attribute == null) continue;

                ServiceFactory.RegisterService(attribute.ServiceType, type);
            }
        }

        /// <summary>
        /// 注册一个服务
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="creator"></param>
        public static void RegisterService(Type serviceType, ServiceCreator creator)
        {
            List<ServiceCreator> creators;
            if (!factories.TryGetValue(serviceType, out creators))
            {
                creators = new List<ServiceCreator>();
                factories[serviceType] = creators;
            }
            creators.Add(creator);
        }

        /// <summary>
        /// 注册一个服务
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="implementType"></param>
        public static void RegisterService(Type serviceType, Type implementType)
        {
            ServiceFactory.RegisterService(serviceType, ServiceCreator.Create(implementType));
        }

        /// <summary>
        /// 注册一个服务
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplement"></typeparam>
        public static void RegisterService<TService, TImplement>()
            where TImplement : TService, new()
        {
            RegisterService(typeof(TService), ServiceCreator.Create<TImplement>());
        }

        /// <summary>
        /// 替换已有的服务
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplement"></typeparam>
        public static void ReplaceService<TService, TImplement>()
            where TImplement : new()
        {
            List<ServiceCreator> creators = new List<ServiceCreator>();
            creators.Add(ServiceCreator.Create<TImplement>());
            factories[typeof(TService)] = creators;
        }

        /// <summary>
        /// 获取一个服务，当集合内有多个相同的服务，会抛出异常。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }

        public static object GetService(Type serviceType)
        {
            List<ServiceCreator> creators;
            if (!factories.TryGetValue(serviceType, out creators))
                throw new KeyNotFoundException("未注册的组件 : " + serviceType.FullName);

            if (creators.Count() > 1)
                throw new IndexOutOfRangeException("注册有多个组件，请使用 GetServices");

            var instance = creators.First().Creator();

            AutoSetProperties(instance);

            return instance;
        }

        public static IEnumerable<object> GetServices(Type serviceType)
        {
            List<ServiceCreator> creators;
            if (!factories.TryGetValue(serviceType, out creators))
                throw new KeyNotFoundException($"未注册的组件 : {serviceType.FullName}");
            IEnumerable<object> result = creators.Select(x => x.Creator());
            foreach (var item in result)
                AutoSetProperties(item);

            return result;
        }

        /// <summary>
        /// 获取一组服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GetServices<T>()
        {
            return GetServices(typeof(T)).Select(x => (T)x);
        }

        private static void AutoSetProperties(Object instance)
        {
            foreach (var property in instance.GetType().GetProperties())
            {
                if (!property.CanWrite) continue;
                if (property.GetCustomAttribute<AutoSetAttribute>() == null) continue;

                property.SetValue(instance, GetService(property.PropertyType));
            }
        }
    }
}
