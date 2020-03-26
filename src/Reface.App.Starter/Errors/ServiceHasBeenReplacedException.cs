using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.AppStarter.Errors
{
    public class ServiceHasBeenReplacedException : Exception
    {
        public Type ServiceType { get; private set; }
        public Type AppModuleType { get; private set; }

        public ServiceHasBeenReplacedException(Type serviceType, Type appModuleType)
            : base($"服务 [ {serviceType} ] 已被模块 [ {appModuleType} ] 替换过 , 无法确保替换的顺序性，因此请手动移除多余的 [ReplaceCreator] 特征")
        {
            ServiceType = serviceType;
            AppModuleType = appModuleType;
        }
    }
}
