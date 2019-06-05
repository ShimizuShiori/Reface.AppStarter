using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.AppStarter
{
    public interface IComponentFactory : IDisposable
    {
        T Create<T>();
        object Create(Type type);
        IComponentFactory BeginScope(string name = "");
        void InitProperties(object obj);
    }
}
