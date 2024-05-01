using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.Ingresso.Evento.API.Migrations
{
    /// <inheritdoc />
    public partial class InitEvento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(200)", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "Date", nullable: false),
                    DataFim = table.Column<DateTime>(type: "Date", nullable: false),
                    DataEvento = table.Column<DateTime>(type: "Date", nullable: false),
                    PublicoMaximo = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<int>(type: "int", nullable: false),
                    Logradouro = table.Column<string>(type: "varchar(300)", nullable: false),
                    Numero = table.Column<string>(type: "varchar(20)", nullable: false),
                    Cidade = table.Column<string>(type: "varchar(50)", nullable: false),
                    Bairro = table.Column<string>(type: "varchar(50)", nullable: false),
                    Cep = table.Column<string>(type: "varchar(20)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(1000)", nullable: true),
                    SiteEvento = table.Column<string>(type: "varchar(300)", nullable: true),
                    Valor = table.Column<decimal>(type: "numeric(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Eventos");
        }
    }
}
