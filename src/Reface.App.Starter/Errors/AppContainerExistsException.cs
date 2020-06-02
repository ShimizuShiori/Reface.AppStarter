using Reface.AppStarter.AppContainers;
using System;
using System.Runtime.Serialization;

namespace Reface.AppStarter.Errors
{
    /// <summary>
    /// 指定的 <see cref="IAppContainer"/> 已在 <see cref="App"/> 中存在了
    /// </summary>
    public class AppContainerExistsException : Exception
    {
        public AppContainerExistsException()
        {
        }

        public AppContainerExistsException(string message) : base(message)
        {
        }

        public AppContainerExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AppContainerExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
