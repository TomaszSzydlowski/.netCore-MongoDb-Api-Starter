using System.Threading.Tasks;
using netCoreMongoDbApi.Resources;
using Microsoft.AspNetCore.Mvc;
using netCoreMongoDbApi.Domain.Services;
using AutoMapper;

namespace netCoreMongoDbApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private const string Authorization = "Authorization";
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public AuthController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        // POST: api/auth/register
        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ErrorResource), 400)]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] SaveUserRegisterResource saveRegisterUserResource)
        {
            var registerResult = await _authService.Register(saveRegisterUserResource);

            if (!registerResult.Success)
                return BadRequest(new ErrorResource(registerResult.Message));

            var loginUserResource = _mapper.Map<SaveUserRegisterResource, LoginUserResource>(saveRegisterUserResource);

            var loginResult = await _authService.Login(loginUserResource);

            if (!loginResult.Success)
                return BadRequest(new ErrorResource(loginResult.Message));

            Response.Headers.Add(Authorization, loginResult.Token);
            return Ok();

        }

        // POST: api/auth/login
        [HttpPost("login")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ErrorResource), 401)]
        public async Task<IActionResult> Login([FromBody] LoginUserResource loginUserResource)
        {
            var result = await _authService.Login(loginUserResource);

            if (result.User == null)
                return Unauthorized(new ErrorResource(result.Message));

            if (!result.Success)
                return BadRequest(new ErrorResource(result.Message));

            return Ok(result.Token);
        }
    }
}