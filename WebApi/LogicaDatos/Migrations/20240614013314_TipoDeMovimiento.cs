using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaDatos.Migrations
{
    /// <inheritdoc />
    public partial class TipoDeMovimiento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "MovimientosDeStock",
                newName: "TipoId");

            migrationBuilder.CreateTable(
                name: "TipoDeMovimientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    tipoDeCambioEnStock = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDeMovimientos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosDeStock_TipoId",
                table: "MovimientosDeStock",
                column: "TipoId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoDeMovimientos_Nombre",
                table: "TipoDeMovimientos",
                column: "Nombre",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MovimientosDeStock_TipoDeMovimientos_TipoId",
                table: "MovimientosDeStock",
                column: "TipoId",
                principalTable: "TipoDeMovimientos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovimientosDeStock_TipoDeMovimientos_TipoId",
                table: "MovimientosDeStock");

            migrationBuilder.DropTable(
                name: "TipoDeMovimientos");

            migrationBuilder.DropIndex(
                name: "IX_MovimientosDeStock_TipoId",
                table: "MovimientosDeStock");

            migrationBuilder.RenameColumn(
                name: "TipoId",
                table: "MovimientosDeStock",
                newName: "Tipo");
        }
    }
}
