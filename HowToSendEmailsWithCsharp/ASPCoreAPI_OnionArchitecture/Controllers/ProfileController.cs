namespace ASPCoreAPI_OnionArchitecture.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IEmailSender _emailSender;

        public ProfileController(ILogger<ProfileController> logger, IEmailSender emailSender, IUserRepository userRepository)
        {
            _logger = logger;
            _emailSender = emailSender;
            _userRepository = userRepository;
        }

        [HttpGet(Name = "ResetPassword/{email}")]
        public async Task<IActionResult> GetAsync([FromRoute(Name = "email")] string email)
        {
            var user = await _userRepository.FindByEmailAsync(email);
            if (user is not null)
            {
                await _emailSender.SendUserResetPasswordEmailAsync(user, "https://example.com/reset-password");
            }

            return Ok();
        }
    }
}