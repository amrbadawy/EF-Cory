using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCory.EntityFramework.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "BLOG");

            migrationBuilder.EnsureSchema(
                name: "KPI");

            migrationBuilder.CreateTable(
                name: "Blogs",
                schema: "BLOG",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KpiAnnualItems",
                schema: "KPI",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpiAnnualItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KPIs",
                schema: "KPI",
                columns: table => new
                {
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KPIs", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                schema: "BLOG",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                schema: "BLOG",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    BlogId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalSchema: "BLOG",
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KpiItems",
                schema: "KPI",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KpiAnnualItemId = table.Column<int>(type: "int", nullable: false),
                    KpiCode = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpiItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KpiItems_KpiAnnualItems_KpiAnnualItemId",
                        column: x => x.KpiAnnualItemId,
                        principalSchema: "KPI",
                        principalTable: "KpiAnnualItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KpiItems_KPIs_KpiCode",
                        column: x => x.KpiCode,
                        principalSchema: "KPI",
                        principalTable: "KPIs",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostTags",
                schema: "BLOG",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTags", x => new { x.PostId, x.TagId });
                    table.ForeignKey(
                        name: "FK_PostTags_Posts_PostId",
                        column: x => x.PostId,
                        principalSchema: "BLOG",
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTags_Tags_TagId",
                        column: x => x.TagId,
                        principalSchema: "BLOG",
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_Name",
                schema: "BLOG",
                table: "Blogs",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KpiAnnualItems_Year",
                schema: "KPI",
                table: "KpiAnnualItems",
                column: "Year",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KpiItems_KpiAnnualItemId_KpiCode",
                schema: "KPI",
                table: "KpiItems",
                columns: new[] { "KpiAnnualItemId", "KpiCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KpiItems_KpiCode",
                schema: "KPI",
                table: "KpiItems",
                column: "KpiCode");

            migrationBuilder.CreateIndex(
                name: "IX_KPIs_Name",
                schema: "KPI",
                table: "KPIs",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_BlogId",
                schema: "BLOG",
                table: "Posts",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_Title",
                schema: "BLOG",
                table: "Posts",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostTags_TagId",
                schema: "BLOG",
                table: "PostTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Name",
                schema: "BLOG",
                table: "Tags",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KpiItems",
                schema: "KPI");

            migrationBuilder.DropTable(
                name: "PostTags",
                schema: "BLOG");

            migrationBuilder.DropTable(
                name: "KpiAnnualItems",
                schema: "KPI");

            migrationBuilder.DropTable(
                name: "KPIs",
                schema: "KPI");

            migrationBuilder.DropTable(
                name: "Posts",
                schema: "BLOG");

            migrationBuilder.DropTable(
                name: "Tags",
                schema: "BLOG");

            migrationBuilder.DropTable(
                name: "Blogs",
                schema: "BLOG");
        }
    }
}
