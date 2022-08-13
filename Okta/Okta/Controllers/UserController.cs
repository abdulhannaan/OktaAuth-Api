using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Okta.Services.User;
using OktaAuth.Models.User;
using System.Threading.Tasks;

namespace Okta.Controllers
{
    [Produces("application/json")]
    [Route("/user")]
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("sign-up")]
        [Route("sign-up")]
        public async Task<ActionResult> SignUp([FromBody] SignUpRequestModel request)
        {
            var response = await _userService.SignUp(request);
            return Ok(response);
        }
    }
}
