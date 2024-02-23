using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PsvManager.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "County", "HouseNumber", "Postcode", "StreetName", "TownOrCity" },
                values: new object[,]
                {
                    { new Guid("0e44a463-3b65-4b2c-81eb-52bc0e2b51c5"), null, "123", "12345", "Main St", "New York" },
                    { new Guid("34740eca-cd06-4c20-a354-fc870d447ace"), null, "456", "67890", "Elm St", "Los Angeles" }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "Make", "MaxPassengers", "Model", "Registration" },
                values: new object[,]
                {
                    { new Guid("3fa9b0c4-ac4b-431e-8684-64c878af9d29"), "Honda", 4, "Accord", "DEF456" },
                    { new Guid("ec409082-19da-43c7-8570-92ce7f68e513"), "Toyota", 5, "Camry", "ABC123" }
                });

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "AddressId", "Forename", "LicenseNumber", "Surname" },
                values: new object[,]
                {
                    { new Guid("8d9f847c-a995-471d-8fc1-f67531abce97"), new Guid("34740eca-cd06-4c20-a354-fc870d447ace"), "Craig", "Test4567", "Cheney" },
                    { new Guid("b7e666b4-7c36-4b11-91d7-67ccf41e182f"), new Guid("0e44a463-3b65-4b2c-81eb-52bc0e2b51c5"), "John", "Test1234", "Bon Jovi" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("8d9f847c-a995-471d-8fc1-f67531abce97"));

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("b7e666b4-7c36-4b11-91d7-67ccf41e182f"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("3fa9b0c4-ac4b-431e-8684-64c878af9d29"));

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: new Guid("ec409082-19da-43c7-8570-92ce7f68e513"));

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("0e44a463-3b65-4b2c-81eb-52bc0e2b51c5"));

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("34740eca-cd06-4c20-a354-fc870d447ace"));
        }
    }
}
