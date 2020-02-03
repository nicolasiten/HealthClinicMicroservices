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

        public async Task Create(PatientModel patientModel)
        {
            var content = new StringContent(JsonConvert.SerializeObject(patientModel), Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync(_baseUrl, content);
            response.EnsureSuccessStatusCode();
        }
    }
}
