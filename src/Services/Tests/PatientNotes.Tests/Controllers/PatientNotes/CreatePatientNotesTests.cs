using PatientNotes.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PatientNotes.Tests.Controllers.PatientNotes
{
    public class CreatePatientNotesTests : IClassFixture<PatientNotesWebApplicationFactory<Startup>>
    {
        private readonly PatientNotesWebApplicationFactory<Startup> _factory;

        public CreatePatientNotesTests(PatientNotesWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenValidPatientNotesItem_ReturnsSuccessStatusCode()
        {
            var client = _factory.CreateClient();

            var patientNote = new PatientNoteModel
            {
                PatientId = 1,
                Note = "Note"
            };

            var content = IntegrationTestHelper.GetRequestContent(patientNote);

            var response = await client.PostAsync("/api/patientnotes", content);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GivenInvalidPatientNotesItem_ReturnsNotFound()
        {
            var client = _factory.CreateClient();

            var patientNote = new PatientNoteModel
            {
                PatientId = 2,
                Note = "Note"
            };

            var content = IntegrationTestHelper.GetRequestContent(patientNote);

            var response = await client.PostAsync("/api/patientnotes", content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
