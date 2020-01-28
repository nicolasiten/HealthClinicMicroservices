using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Patient.Infrastructure.Migrations
{
    public partial class PatientSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(maxLength: 200, nullable: false),
                    FirstName = table.Column<string>(maxLength: 200, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    Sex = table.Column<string>(maxLength: 20, nullable: false),
                    Address = table.Column<string>(maxLength: 200, nullable: false),
                    Phone = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
