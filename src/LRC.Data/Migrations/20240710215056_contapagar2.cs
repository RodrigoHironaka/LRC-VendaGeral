using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LRC.Data.Migrations
{
    /// <inheritdoc />
    public partial class contapagar2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContaReceber_Fornecedores_FornecedorId",
                table: "ContaReceber");

            migrationBuilder.DropIndex(
                name: "IX_ContaReceber_FornecedorId",
                table: "ContaReceber");

            migrationBuilder.DropColumn(
                name: "FornecedorId",
                table: "ContaReceber");

            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "Produtos",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<decimal>(
                name: "Valor",
                table: "ContasPagar",
                type: "decimal(10,5)",
                precision: 10,
                scale: 5,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Valor",
                table: "ContaReceber",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Valor",
                table: "ContasPagar");

            migrationBuilder.DropColumn(
                name: "Valor",
                table: "ContaReceber");

            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "Produtos",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,5)",
                oldPrecision: 10,
                oldScale: 5);

            migrationBuilder.AddColumn<Guid>(
                name: "FornecedorId",
                table: "ContaReceber",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ContaReceber_FornecedorId",
                table: "ContaReceber",
                column: "FornecedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContaReceber_Fornecedores_FornecedorId",
                table: "ContaReceber",
                column: "FornecedorId",
                principalTable: "Fornecedores",
                principalColumn: "Id");
        }
    }
}
