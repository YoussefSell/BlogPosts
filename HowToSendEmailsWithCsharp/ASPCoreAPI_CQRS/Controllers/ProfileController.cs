namespace ASPCoreAPI_CQRS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;

        public ProfileController(ILogger<ProfileController> logger, IMediator mediator, IUserRepository userRepository)
        {
            _logger = logger;
            _mediator = mediator;
            _userRepository = userRepository;
        }

        [HttpGet(Name = "ResetPassword/{email}")]
        public async Task<IActionResult> GetAsync([FromRoute(Name = "email")] string email)
        {
            var user = await _userRepository.FindByEmailAsync(email);
            if (user is not null)
            {
                await _mediator.Send(new SendResetPasswordCommand(user, "https://example.com/reset-password"));
            }

            return Ok();
        }
    }
}