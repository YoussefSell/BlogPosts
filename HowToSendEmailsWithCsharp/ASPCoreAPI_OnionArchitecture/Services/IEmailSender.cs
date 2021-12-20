namespace ASPCoreAPI_OnionArchitecture.Services
{
    public interface IEmailSender
    {
        public Task SendUserResetPasswordEmailAsync(User user, string actionLink);
    }
}
