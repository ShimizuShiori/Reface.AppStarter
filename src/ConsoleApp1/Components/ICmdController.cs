using System;

namespace ConsoleApp1.Components
{
    public interface ICmdController
    {
        void ShowMessage(ConsoleColor color, string messageFormatter, params object[] args);
    }
}
