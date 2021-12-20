namespace ASPCoreAPI_CQRS.Features.Emails.Commands.SendResetPasswordEmail
{
    public class SendResetPasswordCommand : IRequest
    {
        public SendResetPasswordCommand(User user, string actionLink)
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            ActionLink = actionLink ?? throw new ArgumentNullException(nameof(actionLink));
        }

        public User User { get; set; }
        public string ActionLink { get; set; }
    }
}
