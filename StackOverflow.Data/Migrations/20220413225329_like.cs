using Microsoft.EntityFrameworkCore.Migrations;

namespace StackOverflow.Data.Migrations
{
    public partial class like : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LikedId",
                table: "Questions",
                newName: "LikeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LikeId",
                table: "Questions",
                newName: "LikedId");
        }
    }
}
