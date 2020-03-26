using Reface.AppStarter.Attributes;
using System.Diagnostics;

namespace Mail
{
    [Component]
    public class DefaultMailSender : IMailSender
    {
        public void Send(string address, string title, string content)
        {
            Debug.WriteLine("邮件发送成功");
        }
    }
}
