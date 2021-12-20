namespace ASPCoreAPI_OnionArchitecture.Services
{
    

    public class EmailSender : IEmailSender
    {
        private readonly IEmailService _emailService;

        public EmailSender(IEmailService emailService)
            => _emailService = emailService;

        public async Task SendUserResetPasswordEmailAsync(User user, string actionLink)
        {
            var message = Message.Compose()
                .To(user.Email)
                .WithSubject("Password reset")
                .WithHtmlContent($@"
<h1>Hello{user.FullName}</h1>
<p>click <a href='{actionLink}'>here</a> to reset your password </p>"
                )
                .Build();

            _ = await _emailService.SendAsync(message);
        }
    }
}
