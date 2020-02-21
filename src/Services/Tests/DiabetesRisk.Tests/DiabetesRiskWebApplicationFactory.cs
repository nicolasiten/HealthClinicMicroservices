using DiabetesRisk.Common.Interfaces;
using DiabetesRisk.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiabetesRisk.Tests
{
    public class DiabetesRiskWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var patientService = new Mock<IPatientService>();
                var patientNoteService = new Mock<IPatientNoteService>();

                patientService.Setup(ps => ps.GetPatientAsync(1)).ReturnsAsync(new PatientModel
                {
                    Id = 1,
                    Family = "Test",
                    Given = "TestNone",
                    Dob = new DateTime(1967, 06, 22),
                    Sex = "M",
                    Address = "address",
                    Phone = "111-222-3333"
                });
                patientNoteService.Setup(pns => pns.GetNotesByPatientIdAsync(1)).ReturnsAsync(new List<string>
                {
                    "No Trigger Terms"
                });

                patientService.Setup(ps => ps.GetPatientAsync(2)).ReturnsAsync(new PatientModel
                {
                    Id = 2,
                    Family = "Test",
                    Given = "TestBorderline",
                    Dob = new DateTime(1946, 06, 22),
                    Sex = "M",
                    Address = "address",
                    Phone = "111-222-3333"
                });
                patientNoteService.Setup(pns => pns.GetNotesByPatientIdAsync(2)).ReturnsAsync(new List<string>
                {
                    "Patient is a Smoker and his Body Weight is too high"
                });

                patientService.Setup(ps => ps.GetPatientAsync(3)).ReturnsAsync(new PatientModel
                {
                    Id = 3,
                    Family = "Test",
                    Given = "TestInDanger",
                    Dob = new DateTime(2005, 06, 22),
                    Sex = "F",
                    Address = "address",
                    Phone = "111-222-3333"
                });
                patientNoteService.Setup(pns => pns.GetNotesByPatientIdAsync(3)).ReturnsAsync(new List<string>
                {
                    "Relapse Relapse Relapse Relapse Dizziness Reaction Reaction Reaction Reaction Antibodies"
                });

                patientService.Setup(ps => ps.GetPatientAsync(4)).ReturnsAsync(new PatientModel
                {
                    Id = 4,
                    Family = "Test",
                    Given = "TestEarlyOnset",
                    Dob = new DateTime(2003, 06, 22),
                    Sex = "M",
                    Address = "address",
                    Phone = "111-222-3333"
                });
                patientNoteService.Setup(pns => pns.GetNotesByPatientIdAsync(4)).ReturnsAsync(new List<string>
                {
                    "Relapse Dizziness Reaction Antibodies Cholesterol"
                });

                services.AddTransient<IPatientService>(_ => { return patientService.Object; });
                services.AddTransient<IPatientNoteService>(_ => { return patientNoteService.Object; });

                var dateTimeService = new Mock<IDateTimeService>();
                dateTimeService.Setup(dt => dt.DateTimeNow()).Returns(new DateTime(2020, 02, 21));
                services.AddTransient<IDateTimeService>(_ => { return dateTimeService.Object; });
            }).UseEnvironment("Test");
        }
    }
}
