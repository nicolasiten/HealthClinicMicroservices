using Patient.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Patient.Tests.Controllers.Patients
{
    public class CreatePatientTests : IClassFixture<PatientWebApplicationFactory<Startup>>
    {
        private readonly PatientWebApplicationFactory<Startup> _factory;

        public CreatePatientTests(PatientWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenValidPatientItem_ReturnSuccessStatusCode()
        {
            var client = _factory.CreateClient();

            var patient = new PatientModel
            {
                Family = "Family",
                Given = "Given",
                Address = "Address",
                Dob = new DateTime(2020, 02, 05),
                Phone = "111-222-3333",
                Sex = "F"
            };

            var content = IntegrationTestHelper.GetRequestContent(patient);

            var response = await client.PostAsync("/api/patient", content);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GivenInvalidPatientItem_ReturnsBadRequest()
        {
            var client = _factory.CreateClient();

            var patient = new PatientModel
            {
                Family = "Family",
                Given = "Given",
                Address = "Address",
                Dob = new DateTime(2020, 02, 05),
                Phone = "111-222-3333",
                Sex = "This string will exceed the maximum length"
            };

            var content = IntegrationTestHelper.GetRequestContent(patient);

            var response = await client.PostAsync("/api/patient", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
