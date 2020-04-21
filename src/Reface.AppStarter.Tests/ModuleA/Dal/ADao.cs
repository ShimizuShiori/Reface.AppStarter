using Reface.AppStarter.Attributes;

namespace Reface.AppStarter.Tests.ModuleA.Dal
{
    public interface IADao
    {
    }

    [Component]
    public class ADao : IADao { }
}
