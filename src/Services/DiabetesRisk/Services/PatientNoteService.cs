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
    public class PatientNoteService : IPatientNoteService
    {
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;

        public PatientNoteService(IConfiguration configuration, HttpClient httpClient)
        {
            _baseUrl = configuration.GetValue<string>("PatientNotesBaseUrl");
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<string>> GetNotesByPatientIdAsync(int patientId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}GetAllByPatientId/{patientId}");
            var responseString = await response.Content.ReadAsStringAsync();

            var patientNotes = JsonConvert.DeserializeObject<IEnumerable<PatientNoteModel>>(responseString);
            return patientNotes.Select(pn => pn.Note).ToList();
        }
    }
}
