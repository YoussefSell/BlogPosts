namespace ASPCoreAPI_CQRS.Features.Emails.Commands.SendResetPasswordEmail
{
    public class SendResetPasswordCommandHandler : IRequestHandler<SendResetPasswordCommand>
    {
        private readonly IEmailService _emailService;

        public SendResetPasswordCommandHandler(IEmailService emailService) 
            => _emailService = emailService;

        public async Task<Unit> Handle(SendResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var message = Message.Compose()
                .To(request.User.Email)
                .WithSubject("Password reset")
                .WithHtmlContent($@"
<h1>Hello{request.User.FullName}</h1>
<p>click <a href='{request.ActionLink}'>here</a> to reset your password </p>"
                )
                .Build();

            _ = await _emailService.SendAsync(message);

            return Unit.Value;
        }
    }
}
