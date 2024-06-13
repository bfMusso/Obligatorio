using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaDatos.Migrations
{
    /// <inheritdoc />
    public partial class SeeliminaUsuariodegestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PComunes_Usuarios_UsuarioDeGestionId",
                table: "PComunes");

            migrationBuilder.DropForeignKey(
                name: "FK_PExpress_Usuarios_UsuarioDeGestionId",
                table: "PExpress");

            migrationBuilder.DropIndex(
                name: "IX_PExpress_UsuarioDeGestionId",
                table: "PExpress");

            migrationBuilder.DropIndex(
                name: "IX_PComunes_UsuarioDeGestionId",
                table: "PComunes");

            migrationBuilder.DropColumn(
                name: "UsuarioDeGestionId",
                table: "PExpress");

            migrationBuilder.DropColumn(
                name: "UsuarioDeGestionId",
                table: "PComunes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioDeGestionId",
                table: "PExpress",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioDeGestionId",
                table: "PComunes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PExpress_UsuarioDeGestionId",
                table: "PExpress",
                column: "UsuarioDeGestionId");

            migrationBuilder.CreateIndex(
                name: "IX_PComunes_UsuarioDeGestionId",
                table: "PComunes",
                column: "UsuarioDeGestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PComunes_Usuarios_UsuarioDeGestionId",
                table: "PComunes",
                column: "UsuarioDeGestionId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PExpress_Usuarios_UsuarioDeGestionId",
                table: "PExpress",
                column: "UsuarioDeGestionId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
