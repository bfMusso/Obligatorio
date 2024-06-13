using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaDatos.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "PedidoSequence");

            migrationBuilder.CreateTable(
                name: "Articulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CodigoProveedor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stock = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecioVenta = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articulos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoRut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion_Calle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion_Numero = table.Column<int>(type: "int", nullable: false),
                    Direccion_Ciudad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DistanciaKM = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordEnctriptado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LineasDePedido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticuloId = table.Column<int>(type: "int", nullable: false),
                    CantidadArticulos = table.Column<int>(type: "int", nullable: false),
                    PrecioVigente = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PedidoId = table.Column<int>(type: "int", nullable: true)
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
                    FechaPedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaDeEntrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    TasaIVA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UsuarioDeGestionId = table.Column<int>(type: "int", nullable: false),
                    CostoDelPedido = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PlazoMaximo = table.Column<int>(type: "int", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_PComunes_Usuarios_UsuarioDeGestionId",
                        column: x => x.UsuarioDeGestionId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PExpress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR [PedidoSequence]"),
                    FechaPedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaDeEntrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    TasaIVA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UsuarioDeGestionId = table.Column<int>(type: "int", nullable: false),
                    CostoDelPedido = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PlaxoMaximo = table.Column<int>(type: "int", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_PExpress_Usuarios_UsuarioDeGestionId",
                        column: x => x.UsuarioDeGestionId,
                        principalTable: "Usuarios",
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
                name: "IX_PComunes_UsuarioDeGestionId",
                table: "PComunes",
                column: "UsuarioDeGestionId");

            migrationBuilder.CreateIndex(
                name: "IX_PExpress_ClienteId",
                table: "PExpress",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_PExpress_UsuarioDeGestionId",
                table: "PExpress",
                column: "UsuarioDeGestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineasDePedido");

            migrationBuilder.DropTable(
                name: "PComunes");

            migrationBuilder.DropTable(
                name: "PExpress");

            migrationBuilder.DropTable(
                name: "Articulos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropSequence(
                name: "PedidoSequence");
        }
    }
}
