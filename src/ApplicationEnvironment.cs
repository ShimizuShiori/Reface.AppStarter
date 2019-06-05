using Autofac;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Reface.AppStarter
{
    /// <summary>
    /// 应用程序环境
    /// </summary>
    public class ApplicationEnvironment
    {
        /// <summary>
        /// 组件容器
        /// </summary>
        public IContainer Container { get; private set; }

        public IComponentFactory ComponentFactory { get; private set; }
        
        /// <summary>
        /// 运行时的上下文对象
        /// </summary>
        public Dictionary<string, object> Context { get; private set; } = new Dictionary<string, object>();

        /// <summary>
        /// 不允许外构造
        /// </summary>
        /// <param name="setup"></param>
        internal ApplicationEnvironment()
        {
        }

        /// <summary>
        /// 安装环境
        /// </summary>
        /// <param name="setup"></param>
        internal void Setup(ApplicationEnvironmentSetup setup)
        {
            this.Container = setup.ContainerBuilder.Build();
            this.ComponentFactory = new ContainerComponentFactory(this.Container);
        }
    }
}
