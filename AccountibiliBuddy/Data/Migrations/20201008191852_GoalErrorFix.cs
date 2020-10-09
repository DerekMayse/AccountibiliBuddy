using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountibiliBuddy.Data.Migrations
{
    public partial class GoalErrorFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8972ec58-2634-47b7-b4a3-9912557e4805", "AQAAAAEAACcQAAAAECXyZt20yCyOgL+9q5zWUj5KOeNjHfXEVKx5a1JIrYkVG9A8nXjv+tS23boCcd5gaw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "019663ec-7f0c-49c7-8d6c-fb1fcb47bef8", "AQAAAAEAACcQAAAAEGKVUUeWEt8cjSGGyrIb0aL7N16oyzHOX/tI1Wo2rLLEU4lZDesi+OYx++Oy9ooBWA==" });
        }
    }
}
