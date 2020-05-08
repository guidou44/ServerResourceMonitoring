using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerResourcesDataAccess.Migrations
{
    public partial class Added_PID_Column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PID",
                table: "Process",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PID",
                table: "Process");
        }
    }
}
