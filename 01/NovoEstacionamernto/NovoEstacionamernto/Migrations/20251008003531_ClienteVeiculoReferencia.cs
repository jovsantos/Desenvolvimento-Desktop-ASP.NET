using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovoEstacionamernto.Migrations
{
    /// <inheritdoc />
    public partial class ClienteVeiculoReferencia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_ClienteId",
                table: "Veiculos",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculos_Clientes_ClienteId",
                table: "Veiculos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veiculos_Clientes_ClienteId",
                table: "Veiculos");

            migrationBuilder.DropIndex(
                name: "IX_Veiculos_ClienteId",
                table: "Veiculos");
        }
    }
}
