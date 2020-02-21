using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DiabetesRisk.Tests.Controllers.DiabetesRisk
{
    public class GetRiskTests : IClassFixture<DiabetesRiskWebApplicationFactory<Startup>>
    {
        private readonly DiabetesRiskWebApplicationFactory<Startup> _factory;

        public GetRiskTests(DiabetesRiskWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData(1, "Patient: Test TestNone (age 52) diabetes assessment is: None")]
        [InlineData(2, "Patient: Test TestBorderline (age 73) diabetes assessment is: Borderline")]
        [InlineData(3, "Patient: Test TestInDanger (age 14) diabetes assessment is: In Danger")]
        [InlineData(4, "Patient: Test TestEarlyOnset (age 16) diabetes assessment is: Early Onset")]
        public async Task GetAssessmentTests(int patientId, string expectedResult)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync($"/api/diabetesrisk/{patientId}");

            response.EnsureSuccessStatusCode();

            Assert.Equal(expectedResult, await response.Content.ReadAsStringAsync());
        }
    }
}
