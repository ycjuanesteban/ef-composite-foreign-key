using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForeignKeyComposite.api.Migrations
{
    /// <inheritdoc />
    public partial class MyFirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entity01",
                columns: table => new
                {
                    Key01 = table.Column<int>(type: "int", nullable: false),
                    Key02 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entity01", x => new { x.Key01, x.Key02 });
                });

            migrationBuilder.CreateTable(
                name: "Entity02",
                columns: table => new
                {
                    Key01 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Foreign01 = table.Column<int>(type: "int", nullable: false),
                    Foreign02 = table.Column<int>(type: "int", nullable: false),
                    OtherProperty = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entity02", x => new { x.Key01, x.Foreign02, x.Foreign01 });
                    table.ForeignKey(
                        name: "FK_Entity02_Entity01_Foreign01_Foreign02",
                        columns: x => new { x.Foreign01, x.Foreign02 },
                        principalTable: "Entity01",
                        principalColumns: new[] { "Key01", "Key02" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entity02_Foreign01_Foreign02",
                table: "Entity02",
                columns: new[] { "Foreign01", "Foreign02" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entity02");

            migrationBuilder.DropTable(
                name: "Entity01");
        }
    }
}
