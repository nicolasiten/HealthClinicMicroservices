using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Patient.Tests.Controllers.Patients
{
    public class DeletePatientTests : IClassFixture<PatientWebApplicationFactory<Startup>>
    {
        private readonly PatientWebApplicationFactory<Startup> _factory;

        public DeletePatientTests(PatientWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenValidPatientId_ReturnsSuccessStatusCode()
        {
            var validId = 1;

            var client = _factory.CreateClient();

            var response = await client.DeleteAsync($"/api/patient/{validId}");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GivenInvalidPatientId_ReturnsNotFound()
        {
            var invalidId = 33;

            var client = _factory.CreateClient();

            var response = await client.DeleteAsync($"/api/patient/{invalidId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
