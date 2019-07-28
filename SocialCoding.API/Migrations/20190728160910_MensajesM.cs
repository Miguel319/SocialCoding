using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialCoding.API.Migrations
{
    public partial class MensajesM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mensajes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RemitenteId = table.Column<int>(nullable: false),
                    ReceptorId = table.Column<int>(nullable: false),
                    Contenido = table.Column<string>(nullable: true),
                    Leido = table.Column<bool>(nullable: false),
                    LeidoEn = table.Column<DateTime>(nullable: true),
                    MensajeEnviado = table.Column<DateTime>(nullable: false),
                    RemitenteLoElimino = table.Column<bool>(nullable: false),
                    ReceptorLoElimino = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mensajes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mensajes_Usuarios_ReceptorId",
                        column: x => x.ReceptorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mensajes_Usuarios_RemitenteId",
                        column: x => x.RemitenteId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mensajes_ReceptorId",
                table: "Mensajes",
                column: "ReceptorId");

            migrationBuilder.CreateIndex(
                name: "IX_Mensajes_RemitenteId",
                table: "Mensajes",
                column: "RemitenteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mensajes");
        }
    }
}
