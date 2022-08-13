using OktaAuth.Helper;
using OktaAuth.Models.Config;
using OktaAuth.Models.User;
using System.Threading.Tasks;

namespace Okta.Services.User
{
    public class UserService : IUserService
    {
        private readonly ApiConfig _apiConfig;
        private readonly OktaApiHelper _oktaApiHelper;

        public UserService(ApiConfig apiConfig, OktaApiHelper oktaApiHelper)
        {
            _apiConfig = apiConfig;
            _oktaApiHelper = oktaApiHelper;
        }

        public async Task<LoginResponseModel> Login(UserLogin login)
        {
            return await _oktaApiHelper.PrimaryAuthentication(login);
        }

        public async Task<SignUpResponseModel> SignUp(SignUpRequestModel signup)
        {
            return await _oktaApiHelper.CreateUserWithPassword(signup);
        }
    }
}
