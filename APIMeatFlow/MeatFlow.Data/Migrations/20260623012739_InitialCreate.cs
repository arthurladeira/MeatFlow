using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeatFlow.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "carne",
                columns: table => new
                {
                    IDT_CARNE = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    descricao_carne = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    origem_carne = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    dat_criacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dat_atualizacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carne", x => x.IDT_CARNE);
                });

            migrationBuilder.CreateTable(
                name: "estado",
                columns: table => new
                {
                    IDT_ESTADO = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sigla_estado = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    nome_estado = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    dat_criacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dat_atualizacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estado", x => x.IDT_ESTADO);
                });

            migrationBuilder.CreateTable(
                name: "cidade",
                columns: table => new
                {
                    IDT_CIDADE = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDT_ESTADO = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nome_cidade = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    dat_criacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dat_atualizacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cidade", x => x.IDT_CIDADE);
                    table.ForeignKey(
                        name: "FK_cidade_estado_IDT_ESTADO",
                        column: x => x.IDT_ESTADO,
                        principalTable: "estado",
                        principalColumn: "IDT_ESTADO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "comprador",
                columns: table => new
                {
                    IDT_COMPRADOR = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    documento_fiscal = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    nome_comprador = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IDT_CIDADE = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    dat_criacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dat_atualizacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comprador", x => x.IDT_COMPRADOR);
                    table.ForeignKey(
                        name: "FK_comprador_cidade_IDT_CIDADE",
                        column: x => x.IDT_CIDADE,
                        principalTable: "cidade",
                        principalColumn: "IDT_CIDADE",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pedido",
                columns: table => new
                {
                    IDT_PEDIDO = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDT_COMPRADOR = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    data_pedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dat_criacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dat_atualizacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedido", x => x.IDT_PEDIDO);
                    table.ForeignKey(
                        name: "FK_pedido_comprador_IDT_COMPRADOR",
                        column: x => x.IDT_COMPRADOR,
                        principalTable: "comprador",
                        principalColumn: "IDT_COMPRADOR",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "item_pedido",
                columns: table => new
                {
                    IDT_ITEM_PEDIDO = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDT_PEDIDO = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDT_CARNE = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    quantidade_kg = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    valor_unitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    codigo_moeda = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    dat_criacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dat_atualizacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_pedido", x => x.IDT_ITEM_PEDIDO);
                    table.ForeignKey(
                        name: "FK_item_pedido_carne_IDT_CARNE",
                        column: x => x.IDT_CARNE,
                        principalTable: "carne",
                        principalColumn: "IDT_CARNE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_item_pedido_pedido_IDT_PEDIDO",
                        column: x => x.IDT_PEDIDO,
                        principalTable: "pedido",
                        principalColumn: "IDT_PEDIDO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cidade_IDT_ESTADO",
                table: "cidade",
                column: "IDT_ESTADO");

            migrationBuilder.CreateIndex(
                name: "IX_comprador_documento_fiscal",
                table: "comprador",
                column: "documento_fiscal",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_comprador_IDT_CIDADE",
                table: "comprador",
                column: "IDT_CIDADE");

            migrationBuilder.CreateIndex(
                name: "IX_estado_sigla_estado",
                table: "estado",
                column: "sigla_estado",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_item_pedido_IDT_CARNE",
                table: "item_pedido",
                column: "IDT_CARNE");

            migrationBuilder.CreateIndex(
                name: "IX_item_pedido_IDT_PEDIDO",
                table: "item_pedido",
                column: "IDT_PEDIDO");

            migrationBuilder.CreateIndex(
                name: "IX_pedido_IDT_COMPRADOR",
                table: "pedido",
                column: "IDT_COMPRADOR");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "item_pedido");

            migrationBuilder.DropTable(
                name: "carne");

            migrationBuilder.DropTable(
                name: "pedido");

            migrationBuilder.DropTable(
                name: "comprador");

            migrationBuilder.DropTable(
                name: "cidade");

            migrationBuilder.DropTable(
                name: "estado");
        }
    }
}
