using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations
{
    public partial class initialemigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cours",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cours", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "etudiants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_etudiants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourEtudiant",
                columns: table => new
                {
                    coursId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    etudiantsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourEtudiant", x => new { x.coursId, x.etudiantsId });
                    table.ForeignKey(
                        name: "FK_CourEtudiant_cours_coursId",
                        column: x => x.coursId,
                        principalTable: "cours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourEtudiant_etudiants_etudiantsId",
                        column: x => x.etudiantsId,
                        principalTable: "etudiants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourEtudiant_etudiantsId",
                table: "CourEtudiant",
                column: "etudiantsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourEtudiant");

            migrationBuilder.DropTable(
                name: "cours");

            migrationBuilder.DropTable(
                name: "etudiants");
        }
    }
}
