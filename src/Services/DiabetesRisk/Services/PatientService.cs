using DiabetesRisk.Common.Interfaces;
using DiabetesRisk.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DiabetesRisk.Services
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

        public async Task<PatientModel> GetPatient(int patientId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}{patientId}");
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<PatientModel>(responseString);
        }
    }
}
