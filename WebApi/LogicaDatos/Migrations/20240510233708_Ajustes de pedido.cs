using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaDatos.Migrations
{
    /// <inheritdoc />
    public partial class Ajustesdepedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlazoMaximo",
                table: "PExpress");

            migrationBuilder.DropColumn(
                name: "PlazoMaximo",
                table: "PComunes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlazoMaximo",
                table: "PExpress",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlazoMaximo",
                table: "PComunes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
