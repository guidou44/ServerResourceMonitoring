using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerResourcesDataAccess.Migrations
{
    public partial class Added_resource_type_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "ShortName",
            //    table: "Server_Resource_Unit",
            //    newName: "Short_Name");

            migrationBuilder.AddColumn<int>(
                name: "Resource_Type_FK",
                table: "Server_Resource_Info",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Resource_Type",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Short_Name = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resource_Type", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Server_Resource_Info_Resource_Type_FK",
                table: "Server_Resource_Info",
                column: "Resource_Type_FK");

            migrationBuilder.AddForeignKey(
                name: "FK_Server_Resource_Info_Resource_Type_Resource_Type_FK",
                table: "Server_Resource_Info",
                column: "Resource_Type_FK",
                principalTable: "Resource_Type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Server_Resource_Info_Resource_Type_Resource_Type_FK",
                table: "Server_Resource_Info");

            migrationBuilder.DropTable(
                name: "Resource_Type");

            migrationBuilder.DropIndex(
                name: "IX_Server_Resource_Info_Resource_Type_FK",
                table: "Server_Resource_Info");

            migrationBuilder.DropColumn(
                name: "Resource_Type_FK",
                table: "Server_Resource_Info");

            migrationBuilder.RenameColumn(
                name: "Short_Name",
                table: "Server_Resource_Unit",
                newName: "ShortName");
        }
    }
}
