using Reface.AppStarter.AppModules;
using Reface.AppStarter.Attributes;
using System;
using System.Reflection;

namespace Reface.AppStarter.ConfigRegistions
{
    /// <summary>
    /// 基于 <see cref="ConfigCreatorAttribute"/> 进行注册的注册器
    /// </summary>
    public class CreatorConfigRegistion : IConfigRegistion
    {
        private readonly IAppModule appModule;
        private readonly ConfigCreatorAttribute attribute;
        private readonly MethodInfo method;

        public CreatorConfigRegistion(IAppModule appModule, ConfigCreatorAttribute attribute, MethodInfo method)
        {
            this.appModule = appModule;
            this.attribute = attribute;
            this.method = method;
        }

        public string Section => this.attribute.Section;

        public Type Type => this.method.ReturnType;

        public object CreateDefaultInstance()
        {
            return this.method.Invoke(this.appModule, null);
        }
    }
}
