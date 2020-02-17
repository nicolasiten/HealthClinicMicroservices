using PatientNotes.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PatientNotes.Tests.Controllers.PatientNotes
{
    public class UpdatePatientNotesTests : IClassFixture<PatientNotesWebApplicationFactory<Startup>>
    {
        private readonly PatientNotesWebApplicationFactory<Startup> _factory;

        public UpdatePatientNotesTests(PatientNotesWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenValidPatientNotesItem_ReturnsSuccessStatusCode()
        {
            var client = _factory.CreateClient();

            var patientNote = new PatientNoteModel
            {
                Id = "5e4835f48ca24a0c5e408313",
                PatientId = 1,
                Note = "Note"
            };

            var content = IntegrationTestHelper.GetRequestContent(patientNote);

            var response = await client.PutAsync($"/api/patientnotes/{patientNote.Id}", content);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GivenInvalidPatientNotesItem_ReturnsNotFound()
        {
            var client = _factory.CreateClient();

            var patientNote = new PatientNoteModel
            {
                Id = "patientNoteId",
                PatientId = 2,
                Note = "Note"
            };

            var content = IntegrationTestHelper.GetRequestContent(patientNote);

            var response = await client.PutAsync($"/api/patientnotes/{patientNote.Id}", content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
