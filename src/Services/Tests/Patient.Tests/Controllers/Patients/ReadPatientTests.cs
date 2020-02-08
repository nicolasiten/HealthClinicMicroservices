using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Patient.Tests.Controllers.Patients
{
    public class ReadPatientTests : IClassFixture<PatientWebApplicationFactory<Startup>>
    {
        private readonly PatientWebApplicationFactory<Startup> _factory;

        public ReadPatientTests(PatientWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenValidPatientId_ReturnsSuccessStatusCode()
        {
            var validId = 1;

            var client = _factory.CreateClient();

            var response = await client.GetAsync($"/api/patient/{validId}");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetAllPatients_ReturnsSuccessStatusCode()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/patient/getall");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GivenInvalidPatientId_ReturnsNotFound()
        {
            var invalidId = 21;

            var client = _factory.CreateClient();

            var response = await client.GetAsync($"/api/patient/{invalidId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
