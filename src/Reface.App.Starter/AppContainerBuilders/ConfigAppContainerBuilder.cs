using Newtonsoft.Json.Linq;
using Reface.AppStarter.AppContainers;
using Reface.AppStarter.AppModules;
using Reface.AppStarter.Attributes;
using Reface.AppStarter.ConfigRegistions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Reface.AppStarter.AppContainerBuilders
{
    /// <summary>
    /// 配置相关的 应用程序容器构建器
    /// </summary>
    public class ConfigAppContainerBuilder : BaseAppContainerBuilder
    {
        private readonly Dictionary<Type, IConfigRegistion> configTypeToRegistionMap = new Dictionary<Type, IConfigRegistion>();

        public void AutoConfig(AttributeAndTypeInfo attributeAndTypeInfo)
        {
            if (!(attributeAndTypeInfo.Attribute is ConfigAttribute)) return;

            this.configTypeToRegistionMap[attributeAndTypeInfo.Type] = new ScannedConfigRegistion(attributeAndTypeInfo);
        }

        public void AddConfig(IAppModule appModule, MethodInfo methodInfo)
        {
            Type returnType = methodInfo.ReturnType;
            ConfigCreatorAttribute attr = methodInfo.GetCustomAttribute<ConfigCreatorAttribute>();
            this.configTypeToRegistionMap[returnType] = new CreatorConfigRegistion(appModule, attr, methodInfo);
        }

        public override IAppContainer Build(AppSetup setup)
        {
            return new ConfigAppContainer(setup.ConfigFilePath, this.configTypeToRegistionMap.Values);
        }

        public override void Prepare(AppSetup setup)
        {
            AutofacContainerBuilder autofacContainerBuilder = setup.GetAppContainerBuilder<AutofacContainerBuilder>();
            autofacContainerBuilder.Building += AutofacContainerBuilder_Building;

        }

        private void AutofacContainerBuilder_Building(object sender, AppContainerBuilderBuildEventArgs e)
        {
            AutofacContainerBuilder autofacContainerBuilder = (AutofacContainerBuilder)sender;
            AppSetup setup = e.AppSetup;
            JObject jObject = null;
            if (File.Exists(setup.ConfigFilePath))
            {
                string json = File.ReadAllText(setup.ConfigFilePath);
                jObject = JObject.Parse(json);
            }

            this.configTypeToRegistionMap
                .ForEach(x =>
                {
                    string section = x.Value.Section;
                    JToken subObject = jObject?.GetValue(section);
                    object configValue;

                    if (subObject == null)
                        configValue = x.Value.CreateDefaultInstance();
                    else
                        configValue = subObject.ToObject(x.Key);

                    autofacContainerBuilder.RegisterInstance(configValue);
                });

            this.ConfigObjectRegisted?.Invoke(this, new AppContainerBuilderBuildEventArgs(setup));
        }

        public event EventHandler<AppContainerBuilderBuildEventArgs> ConfigObjectRegisted;
    }
}
