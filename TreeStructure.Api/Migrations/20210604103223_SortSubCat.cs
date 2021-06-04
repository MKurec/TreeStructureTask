using Microsoft.EntityFrameworkCore.Migrations;

namespace TreeStructure.Api.Migrations
{
    public partial class SortSubCat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SortSubCategoriesInDecending",
                table: "Categories",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortSubCategoriesInDecending",
                table: "Categories");
        }
    }
}
