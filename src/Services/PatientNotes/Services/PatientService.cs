using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PatientNotes.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PatientNotes.Services
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

        public async Task<bool> PatientExists(int patientId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}{patientId}");
            return response.IsSuccessStatusCode;
        }
    }
}
