using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TechTree.AssociateService.Migrations
{
    public partial class Associate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Association",
                table: "Associates");

            migrationBuilder.CreateTable(
                name: "Association",
                columns: table => new
                {
                    AssociateId = table.Column<int>(nullable: false),
                    NodeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Association", x => new { x.AssociateId, x.NodeId });
                    table.ForeignKey(
                        name: "FK_Association_Associates_AssociateId",
                        column: x => x.AssociateId,
                        principalTable: "Associates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Association");

            migrationBuilder.AddColumn<string>(
                name: "Association",
                table: "Associates",
                nullable: true);
        }
    }
}
