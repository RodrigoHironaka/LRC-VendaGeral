using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LRC.Data.Migrations
{
    /// <inheritdoc />
    public partial class contapagar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContaPagar_Clientes_ClienteId",
                table: "ContaPagar");

            migrationBuilder.DropForeignKey(
                name: "FK_ContaPagar_Fornecedores_FornecedorId",
                table: "ContaPagar");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcela_ContaPagar_ContaPagarId",
                table: "Parcela");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContaPagar",
                table: "ContaPagar");

            migrationBuilder.DropIndex(
                name: "IX_ContaPagar_ClienteId",
                table: "ContaPagar");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "ContaPagar");

            migrationBuilder.RenameTable(
                name: "ContaPagar",
                newName: "ContasPagar");

            migrationBuilder.RenameIndex(
                name: "IX_ContaPagar_FornecedorId",
                table: "ContasPagar",
                newName: "IX_ContasPagar_FornecedorId");

            migrationBuilder.AlterColumn<string>(
                name: "Observacao",
                table: "ContasPagar",
                type: "varchar(8000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NumeroDocumento",
                table: "ContasPagar",
                type: "varchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "ContasPagar",
                type: "varchar(200)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContasPagar",
                table: "ContasPagar",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContasPagar_Fornecedores_FornecedorId",
                table: "ContasPagar",
                column: "FornecedorId",
                principalTable: "Fornecedores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcela_ContasPagar_ContaPagarId",
                table: "Parcela",
                column: "ContaPagarId",
                principalTable: "ContasPagar",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContasPagar_Fornecedores_FornecedorId",
                table: "ContasPagar");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcela_ContasPagar_ContaPagarId",
                table: "Parcela");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContasPagar",
                table: "ContasPagar");

            migrationBuilder.RenameTable(
                name: "ContasPagar",
                newName: "ContaPagar");

            migrationBuilder.RenameIndex(
                name: "IX_ContasPagar_FornecedorId",
                table: "ContaPagar",
                newName: "IX_ContaPagar_FornecedorId");

            migrationBuilder.AlterColumn<string>(
                name: "Observacao",
                table: "ContaPagar",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(8000)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NumeroDocumento",
                table: "ContaPagar",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "ContaPagar",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)");

            migrationBuilder.AddColumn<Guid>(
                name: "ClienteId",
                table: "ContaPagar",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContaPagar",
                table: "ContaPagar",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ContaPagar_ClienteId",
                table: "ContaPagar",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContaPagar_Clientes_ClienteId",
                table: "ContaPagar",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContaPagar_Fornecedores_FornecedorId",
                table: "ContaPagar",
                column: "FornecedorId",
                principalTable: "Fornecedores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcela_ContaPagar_ContaPagarId",
                table: "Parcela",
                column: "ContaPagarId",
                principalTable: "ContaPagar",
                principalColumn: "Id");
        }
    }
}
