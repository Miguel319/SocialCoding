using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialCoding.API.Migrations
{
    public partial class IdPublicaAgregada : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdPublica",
                table: "Imagenes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdPublica",
                table: "Imagenes");
        }
    }
}
