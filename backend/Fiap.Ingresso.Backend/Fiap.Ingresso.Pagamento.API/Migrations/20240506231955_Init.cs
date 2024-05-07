using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.Ingresso.Pagamento.API.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pagamentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IngressoId = table.Column<string>(type: "varchar(50)", nullable: true),
                    TipoPagamento = table.Column<int>(type: "int", nullable: false),
                    ValorPagamento = table.Column<decimal>(type: "numeric(18,0)", nullable: false),
                    NumeroCartao = table.Column<string>(type: "varchar(40)", nullable: true),
                    NomeCartao = table.Column<string>(type: "varchar(60)", nullable: true),
                    VencimentoCartao = table.Column<string>(type: "varchar(5)", nullable: true),
                    CodigoVerificador = table.Column<string>(type: "varchar(3)", nullable: true),
                    LinhaDigitavel = table.Column<string>(type: "varchar(150)", nullable: true),
                    DataPagamento = table.Column<DateTime>(type: "Date", nullable: false),
                    PagamentoValido = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamentos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pagamentos");
        }
    }
}
