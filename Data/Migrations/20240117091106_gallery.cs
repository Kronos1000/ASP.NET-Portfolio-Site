using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatrickWare.Data.Migrations
{
    public partial class gallery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GalleryImage1FileName",
                table: "PreviousExperience",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GalleryImage2FileName",
                table: "PreviousExperience",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GalleryImage3FileName",
                table: "PreviousExperience",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GalleryImage4FileName",
                table: "PreviousExperience",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GalleryImage5FileName",
                table: "PreviousExperience",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GalleryImage1FileName",
                table: "PreviousExperience");

            migrationBuilder.DropColumn(
                name: "GalleryImage2FileName",
                table: "PreviousExperience");

            migrationBuilder.DropColumn(
                name: "GalleryImage3FileName",
                table: "PreviousExperience");

            migrationBuilder.DropColumn(
                name: "GalleryImage4FileName",
                table: "PreviousExperience");

            migrationBuilder.DropColumn(
                name: "GalleryImage5FileName",
                table: "PreviousExperience");
        }
    }
}
