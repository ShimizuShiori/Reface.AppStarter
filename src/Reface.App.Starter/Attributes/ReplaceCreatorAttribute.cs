using Reface.AppStarter.AppModuleMethodHandlers;
using System;

namespace Reface.AppStarter.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ReplaceCreatorAttribute : AppModuleMethodAttribute
    {
        public override Type AppModuleMethodHandlerType => typeof(ComponentReplaceHandler);
    }
}
