using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerResourcesDataAccess.Migrations
{
    public partial class Initial_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Sample_Time",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("MySQL:AutoIncrement", true),
            //        Time = table.Column<DateTime>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Sample_Time", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Server_Resource_Unit",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("MySQL:AutoIncrement", true),
            //        ShortName = table.Column<string>(nullable: false),
            //        Unit = table.Column<string>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Server_Resource_Unit", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Test_Entity",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("MySQL:AutoIncrement", true),
            //        Name = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Test_Entity", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Server_Resource_Info",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("MySQL:AutoIncrement", true),
            //        Value = table.Column<double>(nullable: false),
            //        Sample_Time_FK = table.Column<int>(nullable: false),
            //        Server_Resource_Unit_FK = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Server_Resource_Info", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Server_Resource_Info_Sample_Time_Sample_Time_FK",
            //            column: x => x.Sample_Time_FK,
            //            principalTable: "Sample_Time",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_Server_Resource_Info_Server_Resource_Unit_Server_Resource_Unit_FK",
            //            column: x => x.Server_Resource_Unit_FK,
            //            principalTable: "Server_Resource_Unit",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Server_Resource_Info_Sample_Time_FK",
            //    table: "Server_Resource_Info",
            //    column: "Sample_Time_FK");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Server_Resource_Info_Server_Resource_Unit_FK",
            //    table: "Server_Resource_Info",
            //    column: "Server_Resource_Unit_FK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Server_Resource_Info");

            migrationBuilder.DropTable(
                name: "Test_Entity");

            migrationBuilder.DropTable(
                name: "Sample_Time");

            migrationBuilder.DropTable(
                name: "Server_Resource_Unit");
        }
    }
}
