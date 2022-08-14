using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Okta.Services.Logging;
using Okta.Services.User;
using OktaAuth.Models.User;
using System.Threading.Tasks;

namespace FrontDeskID.Controllers
{
    [Produces("application/json")]
    [Route("/token")]
    public class TokenController : Controller
    {
        private IUserService _userService;
        private ILoggerManager _logger;
        public TokenController(IUserService userService, ILoggerManager logger)
        {
            _userService = userService;
            _logger = logger;
        }


        [AllowAnonymous]
        [HttpPost]
        [ActionName("create")]
        [Route("create")]
        public async Task<ActionResult> CreateToken([FromBody] UserLogin request)
        {
            _logger.LogInfo("Creating TOken");
            LoginResponseModel response = await _userService.Login(request);
            return Ok(response);
        }

    }
}
