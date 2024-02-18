using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Management.Migrations
{
    /// <inheritdoc />
    public partial class addtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "F_Artwork",
                columns: table => new
                {
                    Artwork_ID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Artwork_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Category_ID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageLocalPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_F_Artwork", x => x.Artwork_ID);
                });

            migrationBuilder.CreateTable(
                name: "F_Configuration",
                columns: table => new
                {
                    Configuration_ID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Commision_Fee = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Applied_Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_F_Configuration", x => x.Configuration_ID);
                });

            migrationBuilder.CreateTable(
                name: "F_Package",
                columns: table => new
                {
                    Package_ID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Package_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Maximum_Artworks = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(18,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_F_Package", x => x.Package_ID);
                });

            migrationBuilder.CreateTable(
                name: "F_Post",
                columns: table => new
                {
                    Post_ID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Artwork_ID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Tittle = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Created_On = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_F_Post", x => x.Post_ID);
                    table.ForeignKey(
                        name: "FK_F_Post_F_Artwork",
                        column: x => x.Artwork_ID,
                        principalTable: "F_Artwork",
                        principalColumn: "Artwork_ID");
                });

            migrationBuilder.CreateTable(
                name: "F_Report",
                columns: table => new
                {
                    Report_ID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created_On = table.Column<DateTime>(type: "datetime", nullable: true),
                    Created_By = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Artwork_ID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Detail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Action_Note = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_F_Report", x => x.Report_ID);
                    table.ForeignKey(
                        name: "FK_F_Report_F_Artwork",
                        column: x => x.Artwork_ID,
                        principalTable: "F_Artwork",
                        principalColumn: "Artwork_ID");
                });

            migrationBuilder.CreateTable(
                name: "D_PackageOfCreator",
                columns: table => new
                {
                    Package_ID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Expired_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Grace_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_D_PackageOfCreator", x => new { x.Package_ID, x.ID });
                    table.ForeignKey(
                        name: "FK_D_PackageOfCreator_F_Package",
                        column: x => x.Package_ID,
                        principalTable: "F_Package",
                        principalColumn: "Package_ID");
                });

            migrationBuilder.CreateTable(
                name: "D_Interaction",
                columns: table => new
                {
                    Interaction_ID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Created_On = table.Column<DateTime>(type: "datetime", nullable: true),
                    Like = table.Column<int>(type: "int", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Post_ID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_D_Interaction", x => x.Interaction_ID);
                    table.ForeignKey(
                        name: "FK_D_Interaction_F_Post",
                        column: x => x.Post_ID,
                        principalTable: "F_Post",
                        principalColumn: "Post_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_D_Interaction_Post_ID",
                table: "D_Interaction",
                column: "Post_ID");

            migrationBuilder.CreateIndex(
                name: "IX_F_Post_Artwork_ID",
                table: "F_Post",
                column: "Artwork_ID");

            migrationBuilder.CreateIndex(
                name: "IX_F_Report_Artwork_ID",
                table: "F_Report",
                column: "Artwork_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "D_Interaction");

            migrationBuilder.DropTable(
                name: "D_PackageOfCreator");

            migrationBuilder.DropTable(
                name: "F_Configuration");

            migrationBuilder.DropTable(
                name: "F_Report");

            migrationBuilder.DropTable(
                name: "F_Post");

            migrationBuilder.DropTable(
                name: "F_Package");

            migrationBuilder.DropTable(
                name: "F_Artwork");
        }
    }
}
