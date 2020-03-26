namespace Mail
{
    public interface IMailSender
    {
        void Send(string address, string title, string content);
    }
}
