using Autofac;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Reface.AppStarter
{
    public class DefaultConfigRegistion : IConfigRegistion
    {
        private readonly ContainerBuilder containerBuilder;
        private readonly JObject jObject;
        private readonly HashSet<string> registedType = new HashSet<string>();

        public DefaultConfigRegistion(ContainerBuilder containerBuilder, JObject jObject)
        {
            this.containerBuilder = containerBuilder;
            this.jObject = jObject;
        }

        public void Register(IModule module)
        {
            if (module.ConfigType == null) return;

            if (registedType.Contains(module.ConfigType.FullName))
                return;

            string configName = module.ConfigType.Name;
            ConfigAttribute configAttribute = module.ConfigType.GetCustomAttribute<ConfigAttribute>();
            if (configAttribute == null && configName.EndsWith("Config"))
                configName = configName.Substring(0, configName.Length - 6);
            else
                configName = configAttribute.Name;
            Object configObject = null;
            if (jObject != null)
            {
                JToken jToken = jObject.GetValue(configName);
                if (jToken != null)
                {
                    configObject = jToken.ToObject(module.ConfigType);
                }
            }
            if (configObject == null)
            {
                configObject = Activator.CreateInstance(module.ConfigType);
            }
            this.containerBuilder
                .RegisterInstance(configObject)
                .AsSelf()
                .SingleInstance()
                .IfNotRegistered(module.ConfigType);

            registedType.Add(module.ConfigType.FullName);
        }
    }
}
