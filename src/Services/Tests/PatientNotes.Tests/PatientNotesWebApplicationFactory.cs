using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PatientNotes.Common.Interfaces;
using PatientNotes.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace PatientNotes.Tests
{
    public class PatientNotesWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services => 
            {
                var patientNoteConnector = new Mock<INoSqlDbConnector<PatientNote>>();
                patientNoteConnector.Setup(pn => pn.DeleteAsync(It.IsAny<string>()));
                patientNoteConnector.Setup(pn => pn.GetAllAsync()).ReturnsAsync(new List<PatientNote>());
                patientNoteConnector.Setup(pn => pn.GetAllByFilterAsync(It.IsAny<Expression<Func<PatientNote, bool>>>())).ReturnsAsync(new List<PatientNote>() { new PatientNote() });
                patientNoteConnector.Setup(pn => pn.InsertAsync(It.IsAny<PatientNote>())).ReturnsAsync(new PatientNote());
                patientNoteConnector.Setup(pn => pn.UpdateAsync(It.IsAny<PatientNote>()));              

                var patientService = new Mock<IPatientService>();
                patientService.Setup(ps => ps.PatientExists(1)).ReturnsAsync(true);
                patientService.Setup(ps => ps.PatientExists(2)).ReturnsAsync(false);

                services.AddTransient<INoSqlDbConnector<PatientNote>>(_ => { return patientNoteConnector.Object; });
                services.AddTransient<IPatientService>(_ => { return patientService.Object; });
            }).UseEnvironment("Test");
        }
    }
}
