using Microsoft.EntityFrameworkCore.Migrations;

namespace RedemptionNews.Migrations
{
    public partial class emailField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "SubComments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "MainComments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "SubComments");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "MainComments");
        }
    }
}
