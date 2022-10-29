namespace CleanArchitecture.Core.Interfaces
{
    public interface IMessageSender
    {
        void SendGuestBookNotificationEmail(string toAddress, string messageBody);
    }
}
