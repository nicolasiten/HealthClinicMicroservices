using HealthClinic.Web.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HealthClinic.Web.Services
{
    public class DiabetesRiskService : IDiabetesRiskService
    {
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;

        public DiabetesRiskService(IConfiguration configuration, HttpClient httpClient)
        {
            _baseUrl = configuration.GetValue<string>("DiabetesRiskBaseUrl");
            _httpClient = httpClient;
        }

        public async Task<string> GetByPatientId(int patientId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}{patientId}");
            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }
    }
}
