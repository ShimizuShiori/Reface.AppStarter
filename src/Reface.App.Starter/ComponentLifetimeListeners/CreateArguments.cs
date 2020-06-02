using System;

namespace Reface.AppStarter.ComponentLifetimeListeners
{
    public class CreateArguments
    {
        /// <summary>
        /// 组件管理器
        /// </summary>
        public IComponentManager ComponentManager { get; private set; }

        /// <summary>
        /// 服务类型
        /// </summary>
        public Type ServiceType { get; private set; }

        public CreateArguments(IComponentManager componentManager, Type serviceType)
        {
            ComponentManager = componentManager;
            ServiceType = serviceType;
        }
    }
}
