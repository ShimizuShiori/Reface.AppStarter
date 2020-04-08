using System;

namespace Reface.AppStarter.Errors
{
    /// <summary>
    /// 当一个类型无法通过 <see cref="JsonSchema.JsonTypeHelper"/> 中的方法定位到一个 JsonType 时抛出的异常。
    /// </summary>
    public class CanNotConvertToJsonTypeException : Exception
    {
        /// <summary>
        /// 未能正确映射为 JsonType 的类型
        /// </summary>
        public Type Type { get; private set; }
        public CanNotConvertToJsonTypeException(Type type)
            : base($"无法生成对应的 Json 类型 : {type.FullName}")
        {
            this.Type = type;
        }
    }
}
