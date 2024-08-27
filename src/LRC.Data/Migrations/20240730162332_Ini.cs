using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LRC.Data.Migrations
{
    /// <inheritdoc />
    public partial class Ini : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FluxoCaixa_Caixa_CaixaId",
                table: "FluxoCaixa");

            migrationBuilder.DropForeignKey(
                name: "FK_FluxoCaixa_FormasPagamento_FormaPagamentoId",
                table: "FluxoCaixa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FluxoCaixa",
                table: "FluxoCaixa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Caixa",
                table: "Caixa");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Caixa");

            migrationBuilder.DropColumn(
                name: "ValorInicial",
                table: "Caixa");

            migrationBuilder.RenameTable(
                name: "FluxoCaixa",
                newName: "FluxosCaixa");

            migrationBuilder.RenameTable(
                name: "Caixa",
                newName: "Caixas");

            migrationBuilder.RenameIndex(
                name: "IX_FluxoCaixa_FormaPagamentoId",
                table: "FluxosCaixa",
                newName: "IX_FluxosCaixa_FormaPagamentoId");

            migrationBuilder.RenameIndex(
                name: "IX_FluxoCaixa_CaixaId",
                table: "FluxosCaixa",
                newName: "IX_FluxosCaixa_CaixaId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "FluxosCaixa",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "FluxosCaixa",
                type: "varchar(200)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FluxosCaixa",
                table: "FluxosCaixa",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Caixas",
                table: "Caixas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FluxosCaixa_Caixas_CaixaId",
                table: "FluxosCaixa",
                column: "CaixaId",
                principalTable: "Caixas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FluxosCaixa_FormasPagamento_FormaPagamentoId",
                table: "FluxosCaixa",
                column: "FormaPagamentoId",
                principalTable: "FormasPagamento",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FluxosCaixa_Caixas_CaixaId",
                table: "FluxosCaixa");

            migrationBuilder.DropForeignKey(
                name: "FK_FluxosCaixa_FormasPagamento_FormaPagamentoId",
                table: "FluxosCaixa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FluxosCaixa",
                table: "FluxosCaixa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Caixas",
                table: "Caixas");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "FluxosCaixa");

            migrationBuilder.RenameTable(
                name: "FluxosCaixa",
                newName: "FluxoCaixa");

            migrationBuilder.RenameTable(
                name: "Caixas",
                newName: "Caixa");

            migrationBuilder.RenameIndex(
                name: "IX_FluxosCaixa_FormaPagamentoId",
                table: "FluxoCaixa",
                newName: "IX_FluxoCaixa_FormaPagamentoId");

            migrationBuilder.RenameIndex(
                name: "IX_FluxosCaixa_CaixaId",
                table: "FluxoCaixa",
                newName: "IX_FluxoCaixa_CaixaId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "FluxoCaixa",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5);

            migrationBuilder.AddColumn<long>(
                name: "Numero",
                table: "Caixa",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<decimal>(
                name: "ValorInicial",
                table: "Caixa",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FluxoCaixa",
                table: "FluxoCaixa",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Caixa",
                table: "Caixa",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FluxoCaixa_Caixa_CaixaId",
                table: "FluxoCaixa",
                column: "CaixaId",
                principalTable: "Caixa",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FluxoCaixa_FormasPagamento_FormaPagamentoId",
                table: "FluxoCaixa",
                column: "FormaPagamentoId",
                principalTable: "FormasPagamento",
                principalColumn: "Id");
        }
    }
}
