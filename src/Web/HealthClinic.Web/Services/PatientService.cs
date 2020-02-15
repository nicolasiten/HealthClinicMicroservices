using HealthClinic.Web.Common.Interfaces;
using HealthClinic.Web.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HealthClinic.Web.Services
{
    public class PatientService : IPatientService
    {
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;

        public PatientService(IConfiguration configuration, HttpClient httpClient)
        {
            _baseUrl = configuration.GetValue<string>("PatientBaseUrl");
            _httpClient = httpClient;
        }

        public async Task CreateAsync(PatientModel patientModel)
        {
            var content = new StringContent(JsonConvert.SerializeObject(patientModel), Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync(_baseUrl, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(int id, PatientModel patientModel)
        {
            var content = new StringContent(JsonConvert.SerializeObject(patientModel), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_baseUrl}{id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<PatientModel>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}getall");
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<PatientModel>>(responseString);
        }

        public async Task<PatientModel> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}{id}");
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<PatientModel>(responseString);
        }
    }
}
