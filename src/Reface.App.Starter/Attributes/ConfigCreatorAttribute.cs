using Reface.AppStarter.AppModuleMethodHandlers;
using System;

namespace Reface.AppStarter.Attributes
{
    /// <summary>
    /// 允许开发者在 <see cref="AppModules"/> 通过该标签定义一个方法，
    /// 并由该方法返回一个用于配置的类型。
    /// 返回的类型与标记了 <see cref="ConfigAttribute"/> 的类具有相同的功能，
    /// 开发者可以通过这个标签创建那些第三方库中，无法添加自定义 <see cref="ConfigAttribute"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ConfigCreatorAttribute : AppModuleMethodAttribute
    {
        /// <summary>
        /// 在配置文件中的节点名称
        /// </summary>
        public string Section { get; set; }

        public override Type AppModuleMethodHandlerType => typeof(ConfigCreatorHandler);

        public ConfigCreatorAttribute(string section)
        {
            this.Section = section;
        }
    }
}
