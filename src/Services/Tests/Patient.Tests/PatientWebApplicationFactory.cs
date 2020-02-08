using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Patient.Infrastructure;
using System;
using System.Net.Http;

namespace Patient.Tests
{
    public class PatientWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<PatientDbContext>(options =>
                {
                    options.UseInMemoryDatabase(Guid.NewGuid().ToString());
                    options.UseInternalServiceProvider(serviceProvider);
                });

                services.AddDbContext<PatientDbContext>();

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<PatientDbContext>();

                context.Database.EnsureCreated();

                SeedSampleData(context);
            });
        }

        private static void SeedSampleData(PatientDbContext dbContext)
        {
            dbContext.Patients.Add(new Entities.Patient
            {
                FirstName = "Firstname",
                LastName = "Lastname",
                Address = "Address",
                DateOfBirth = DateTime.Today,
                Phone = "111-222-3333",
                Sex = "F"
            });

            dbContext.SaveChanges();
        }
    }
}
