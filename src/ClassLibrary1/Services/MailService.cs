using Mail;
using System.Diagnostics;

namespace ClassLibrary1.Services
{
    public class MailService : IMailSender
    {
        public void Send(string address, string title, string content)
        {
            Debug.WriteLine("use MailService");
        }
    }
}
