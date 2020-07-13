using Reface.AppStarter.Attributes;
using System;

namespace ConsoleApp1.Components
{
    [Component]
    public class DefaultCmdController : ICmdController
    {
        public void ShowMessage(ConsoleColor color, string messageFormatter, params object[] args)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(messageFormatter, args);
            Console.ForegroundColor = oldColor;
        }
    }
}
