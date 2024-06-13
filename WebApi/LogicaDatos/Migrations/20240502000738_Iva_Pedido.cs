using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaDatos.Migrations
{
    /// <inheritdoc />
    public partial class Iva_Pedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TasaIVA",
                table: "PExpress",
                newName: "IVAActual");

            migrationBuilder.RenameColumn(
                name: "TasaIVA",
                table: "PComunes",
                newName: "IVAActual");

            migrationBuilder.CreateTable(
                name: "Impuestos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Impuestos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Impuestos");

            migrationBuilder.RenameColumn(
                name: "IVAActual",
                table: "PExpress",
                newName: "TasaIVA");

            migrationBuilder.RenameColumn(
                name: "IVAActual",
                table: "PComunes",
                newName: "TasaIVA");
        }
    }
}
