using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunnerApp.Data.Migrations
{
    public partial class MissCalledCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataOfBirth",
                table: "Users",
                newName: "DateOfBirth");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "Users",
                newName: "DataOfBirth");
        }
    }
}
