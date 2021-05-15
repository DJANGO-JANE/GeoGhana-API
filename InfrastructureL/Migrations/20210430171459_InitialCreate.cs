using Microsoft.EntityFrameworkCore.Migrations;

namespace InfrastructureL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegionsGH",
                columns: table => new
                {
                    RegionCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CapitalCity = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionsGH", x => x.RegionCode);
                });

            migrationBuilder.CreateTable(
                name: "CitiesGH",
                columns: table => new
                {
                    CityCode = table.Column<int>(type: "int", maxLength: 2, nullable: false)
                        .Annotation("SqlServer:Identity", "11, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RegionName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RegionCode = table.Column<string>(type: "nvarchar(3)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitiesGH", x => x.CityCode);
                    table.ForeignKey(
                        name: "FK_CitiesGH_RegionsGH_RegionCode",
                        column: x => x.RegionCode,
                        principalTable: "RegionsGH",
                        principalColumn: "RegionCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocalitiesGH",
                columns: table => new
                {
                    ConstID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RegionName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LocalityCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    RegionCode = table.Column<string>(type: "nvarchar(3)", nullable: true),
                    CityCode = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalitiesGH", x => x.ConstID);
                    table.ForeignKey(
                        name: "FK_LocalitiesGH_CitiesGH_CityCode",
                        column: x => x.CityCode,
                        principalTable: "CitiesGH",
                        principalColumn: "CityCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LocalitiesGH_RegionsGH_RegionCode",
                        column: x => x.RegionCode,
                        principalTable: "RegionsGH",
                        principalColumn: "RegionCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CitiesGH_RegionCode",
                table: "CitiesGH",
                column: "RegionCode");

            migrationBuilder.CreateIndex(
                name: "IX_LocalitiesGH_CityCode",
                table: "LocalitiesGH",
                column: "CityCode");

            migrationBuilder.CreateIndex(
                name: "IX_LocalitiesGH_RegionCode",
                table: "LocalitiesGH",
                column: "RegionCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalitiesGH");

            migrationBuilder.DropTable(
                name: "CitiesGH");

            migrationBuilder.DropTable(
                name: "RegionsGH");
        }
    }
}
