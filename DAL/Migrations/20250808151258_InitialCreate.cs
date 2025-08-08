using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OwnerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    OwnerName = table.Column<string>(type: "TEXT", nullable: false),
                    OwnerEmail = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AnimalId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CustomerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    VeterinarianId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "BirthDate", "Name", "OwnerEmail", "OwnerId", "OwnerName" },
                values: new object[,]
                {
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d476"), new DateTime(2024, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rabbit", "rabbitsowner@example.com", new Guid("becaab5e-3f90-4bb0-b623-8257ebed9e98"), "Rabbit Owner" },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d477"), new DateTime(2023, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cat", "catowner@example.com", new Guid("193d0469-0245-4a28-985d-685cc879b0f5"), "Cat Owner" },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"), new DateTime(2022, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dog", "dogowner@example.com", new Guid("02806643-41b8-4891-afaf-e73e145c717b"), "Dog Owner" }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AnimalId", "CustomerId", "EndTime", "Notes", "StartTime", "Status", "VeterinarianId" },
                values: new object[,]
                {
                    { new Guid("f47ac10b-58cc-4372-a567-0e0232c3d479"), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"), new Guid("02806643-41b8-4891-afaf-e73e145c717b"), new DateTime(2025, 8, 12, 17, 0, 0, 0, DateTimeKind.Unspecified), "Vet appointment", new DateTime(2025, 8, 12, 16, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d480"), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"), new Guid("02806643-41b8-4891-afaf-e73e145c717b"), new DateTime(2025, 8, 13, 17, 0, 0, 0, DateTimeKind.Unspecified), "Follow-up check", new DateTime(2025, 8, 13, 16, 0, 0, 0, DateTimeKind.Unspecified), 0, new Guid("00000000-0000-0000-0000-000000000000") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropTable(
                name: "Appointments");
        }
    }
}
