using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountibiliBuddy.Data.Migrations
{
    public partial class GoalFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goal_AspNetUsers_UserId1",
                table: "Goal");

            migrationBuilder.DropIndex(
                name: "IX_Goal_UserId1",
                table: "Goal");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Goal");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Goal",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "05c4c9cb-e681-4a4e-8bb3-da6212f8a898", "AQAAAAEAACcQAAAAEB495kq8vVXVakqdzvAp59wK8l0LLwkY9trW8ZMRkBoMN2Arir91dnx3cLEjNqN7Jw==" });

            migrationBuilder.CreateIndex(
                name: "IX_Goal_UserId",
                table: "Goal",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Goal_AspNetUsers_UserId",
                table: "Goal",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goal_AspNetUsers_UserId",
                table: "Goal");

            migrationBuilder.DropIndex(
                name: "IX_Goal_UserId",
                table: "Goal");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Goal",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Goal",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "00722962-8b47-4924-b003-ab9ebe19b233", "AQAAAAEAACcQAAAAEPKl2NMU9rOB2CebXVAhzq6K22WWw42mmdppxMJ+V/+kuisuQSnu0AfqtelNbLnPMw==" });

            migrationBuilder.CreateIndex(
                name: "IX_Goal_UserId1",
                table: "Goal",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Goal_AspNetUsers_UserId1",
                table: "Goal",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
