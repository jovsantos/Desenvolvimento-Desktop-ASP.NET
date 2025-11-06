using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovoEstacionamernto.Migrations
{
    /// <inheritdoc />
    public partial class AlteracaoClienteVeiculo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cor",
                table: "Veiculos",
                newName: "Cores");

            migrationBuilder.AddColumn<string>(
                name: "Apelido",
                table: "Clientes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apelido",
                table: "Clientes");

            migrationBuilder.RenameColumn(
                name: "Cores",
                table: "Veiculos",
                newName: "Cor");
        }
    }
}
