namespace API.Interfaces
{
    public interface ICommRepository
    {
        Task<bool>SendEmail(EmailFormDto email);
         Task<bool> SendSms(EmailFormDto email);
         
    }
}