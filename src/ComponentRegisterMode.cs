namespace Reface.AppStarter
{
    /// <summary>
    /// 元件注册模式
    /// </summary>
    public enum ComponentRegisterMode
    {
        AsSelf = 1,
        AsInterfaces = 2,
        All = AsSelf + AsInterfaces
    }
}
