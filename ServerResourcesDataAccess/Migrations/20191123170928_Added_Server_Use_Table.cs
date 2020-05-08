using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerResourcesDataAccess.Migrations
{
    public partial class Added_Server_Use_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Server_User_FK",
                table: "Process",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Server_User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Server_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Process_Server_User_FK",
                table: "Process",
                column: "Server_User_FK");

            migrationBuilder.AddForeignKey(
                name: "FK_Process_Server_User_Server_User_FK",
                table: "Process",
                column: "Server_User_FK",
                principalTable: "Server_User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Process_Server_User_Server_User_FK",
                table: "Process");

            migrationBuilder.DropTable(
                name: "Server_User");

            migrationBuilder.DropIndex(
                name: "IX_Process_Server_User_FK",
                table: "Process");

            migrationBuilder.DropColumn(
                name: "Server_User_FK",
                table: "Process");
        }
    }
}
