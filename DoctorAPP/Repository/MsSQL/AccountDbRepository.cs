using DoctorAPP.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace DoctorAPP.Repository.MsSQL
{
    public class AccountDbRepository : IAccountRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public AccountDbRepository(IConfiguration configuration)
        {
           var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            _httpClient = new HttpClient(httpClientHandler);
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri("http://localhost:5185");
        }

        public async Task<string> SignInUser(LoginUserViewModel loginUserViewModel)
        {
            var newJsonAsString = JsonConvert.SerializeObject(loginUserViewModel);
            var requestBody = new StringContent(newJsonAsString, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Add("ApiKey", _configuration.GetValue<string>("ApiKey"));
            var response = await _httpClient.PostAsync("/Login", requestBody);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var token = JObject.Parse(content)["token"].ToString();

                return token;
            }

            return null;
        }

        public async Task<bool> SignUpUser(RegisterUserViewModel user)
        {
            var newJsonAsString = JsonConvert.SerializeObject(user);
            var requestBody = new StringContent(newJsonAsString, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Add("ApiKey", _configuration.GetValue<string>("ApiKey"));
            var response = await _httpClient.PostAsync("/Signup", requestBody);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return true;
            }

            return false;
        }
    }
}
