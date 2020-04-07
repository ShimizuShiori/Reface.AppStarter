using Reface.AppStarter;
using Reface.AppStarter.AppModules;
using System;

namespace ClassLibrary1
{
    [ComponentScanAppModule]
    public class CL1AppModule : AppModule
    {
        public override void OnUsing(AppSetup setup, IAppModule targetModule)
        {
            var infos = setup.GetScanResult(targetModule);
            foreach (var info in infos.ScannableAttributeAndTypeInfos)
            {
                Console.WriteLine($"{this.GetType()} : {info.Type}");
            }
        }
    }
}
