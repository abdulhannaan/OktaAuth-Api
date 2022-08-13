using Newtonsoft.Json;
using OktaAuth.Models.Config;
using OktaAuth.Models.User;
using OktaModels.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OktaAuth.Helper
{
    public class OktaApiHelper: ApiHelper
    {
        private readonly ApiConfig _config;

        private readonly HttpClient _client;
        public OktaApiHelper(ApiConfig config, HttpClient client) : base(config.ApiUrl, client)
        {
            _client = client;
            _config = config;
        }

        public void SetHeaders(bool isAuthorize = true)
        {
            _client.Timeout = TimeSpan.FromMinutes(5);
            _client.BaseAddress = new Uri(_config.ApiUrl);
            if(isAuthorize)
                _client.DefaultRequestHeaders.Add("Authorization", "SSWS "+ _config.ApiKey);
        }

        public async Task<LoginResponseModel> PrimaryAuthentication(UserLogin loginInfo)
        {
            SetHeaders(false);
            var response = await Post<string>("api/v1/authn", loginInfo);
            return JsonConvert.DeserializeObject<LoginResponseModel>(response);
        }
        
        public async Task<SignUpResponseModel> CreateUserWithPassword(SignUpRequestModel signup)
        {
            SetHeaders();
            var response = await Post<string>("api/v1/users", signup);
            return JsonConvert.DeserializeObject<SignUpResponseModel>(response);
        }
    }
}
