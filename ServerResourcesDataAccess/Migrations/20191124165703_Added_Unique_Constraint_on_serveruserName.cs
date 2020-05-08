using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerResourcesDataAccess.Migrations
{
    public partial class Added_Unique_Constraint_on_serveruserName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "Short_Name",
            //    table: "Server_Resource_Unit",
            //    newName: "ShortName");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Server_User",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Server_User_Name",
                table: "Server_User",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Server_User_Name",
                table: "Server_User");

            migrationBuilder.RenameColumn(
                name: "ShortName",
                table: "Server_Resource_Unit",
                newName: "Short_Name");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Server_User",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
