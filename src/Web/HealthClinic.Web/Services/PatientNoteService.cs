using HealthClinic.Web.Common.Interfaces;
using HealthClinic.Web.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HealthClinic.Web.Services
{
    public class PatientNoteService : IPatientNoteService
    {
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;

        public PatientNoteService(IConfiguration configuration, HttpClient httpClient)
        {
            _baseUrl = configuration.GetValue<string>("PatientNotesBaseUrl");
            _httpClient = httpClient;
        }

        public async Task CreateAsync(PatientNoteModel patientNoteModel)
        {
            var content = new StringContent(JsonConvert.SerializeObject(patientNoteModel), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_baseUrl, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(string id, PatientNoteModel patientNoteModel) 
        {
            var content = new StringContent(JsonConvert.SerializeObject(patientNoteModel), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_baseUrl}{id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<PatientNoteModel>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}getall");
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<PatientNoteModel>>(responseString);
        }

        public async Task<IEnumerable<PatientNoteModel>> GetByPatientIdAsync(int patientId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}GetAllByPatientId/{patientId}");
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<PatientNoteModel>>(responseString);
        }

        public async Task<PatientNoteModel> GetByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}{id}");
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<PatientNoteModel>(responseString);
        }
    }
}
