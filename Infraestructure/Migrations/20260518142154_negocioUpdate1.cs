using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class negocioUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Documento",
                schema: "order",
                table: "negocio",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "order",
                table: "negocio",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                schema: "order",
                table: "negocio",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Documento",
                schema: "order",
                table: "negocio");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "order",
                table: "negocio");

            migrationBuilder.DropColumn(
                name: "Telefone",
                schema: "order",
                table: "negocio");
        }
    }
}
