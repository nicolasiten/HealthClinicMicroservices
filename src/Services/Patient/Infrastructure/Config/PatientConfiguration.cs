using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patient.Infrastructure.Config
{
    public class PatientConfiguration : IEntityTypeConfiguration<Entities.Patient>
    {
        public void Configure(EntityTypeBuilder<Entities.Patient> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.LastName)                
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.FirstName)                
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.DateOfBirth)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(p => p.Sex)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(p => p.Address)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.Phone)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
