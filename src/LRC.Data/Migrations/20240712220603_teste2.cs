using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LRC.Data.Migrations
{
    /// <inheritdoc />
    public partial class teste2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContaReceber_Clientes_ClienteId",
                table: "ContaReceber");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcela_ContaReceber_ContaReceberId",
                table: "Parcela");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContaReceber",
                table: "ContaReceber");

            migrationBuilder.RenameTable(
                name: "ContaReceber",
                newName: "ContasReceber");

            migrationBuilder.RenameIndex(
                name: "IX_ContaReceber_ClienteId",
                table: "ContasReceber",
                newName: "IX_ContasReceber_ClienteId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFechamento",
                table: "ContasPagar",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataVencimento",
                table: "ContasPagar",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "ContasReceber",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Observacao",
                table: "ContasReceber",
                type: "varchar(8000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NumeroDocumento",
                table: "ContasReceber",
                type: "varchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "ContasReceber",
                type: "varchar(200)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataEmissao",
                table: "ContasReceber",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFechamento",
                table: "ContasReceber",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataVencimento",
                table: "ContasReceber",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContasReceber",
                table: "ContasReceber",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContasReceber_Clientes_ClienteId",
                table: "ContasReceber",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcela_ContasReceber_ContaReceberId",
                table: "Parcela",
                column: "ContaReceberId",
                principalTable: "ContasReceber",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContasReceber_Clientes_ClienteId",
                table: "ContasReceber");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcela_ContasReceber_ContaReceberId",
                table: "Parcela");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContasReceber",
                table: "ContasReceber");

            migrationBuilder.DropColumn(
                name: "DataFechamento",
                table: "ContasPagar");

            migrationBuilder.DropColumn(
                name: "DataVencimento",
                table: "ContasPagar");

            migrationBuilder.DropColumn(
                name: "DataFechamento",
                table: "ContasReceber");

            migrationBuilder.DropColumn(
                name: "DataVencimento",
                table: "ContasReceber");

            migrationBuilder.RenameTable(
                name: "ContasReceber",
                newName: "ContaReceber");

            migrationBuilder.RenameIndex(
                name: "IX_ContasReceber_ClienteId",
                table: "ContaReceber",
                newName: "IX_ContaReceber_ClienteId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "ContaReceber",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5);

            migrationBuilder.AlterColumn<string>(
                name: "Observacao",
                table: "ContaReceber",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(8000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NumeroDocumento",
                table: "ContaReceber",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "ContaReceber",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataEmissao",
                table: "ContaReceber",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContaReceber",
                table: "ContaReceber",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContaReceber_Clientes_ClienteId",
                table: "ContaReceber",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcela_ContaReceber_ContaReceberId",
                table: "Parcela",
                column: "ContaReceberId",
                principalTable: "ContaReceber",
                principalColumn: "Id");
        }
    }
}
