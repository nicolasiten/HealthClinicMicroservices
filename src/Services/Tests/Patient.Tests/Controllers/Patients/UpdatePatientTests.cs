using Patient.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Patient.Tests.Controllers.Patients
{
    public class UpdatePatientTests : IClassFixture<PatientWebApplicationFactory<Startup>>
    {
        private readonly PatientWebApplicationFactory<Startup> _factory;

        public UpdatePatientTests(PatientWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenValidPatientItem_ReturnSuccessStatusCode()
        {
            var client = _factory.CreateClient();

            var patient = new PatientModel
            {
                Id = 1,
                Family = "Family",
                Given = "Given",
                Address = "Address",
                Dob = new DateTime(2020, 02, 05),
                Phone = "111-222-3333",
                Sex = "F"
            };

            var content = IntegrationTestHelper.GetRequestContent(patient);

            var response = await client.PutAsync($"/api/patient/{patient.Id}", content);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GivenInvalidPatientItem_ReturnsBadRequest()
        {
            var client = _factory.CreateClient();

            var patient = new PatientModel
            {
                Id = 1,
                Family = "Family",
                Given = "Given",
                Address = "Address",
                Dob = new DateTime(2020, 02, 05),
                Phone = "111-222-3333",
                Sex = "This string will exceed the maximum length"
            };

            var content = IntegrationTestHelper.GetRequestContent(patient);

            var response = await client.PutAsync($"/api/patient/{patient.Id}", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
