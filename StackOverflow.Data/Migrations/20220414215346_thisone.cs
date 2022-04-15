using Microsoft.EntityFrameworkCore.Migrations;

namespace StackOverflow.Data.Migrations
{
    public partial class thisone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Questions_QuestionId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_QuestionId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "LikeId",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "text",
                table: "Tags",
                newName: "Name");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Questions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tags",
                newName: "text");

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "Tags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LikeId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_QuestionId",
                table: "Tags",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Questions_QuestionId",
                table: "Tags",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
