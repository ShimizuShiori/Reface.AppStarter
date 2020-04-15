using System;

namespace Reface.AppStarter
{
    /// <summary>
    /// 向容器注册配置类的注册器
    /// </summary>
    public interface IConfigRegistion
    {
        Type Type { get; }
        string Section { get; }

        object CreateDefaultInstance();
    }
}
