using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoPermisos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPermisos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreEmpleado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApellidosEmpleado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaPermiso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoPermisoId = table.Column<int>(type: "int", nullable: false),
                    TipoPermisoId1 = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permisos_TipoPermisos_TipoPermisoId",
                        column: x => x.TipoPermisoId,
                        principalTable: "TipoPermisos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Permisos_TipoPermisos_TipoPermisoId1",
                        column: x => x.TipoPermisoId1,
                        principalTable: "TipoPermisos",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "TipoPermisos",
                columns: new[] { "Id", "CreatedAt", "Deleted", "Descripcion", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 16, 20, 26, 31, 720, DateTimeKind.Utc).AddTicks(6722), null, "Enfermedad", null },
                    { 2, new DateTime(2024, 6, 16, 20, 26, 31, 720, DateTimeKind.Utc).AddTicks(6723), null, "Diligencias", null },
                    { 3, new DateTime(2024, 6, 16, 20, 26, 31, 720, DateTimeKind.Utc).AddTicks(6725), null, "Otros", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_TipoPermisoId",
                table: "Permisos",
                column: "TipoPermisoId");

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_TipoPermisoId1",
                table: "Permisos",
                column: "TipoPermisoId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "TipoPermisos");
        }
    }
}
