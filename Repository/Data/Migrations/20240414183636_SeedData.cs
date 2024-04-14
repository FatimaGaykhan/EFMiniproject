using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Data.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Educations",
                columns: new[] { "Id", "Color", "CreatedDate", "Name" },
                values: new object[] { 1000, "orange", new DateTime(2024, 4, 14, 22, 36, 36, 515, DateTimeKind.Local).AddTicks(1180), "Back End" });

            migrationBuilder.InsertData(
                table: "Educations",
                columns: new[] { "Id", "Color", "CreatedDate", "Name" },
                values: new object[] { 1001, "green", new DateTime(2024, 4, 14, 22, 36, 36, 515, DateTimeKind.Local).AddTicks(1210), "Front End" });

            migrationBuilder.InsertData(
                table: "Educations",
                columns: new[] { "Id", "Color", "CreatedDate", "Name" },
                values: new object[] { 1002, "black", new DateTime(2024, 4, 14, 22, 36, 36, 515, DateTimeKind.Local).AddTicks(1220), "Cyber Security" });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Capacity", "CreatedDate", "EducationId", "Name" },
                values: new object[,]
                {
                    { 30, 20, new DateTime(2024, 4, 14, 22, 36, 36, 515, DateTimeKind.Local).AddTicks(1270), 1000, "Group100" },
                    { 32, 15, new DateTime(2024, 4, 14, 22, 36, 36, 515, DateTimeKind.Local).AddTicks(1280), 1001, "Group105" },
                    { 33, 20, new DateTime(2024, 4, 14, 22, 36, 36, 515, DateTimeKind.Local).AddTicks(1280), 1002, "Group106" },
                    { 34, 18, new DateTime(2024, 4, 14, 22, 36, 36, 515, DateTimeKind.Local).AddTicks(1280), 1002, "Group107" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Educations",
                keyColumn: "Id",
                keyValue: 1000);

            migrationBuilder.DeleteData(
                table: "Educations",
                keyColumn: "Id",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "Educations",
                keyColumn: "Id",
                keyValue: 1002);
        }
    }
}
