namespace Reface.AppStarter.Attributes
{
    /// <summary>
    /// 组件以何种方式注册到容器 <see cref="Reface.AppStarter.AppContainerBuilders.AutofacContainerBuilder"/> 中
    /// </summary>
    public enum RegistionMode
    {
        /// <summary>
        /// 不注册
        /// </summary>
        No = 0,
        /// <summary>
        /// 以实现的接口注册
        /// </summary>
        AsInterfaces = 1,
        /// <summary>
        /// 以自身的类型注册
        /// </summary>
        AsSelf = 2,
        /// <summary>
        /// 同时以接口和自身类型注册
        /// </summary>
        Both = AsInterfaces | AsSelf
    }
}
