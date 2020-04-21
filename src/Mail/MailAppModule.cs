using Reface.AppStarter;
using Reface.AppStarter.AppModules;
using System;

namespace Mail
{
    [ComponentScanAppModule]
    public class MailAppModule : AppModule
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
