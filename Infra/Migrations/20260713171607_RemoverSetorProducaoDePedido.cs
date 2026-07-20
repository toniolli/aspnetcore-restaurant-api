using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class RemoverSetorProducaoDePedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pedido_setorProducao_SetorProducaoId",
                table: "pedido");

            migrationBuilder.DropIndex(
                name: "IX_pedido_SetorProducaoId",
                table: "pedido");

            migrationBuilder.DropColumn(
                name: "SetorProducaoId",
                table: "pedido");

            migrationBuilder.AddColumn<int>(
                name: "SetorProducaoId",
                table: "ItemPedidos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedidos_SetorProducaoId",
                table: "ItemPedidos",
                column: "SetorProducaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPedidos_setorProducao_SetorProducaoId",
                table: "ItemPedidos",
                column: "SetorProducaoId",
                principalTable: "setorProducao",
                principalColumn: "Id_SetorProducao",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemPedidos_setorProducao_SetorProducaoId",
                table: "ItemPedidos");

            migrationBuilder.DropIndex(
                name: "IX_ItemPedidos_SetorProducaoId",
                table: "ItemPedidos");

            migrationBuilder.DropColumn(
                name: "SetorProducaoId",
                table: "ItemPedidos");

            migrationBuilder.AddColumn<int>(
                name: "SetorProducaoId",
                table: "pedido",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_pedido_SetorProducaoId",
                table: "pedido",
                column: "SetorProducaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_pedido_setorProducao_SetorProducaoId",
                table: "pedido",
                column: "SetorProducaoId",
                principalTable: "setorProducao",
                principalColumn: "Id_SetorProducao",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
