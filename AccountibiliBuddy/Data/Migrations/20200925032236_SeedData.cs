using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountibiliBuddy.Data.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "00000000-ffff-ffff-ffff-ffffffffffff", 0, "b522fd54-a211-4d5d-89b8-7e3bb6c5ebda", "admin@admin.com", true, "Admina", "Straytor", false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEEwMJgi8zJV+huC4ZKkohmUogzE3I05iIwbGeiT5ICgoIzbYUwMMI2GbyibUuGTZcw==", null, false, "7f434309-a4d9-48e9-9ebb-8803db794577", false, "admin@admin.com" });

            migrationBuilder.InsertData(
                table: "GoalType",
                columns: new[] { "GoalTypeId", "PointValue", "Type" },
                values: new object[,]
                {
                    { 1, 1, "Daily Goal" },
                    { 2, 5, "Weekly Goal" },
                    { 3, 10, "Long Term Goal" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff");

            migrationBuilder.DeleteData(
                table: "GoalType",
                keyColumn: "GoalTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GoalType",
                keyColumn: "GoalTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GoalType",
                keyColumn: "GoalTypeId",
                keyValue: 3);
        }
    }
}
