﻿using Reface.AppStarter.Configs;
using Reface.AppStarter.JsonSchema;
using System.Collections.Generic;
using System.IO;

namespace Reface.AppStarter.AppContainers
{
    /// <summary>
    /// 所有配置类的容器，
    /// 该类的主要功能是生成 json-schema 文件
    /// </summary>
    public class ConfigAppContainer : BaseAppContainer
    {
        private readonly string configFilePath;
        private readonly IEnumerable<IConfigRegistion> registions;

        public ConfigAppContainer(string configFilePath, IEnumerable<IConfigRegistion> registions)
        {
            this.configFilePath = configFilePath;
            this.registions = registions;
        }

        public override void OnAppStarted(App app)
        {
            IComponentContainer componentContainer = app.GetAppContainer<IComponentContainer>();
            using (var scope = componentContainer.BeginScope("GenerateJsonSchema"))
            {
                AppConfig appConfig = scope.CreateComponent<AppConfig>();
                if (!appConfig.GenerateConfigJsonSchema) return;
                IJsonSchemaGenerator generator = scope.CreateComponent<IJsonSchemaGenerator>();
                string json = generator.Generate(this.registions);
                string path = this.configFilePath.Replace(".json", ".schema.json");
                File.WriteAllText(path, json);
            }
        }
    }
}
