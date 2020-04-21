using Reface.AppStarter;
using Reface.AppStarter.AppModules;
using System;

namespace ClassLibrary1
{
    [ComponentScanAppModule]
    public class CL1AppModule : AppModule
    {
        public override void OnUsing(AppModuleUsingArguments args)
        {
            foreach (var info in args.ScannedAttributeAndTypeInfos)
            {
                Console.WriteLine($"{this.GetType()} : {info.Type}");
            }
        }
    }
}
