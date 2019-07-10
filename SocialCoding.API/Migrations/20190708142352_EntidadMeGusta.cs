using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialCoding.API.Migrations
{
    public partial class EntidadMeGusta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MeGustas",
                columns: table => new
                {
                    MeGustadorId = table.Column<int>(nullable: false),
                    MeGustaaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeGustas", x => new { x.MeGustadorId, x.MeGustaaId });
                    table.ForeignKey(
                        name: "FK_MeGustas_Usuarios_MeGustaaId",
                        column: x => x.MeGustaaId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MeGustas_Usuarios_MeGustadorId",
                        column: x => x.MeGustadorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeGustas_MeGustaaId",
                table: "MeGustas",
                column: "MeGustaaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeGustas");
        }
    }
}
