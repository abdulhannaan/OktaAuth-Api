using OktaAuth.Models.User;
using System.Threading.Tasks;

namespace Okta.Services.User
{
    public interface IUserService
    {
        Task<LoginResponseModel> Login(UserLogin login);

        Task<SignUpResponseModel> SignUp(SignUpRequestModel signup);
        Task ClearSession(string userId);
    }
}
