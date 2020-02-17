using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PatientNotes.Tests.Controllers.PatientNotes
{
    public class ReadPatientNotesTests : IClassFixture<PatientNotesWebApplicationFactory<Startup>>
    {
        private readonly PatientNotesWebApplicationFactory<Startup> _factory;

        public ReadPatientNotesTests(PatientNotesWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenPatientNotesId_ReturnsSuccessStatusCode()
        {
            var id = "patientId";

            var client = _factory.CreateClient();

            var response = await client.GetAsync($"/api/patientnotes/{id}");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetAllPatientNotes_ReturnsSuccessStatusCode()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/patientnotes/getall");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetAllPatientNotesByPatientId_ReturnsSuccessStatusCode()
        {
            var patientId = 1;

            var client = _factory.CreateClient();

            var response = await client.GetAsync($"/api/patientnotes/GetAllByPatientId/{patientId}");

            response.EnsureSuccessStatusCode();
        }
    }
}
