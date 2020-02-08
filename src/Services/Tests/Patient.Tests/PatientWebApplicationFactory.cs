using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Patient.Infrastructure;
using System;
using System.Linq;
using System.Net.Http;

namespace Patient.Tests
{
    public class PatientWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<PatientDbContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }                    

                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<PatientDbContext>(options =>
                {
                    options.UseInMemoryDatabase("PatientTestDb");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<PatientDbContext>();

                context.Database.EnsureCreated();

                SeedSampleData(context);
            }).UseEnvironment("Test");
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
