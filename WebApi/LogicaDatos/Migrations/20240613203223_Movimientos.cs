using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaDatos.Migrations
{
    /// <inheritdoc />
    public partial class Movimientos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineasDePedido");

            migrationBuilder.DropTable(
                name: "PComunes");

            migrationBuilder.DropTable(
                name: "PExpress");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropSequence(
                name: "PedidoSequence");

            migrationBuilder.AddColumn<int>(
                name: "TipoUsuario",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MovimientosDeStock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticuloDeMovimientoId = table.Column<int>(type: "int", nullable: false),
                    CantidadArticulo = table.Column<int>(type: "int", nullable: false),
                    UsuarioDeMovimientoId = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientosDeStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimientosDeStock_Articulos_ArticuloDeMovimientoId",
                        column: x => x.ArticuloDeMovimientoId,
                        principalTable: "Articulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovimientosDeStock_Usuarios_UsuarioDeMovimientoId",
                        column: x => x.UsuarioDeMovimientoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosDeStock_ArticuloDeMovimientoId",
                table: "MovimientosDeStock",
                column: "ArticuloDeMovimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosDeStock_UsuarioDeMovimientoId",
                table: "MovimientosDeStock",
                column: "UsuarioDeMovimientoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimientosDeStock");

            migrationBuilder.DropColumn(
                name: "TipoUsuario",
                table: "Usuarios");

            migrationBuilder.CreateSequence(
                name: "PedidoSequence");

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoRut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DistanciaKM = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion_Calle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion_Ciudad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion_Numero = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LineasDePedido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticuloId = table.Column<int>(type: "int", nullable: false),
                    CantidadArticulos = table.Column<int>(type: "int", nullable: false),
                    PedidoId = table.Column<int>(type: "int", nullable: true),
                    PrecioVigente = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineasDePedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LineasDePedido_Articulos_ArticuloId",
                        column: x => x.ArticuloId,
                        principalTable: "Articulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PComunes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [PedidoSequence]"),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    Anulado = table.Column<bool>(type: "bit", nullable: false),
                    CostoDelPedido = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaDeEntrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaPedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IVAActual = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PComunes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PComunes_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PExpress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [PedidoSequence]"),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    Anulado = table.Column<bool>(type: "bit", nullable: false),
                    CostoDelPedido = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaDeEntrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaPedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IVAActual = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PExpress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PExpress_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LineasDePedido_ArticuloId",
                table: "LineasDePedido",
                column: "ArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_LineasDePedido_PedidoId",
                table: "LineasDePedido",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_PComunes_ClienteId",
                table: "PComunes",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_PExpress_ClienteId",
                table: "PExpress",
                column: "ClienteId");
        }
    }
}
