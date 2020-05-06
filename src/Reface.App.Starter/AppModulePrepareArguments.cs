using Reface.AppStarter.Attributes;

namespace Reface.AppStarter
{
    /// <summary>
    /// <see cref="AppModulePrepairAttribute.Prepair(AppModulePrepareArguments)"/> 的参数
    /// </summary>
    public class AppModulePrepareArguments
    {
        public AppSetup AppSetup { get; private set; }

        public AppModulePrepareArguments(AppSetup appSetup)
        {
            AppSetup = appSetup;
        }
    }
}
