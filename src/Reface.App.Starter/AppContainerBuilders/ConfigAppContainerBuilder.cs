using Newtonsoft.Json.Linq;
using Reface.AppStarter.AppContainers;
using Reface.AppStarter.Attributes;
using System;
using System.Collections.Generic;
using System.IO;

namespace Reface.AppStarter.AppContainerBuilders
{
    /// <summary>
    /// 配置相关的 应用程序容器构建器
    /// </summary>
    public class ConfigAppContainerBuilder : BaseAppContainerBuilder
    {
        private readonly IList<AttributeAndTypeInfo> attributeAndTypeInfos = new List<AttributeAndTypeInfo>();

        public void AutoConfig(AttributeAndTypeInfo attributeAndTypeInfo)
        {
            if (!(attributeAndTypeInfo.Attribute is ConfigAttribute)) return;
            this.attributeAndTypeInfos.Add(attributeAndTypeInfo);
        }

        public override IAppContainer Build(AppSetup setup)
        {
            return new ConfigAppContainer(setup.ConfigFilePath, this.attributeAndTypeInfos);
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

            this.attributeAndTypeInfos
                .ForEach(x =>
                {
                    ConfigAttribute configAttribute = x.Attribute as ConfigAttribute;
                    JToken subObject = jObject?.GetValue(configAttribute.Section);
                    object configValue;

                    if (subObject == null)
                        configValue = System.Activator.CreateInstance(x.Type);
                    else
                        configValue = subObject.ToObject(x.Type);

                    autofacContainerBuilder.RegisterInstance(configValue);
                });
            this.ConfigObjectRegisted?.Invoke(this, new AppContainerBuilderBuildEventArgs(setup));
        }

        public event EventHandler<AppContainerBuilderBuildEventArgs> ConfigObjectRegisted;
    }
}
