using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class EstruturaInicialSistema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cardapio",
                columns: table => new
                {
                    Id_Cardapio = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cardapio", x => x.Id_Cardapio);
                });

            migrationBuilder.CreateTable(
                name: "mesa",
                columns: table => new
                {
                    Id_Mesa = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NumeroMesa = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mesa", x => x.Id_Mesa);
                });

            migrationBuilder.CreateTable(
                name: "setorProducao",
                columns: table => new
                {
                    Id_SetorProducao = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_setorProducao", x => x.Id_SetorProducao);
                });

            migrationBuilder.CreateTable(
                name: "item_cardapio",
                columns: table => new
                {
                    Id_ItemCardapio = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CardapioId = table.Column<int>(type: "integer", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    Preco = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Disponivel = table.Column<bool>(type: "boolean", nullable: false),
                    Categoria = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_cardapio", x => x.Id_ItemCardapio);
                    table.ForeignKey(
                        name: "FK_item_cardapio_cardapio_CardapioId",
                        column: x => x.CardapioId,
                        principalTable: "cardapio",
                        principalColumn: "Id_Cardapio",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "comanda",
                columns: table => new
                {
                    Id_Comanda = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MesaId = table.Column<int>(type: "integer", nullable: false),
                    StatusComanda = table.Column<int>(type: "integer", nullable: false),
                    DataAbertura = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    DataFechamento = table.Column<DateTime>(type: "timestamptz", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comanda", x => x.Id_Comanda);
                    table.ForeignKey(
                        name: "FK_comanda_mesa_MesaId",
                        column: x => x.MesaId,
                        principalTable: "mesa",
                        principalColumn: "Id_Mesa",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pedido",
                columns: table => new
                {
                    Id_Pedido = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MesaId = table.Column<int>(type: "integer", nullable: false),
                    ComandaId = table.Column<int>(type: "integer", nullable: false),
                    SetorProducaoId = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedido", x => x.Id_Pedido);
                    table.ForeignKey(
                        name: "FK_pedido_comanda_ComandaId",
                        column: x => x.ComandaId,
                        principalTable: "comanda",
                        principalColumn: "Id_Comanda",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pedido_mesa_MesaId",
                        column: x => x.MesaId,
                        principalTable: "mesa",
                        principalColumn: "Id_Mesa",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pedido_setorProducao_SetorProducaoId",
                        column: x => x.SetorProducaoId,
                        principalTable: "setorProducao",
                        principalColumn: "Id_SetorProducao",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemPedidos",
                columns: table => new
                {
                    Id_ItemPedido = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PedidoId = table.Column<int>(type: "integer", nullable: false),
                    ItemCardapioId = table.Column<int>(type: "integer", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Quantidade = table.Column<int>(type: "integer", nullable: false),
                    ValorUnitario = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Observacao = table.Column<string>(type: "text", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "timestamptz", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamptz", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPedidos", x => x.Id_ItemPedido);
                    table.ForeignKey(
                        name: "FK_ItemPedidos_item_cardapio_ItemCardapioId",
                        column: x => x.ItemCardapioId,
                        principalTable: "item_cardapio",
                        principalColumn: "Id_ItemCardapio",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemPedidos_pedido_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "pedido",
                        principalColumn: "Id_Pedido",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_comanda_MesaId",
                table: "comanda",
                column: "MesaId");

            migrationBuilder.CreateIndex(
                name: "IX_item_cardapio_CardapioId",
                table: "item_cardapio",
                column: "CardapioId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedidos_ItemCardapioId",
                table: "ItemPedidos",
                column: "ItemCardapioId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedidos_PedidoId",
                table: "ItemPedidos",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_pedido_ComandaId",
                table: "pedido",
                column: "ComandaId");

            migrationBuilder.CreateIndex(
                name: "IX_pedido_MesaId",
                table: "pedido",
                column: "MesaId");

            migrationBuilder.CreateIndex(
                name: "IX_pedido_SetorProducaoId",
                table: "pedido",
                column: "SetorProducaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemPedidos");

            migrationBuilder.DropTable(
                name: "item_cardapio");

            migrationBuilder.DropTable(
                name: "pedido");

            migrationBuilder.DropTable(
                name: "cardapio");

            migrationBuilder.DropTable(
                name: "comanda");

            migrationBuilder.DropTable(
                name: "setorProducao");

            migrationBuilder.DropTable(
                name: "mesa");
        }
    }
}
