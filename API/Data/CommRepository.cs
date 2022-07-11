namespace API.Data
{
    public class CommRepository: ICommRepository
    {
        private readonly IEmailSender _sender;
        public CommRepository(IEmailSender sender)
        {
              _sender = sender;
        }

        public async Task<bool> SendSms(EmailFormDto email)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SendEmail(EmailFormDto email)
        {
            //  var response= await _sender.SendEmailAsync(email.ToEmail,email.Subject,email.Content).ConfigureAwait(false);
            //  if(response.StatusCode===200)   return true;
            //  return false;
            throw new NotImplementedException();
        }
    }
}