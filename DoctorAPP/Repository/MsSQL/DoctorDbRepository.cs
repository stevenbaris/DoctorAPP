using DoctorAPP.Models;
using Newtonsoft.Json;
using System.Text;

namespace DoctorAPP.Repository.MsSQL
{
    public class DoctorDbRepository : IDoctorRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public DoctorDbRepository(IConfiguration configuration)
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            _httpClient = new HttpClient(httpClientHandler);
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri("http://localhost:5185/api/doctor");
        }

        public async Task<Doctor> AddDoctor(Doctor newDoctor, string token)
        {
            _httpClient.DefaultRequestHeaders.Add("ApiKey", _configuration.GetValue<string>("ApiKey"));
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var newJsonAsString = JsonConvert.SerializeObject(newDoctor);
            var requestBody = new StringContent(newJsonAsString, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("", requestBody);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var contact = JsonConvert.DeserializeObject<Doctor>(content);
                return contact;
            }

            return null;
        }

        public async Task DeleteDoctor(int doctorId, string token)
        {
            _httpClient.DefaultRequestHeaders.Add("ApiKey", _configuration.GetValue<string>("ApiKey"));
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var response = await _httpClient.DeleteAsync($"/api/doctor/{doctorId}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to delete data. Error: " + response.StatusCode);
            }
        }

        public async Task<List<Doctor>> GetAllDoctor(string token)
        {
            _httpClient.DefaultRequestHeaders.Add("ApiKey", _configuration.GetValue<string>("ApiKey"));
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var response = await _httpClient.GetAsync("");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var doctorList = JsonConvert.DeserializeObject<List<Doctor>>(content);
                return doctorList ?? new List<Doctor>();
            }

            return new List<Doctor>();
        }

        public async Task<Doctor> GetDoctorById(int id, string token)
        {
            _httpClient.DefaultRequestHeaders.Add("ApiKey", _configuration.GetValue<string>("ApiKey"));
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var response = await _httpClient.GetAsync($"/api/doctor/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var doctor = JsonConvert.DeserializeObject<Doctor>(content);
                return doctor;
            }

            return null;
        }

        public async Task<Doctor> UpdateDoctor(int doctorId, Doctor updatedDoctor, string token)
        {
            _httpClient.DefaultRequestHeaders.Add("ApiKey", _configuration.GetValue<string>("ApiKey"));
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var doctorJson = JsonConvert.SerializeObject(updatedDoctor);
            var doctorContent = new StringContent(doctorJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/api/doctor/{doctorId}", doctorContent);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var modifiedDoctor = JsonConvert.DeserializeObject<Doctor>(responseContent);
            return modifiedDoctor;
        }
    }
}
