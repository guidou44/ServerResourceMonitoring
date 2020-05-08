using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerResourcesDataAccess.Migrations
{
    public partial class Added_Process_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Process_FK",
                table: "Server_Resource_Info",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Process",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Process", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Server_Resource_Info_Process_FK",
                table: "Server_Resource_Info",
                column: "Process_FK");

            migrationBuilder.AddForeignKey(
                name: "FK_Server_Resource_Info_Process_Process_FK",
                table: "Server_Resource_Info",
                column: "Process_FK",
                principalTable: "Process",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Server_Resource_Info_Process_Process_FK",
                table: "Server_Resource_Info");

            migrationBuilder.DropTable(
                name: "Process");

            migrationBuilder.DropIndex(
                name: "IX_Server_Resource_Info_Process_FK",
                table: "Server_Resource_Info");

            migrationBuilder.DropColumn(
                name: "Process_FK",
                table: "Server_Resource_Info");
        }
    }
}
