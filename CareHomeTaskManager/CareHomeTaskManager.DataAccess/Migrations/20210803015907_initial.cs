using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CareHomeTaskManager.DataAccess.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CareTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    PatientName = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    CreatedByUser = table.Column<string>(type: "TEXT", maxLength: 450, nullable: true),
                    ActualStartDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TargetDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Reason = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    Action = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    Frequency = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    Completed = table.Column<bool>(type: "INTEGER", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Outcome = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareTasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    JwtToken = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Email);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CareTasks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
