using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.Ingresso.Usuario.API.Migrations
{
    /// <inheritdoc />
    public partial class senha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Usuarios",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Usuarios");
        }
    }
}
