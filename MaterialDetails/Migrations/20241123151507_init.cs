using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MaterialDetails.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_referencedetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false, computedColumnSql: "'REF-' + FORMAT([Id], '0000')"),
                    ReferenceDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_referencedetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_type",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_unit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_unit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_user", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_user_tbl_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tbl_role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_material",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Consumption = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TypesId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    ReferenceDetailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_material", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_material_tbl_referencedetail_ReferenceDetailId",
                        column: x => x.ReferenceDetailId,
                        principalTable: "tbl_referencedetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_material_tbl_type_TypesId",
                        column: x => x.TypesId,
                        principalTable: "tbl_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_material_tbl_unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "tbl_unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "tbl_role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Employee" },
                    { 3, "User" }
                });

            migrationBuilder.InsertData(
                table: "tbl_type",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Acc0185" },
                    { 2, "Acc0567" },
                    { 3, "Dev0476" },
                    { 4, "ADM6633" }
                });

            migrationBuilder.InsertData(
                table: "tbl_unit",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "NOS" },
                    { 2, "PKT" },
                    { 3, "BOX" },
                    { 4, "ITM" },
                    { 5, "ROL" },
                    { 6, "LTR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_material_ReferenceDetailId",
                table: "tbl_material",
                column: "ReferenceDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_material_TypesId",
                table: "tbl_material",
                column: "TypesId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_material_UnitId",
                table: "tbl_material",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_user_RoleId",
                table: "tbl_user",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_material");

            migrationBuilder.DropTable(
                name: "tbl_user");

            migrationBuilder.DropTable(
                name: "tbl_referencedetail");

            migrationBuilder.DropTable(
                name: "tbl_type");

            migrationBuilder.DropTable(
                name: "tbl_unit");

            migrationBuilder.DropTable(
                name: "tbl_role");
        }
    }
}
