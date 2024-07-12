using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LRC.Data.Migrations
{
    /// <inheritdoc />
    public partial class fornecedores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContaPagar_Fornecedor_FornecedorId",
                table: "ContaPagar");

            migrationBuilder.DropForeignKey(
                name: "FK_ContaReceber_Fornecedor_FornecedorId",
                table: "ContaReceber");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fornecedor",
                table: "Fornecedor");

            migrationBuilder.RenameTable(
                name: "Fornecedor",
                newName: "Fornecedores");

            migrationBuilder.RenameColumn(
                name: "Endereco_Referencia",
                table: "Fornecedores",
                newName: "Referencia");

            migrationBuilder.RenameColumn(
                name: "Endereco_Numero",
                table: "Fornecedores",
                newName: "Numero");

            migrationBuilder.RenameColumn(
                name: "Endereco_Logradouro",
                table: "Fornecedores",
                newName: "Logradouro");

            migrationBuilder.RenameColumn(
                name: "Endereco_Complemento",
                table: "Fornecedores",
                newName: "Complemento");

            migrationBuilder.RenameColumn(
                name: "Endereco_Bairro",
                table: "Fornecedores",
                newName: "Bairro");

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Fornecedores",
                type: "varchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RazaoSocial",
                table: "Fornecedores",
                type: "varchar(200)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NomeFantasia",
                table: "Fornecedores",
                type: "varchar(200)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Documento2",
                table: "Fornecedores",
                type: "varchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Celular2",
                table: "Fornecedores",
                type: "varchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Celular",
                table: "Fornecedores",
                type: "varchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Referencia",
                table: "Fornecedores",
                type: "varchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "Fornecedores",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Complemento",
                table: "Fornecedores",
                type: "varchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Bairro",
                table: "Fornecedores",
                type: "varchar(70)",
                maxLength: 70,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fornecedores",
                table: "Fornecedores",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContaPagar_Fornecedores_FornecedorId",
                table: "ContaPagar",
                column: "FornecedorId",
                principalTable: "Fornecedores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContaReceber_Fornecedores_FornecedorId",
                table: "ContaReceber",
                column: "FornecedorId",
                principalTable: "Fornecedores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContaPagar_Fornecedores_FornecedorId",
                table: "ContaPagar");

            migrationBuilder.DropForeignKey(
                name: "FK_ContaReceber_Fornecedores_FornecedorId",
                table: "ContaReceber");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fornecedores",
                table: "Fornecedores");

            migrationBuilder.RenameTable(
                name: "Fornecedores",
                newName: "Fornecedor");

            migrationBuilder.RenameColumn(
                name: "Referencia",
                table: "Fornecedor",
                newName: "Endereco_Referencia");

            migrationBuilder.RenameColumn(
                name: "Numero",
                table: "Fornecedor",
                newName: "Endereco_Numero");

            migrationBuilder.RenameColumn(
                name: "Logradouro",
                table: "Fornecedor",
                newName: "Endereco_Logradouro");

            migrationBuilder.RenameColumn(
                name: "Complemento",
                table: "Fornecedor",
                newName: "Endereco_Complemento");

            migrationBuilder.RenameColumn(
                name: "Bairro",
                table: "Fornecedor",
                newName: "Endereco_Bairro");

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Fornecedor",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RazaoSocial",
                table: "Fornecedor",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)");

            migrationBuilder.AlterColumn<string>(
                name: "NomeFantasia",
                table: "Fornecedor",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Documento2",
                table: "Fornecedor",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Celular2",
                table: "Fornecedor",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Celular",
                table: "Fornecedor",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Endereco_Referencia",
                table: "Fornecedor",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Endereco_Numero",
                table: "Fornecedor",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Endereco_Complemento",
                table: "Fornecedor",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Endereco_Bairro",
                table: "Fornecedor",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(70)",
                oldMaxLength: 70,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fornecedor",
                table: "Fornecedor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContaPagar_Fornecedor_FornecedorId",
                table: "ContaPagar",
                column: "FornecedorId",
                principalTable: "Fornecedor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContaReceber_Fornecedor_FornecedorId",
                table: "ContaReceber",
                column: "FornecedorId",
                principalTable: "Fornecedor",
                principalColumn: "Id");
        }
    }
}
