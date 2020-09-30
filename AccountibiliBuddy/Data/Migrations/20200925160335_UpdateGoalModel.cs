using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountibiliBuddy.Data.Migrations
{
    public partial class UpdateGoalModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GoalTypeId",
                table: "Goal",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "00722962-8b47-4924-b003-ab9ebe19b233", "AQAAAAEAACcQAAAAEPKl2NMU9rOB2CebXVAhzq6K22WWw42mmdppxMJ+V/+kuisuQSnu0AfqtelNbLnPMw==" });

            migrationBuilder.CreateIndex(
                name: "IX_Goal_GoalTypeId",
                table: "Goal",
                column: "GoalTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Goal_GoalType_GoalTypeId",
                table: "Goal",
                column: "GoalTypeId",
                principalTable: "GoalType",
                principalColumn: "GoalTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goal_GoalType_GoalTypeId",
                table: "Goal");

            migrationBuilder.DropIndex(
                name: "IX_Goal_GoalTypeId",
                table: "Goal");

            migrationBuilder.DropColumn(
                name: "GoalTypeId",
                table: "Goal");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b522fd54-a211-4d5d-89b8-7e3bb6c5ebda", "AQAAAAEAACcQAAAAEEwMJgi8zJV+huC4ZKkohmUogzE3I05iIwbGeiT5ICgoIzbYUwMMI2GbyibUuGTZcw==" });
        }
    }
}
