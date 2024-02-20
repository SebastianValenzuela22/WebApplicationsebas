using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationsebas.Migrations
{
    /// <inheritdoc />
    public partial class Primera : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conductores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreConductor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AñoExpiraLicencia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conductores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Automoviles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Modelo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Año = table.Column<int>(type: "int", nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    conductorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Automoviles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Automoviles_Conductores_conductorId",
                        column: x => x.conductorId,
                        principalTable: "Conductores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Automoviles_conductorId",
                table: "Automoviles",
                column: "conductorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Automoviles");

            migrationBuilder.DropTable(
                name: "Conductores");
        }
    }
}
