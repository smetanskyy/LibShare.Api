using Microsoft.EntityFrameworkCore.Migrations;

namespace LibShare.Api.Migrations
{
    public partial class addfieldDeletionReason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeletionReason",
                table: "UserProfile",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletionReason",
                table: "tblCategories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletionReason",
                table: "tblBooks",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletionReason",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "DeletionReason",
                table: "tblCategories");

            migrationBuilder.DropColumn(
                name: "DeletionReason",
                table: "tblBooks");
        }
    }
}
