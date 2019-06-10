using Newtonsoft.Json.Linq;
using Reface.AppStarter.AppContainers;
using Reface.AppStarter.Attributes;
using System.Collections.Generic;
using System.IO;

namespace Reface.AppStarter.AppContainerBuilders
{
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
            return new EmptyAppContainer();
        }

        public override void Prepare(AppSetup setup)
        {
            AutofacContainerBuilder autofacContainerBuilder
                = setup.GetAppContainerBuilder<AutofacContainerBuilder>();
            autofacContainerBuilder.Building += AutofacContainerBuilder_Building;
        }

        private void AutofacContainerBuilder_Building(object sender, AppContainerBuilderBuildEventArgs e)
        {
            var setup = e.AppSetup;
            if (!File.Exists(setup.ConfigFilePath)) return;
            string json = File.ReadAllText(setup.ConfigFilePath);
            JObject jObject = JObject.Parse(json);
            AutofacContainerBuilder autofacContainerBuilder = setup.GetAppContainerBuilder<AutofacContainerBuilder>();
            this.attributeAndTypeInfos
                .ForEach(x =>
                {
                    ConfigAttribute configAttribute = x.Attribute as ConfigAttribute;
                    object configValue = jObject
                        .GetValue(configAttribute.Section)
                        .ToObject(x.Type);
                    autofacContainerBuilder.RegisterInstance(configValue);
                });
        }
    }
}
