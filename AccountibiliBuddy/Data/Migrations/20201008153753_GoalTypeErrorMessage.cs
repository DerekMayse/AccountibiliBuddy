using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountibiliBuddy.Data.Migrations
{
    public partial class GoalTypeErrorMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "019663ec-7f0c-49c7-8d6c-fb1fcb47bef8", "AQAAAAEAACcQAAAAEGKVUUeWEt8cjSGGyrIb0aL7N16oyzHOX/tI1Wo2rLLEU4lZDesi+OYx++Oy9ooBWA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "05c4c9cb-e681-4a4e-8bb3-da6212f8a898", "AQAAAAEAACcQAAAAEB495kq8vVXVakqdzvAp59wK8l0LLwkY9trW8ZMRkBoMN2Arir91dnx3cLEjNqN7Jw==" });
        }
    }
}
