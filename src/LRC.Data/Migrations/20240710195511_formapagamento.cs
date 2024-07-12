using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LRC.Data.Migrations
{
    /// <inheritdoc />
    public partial class formapagamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FluxoCaixa_FormaPagamento_FormaPagamentoId",
                table: "FluxoCaixa");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcela_FormaPagamento_FormaPagamentoId",
                table: "Parcela");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_FormaPagamento_FormaPagamentoId",
                table: "Pedido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormaPagamento",
                table: "FormaPagamento");

            migrationBuilder.RenameTable(
                name: "FormaPagamento",
                newName: "FormasPagamento");

            migrationBuilder.RenameColumn(
                name: "PeridoParcelamento",
                table: "FormasPagamento",
                newName: "PeriodoParcelamento");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "FormasPagamento",
                type: "varchar(200)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormasPagamento",
                table: "FormasPagamento",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FluxoCaixa_FormasPagamento_FormaPagamentoId",
                table: "FluxoCaixa",
                column: "FormaPagamentoId",
                principalTable: "FormasPagamento",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcela_FormasPagamento_FormaPagamentoId",
                table: "Parcela",
                column: "FormaPagamentoId",
                principalTable: "FormasPagamento",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_FormasPagamento_FormaPagamentoId",
                table: "Pedido",
                column: "FormaPagamentoId",
                principalTable: "FormasPagamento",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FluxoCaixa_FormasPagamento_FormaPagamentoId",
                table: "FluxoCaixa");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcela_FormasPagamento_FormaPagamentoId",
                table: "Parcela");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_FormasPagamento_FormaPagamentoId",
                table: "Pedido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormasPagamento",
                table: "FormasPagamento");

            migrationBuilder.RenameTable(
                name: "FormasPagamento",
                newName: "FormaPagamento");

            migrationBuilder.RenameColumn(
                name: "PeriodoParcelamento",
                table: "FormaPagamento",
                newName: "PeridoParcelamento");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "FormaPagamento",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormaPagamento",
                table: "FormaPagamento",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FluxoCaixa_FormaPagamento_FormaPagamentoId",
                table: "FluxoCaixa",
                column: "FormaPagamentoId",
                principalTable: "FormaPagamento",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcela_FormaPagamento_FormaPagamentoId",
                table: "Parcela",
                column: "FormaPagamentoId",
                principalTable: "FormaPagamento",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_FormaPagamento_FormaPagamentoId",
                table: "Pedido",
                column: "FormaPagamentoId",
                principalTable: "FormaPagamento",
                principalColumn: "Id");
        }
    }
}
