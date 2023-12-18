using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatrickWare.Data.Migrations
{
    public partial class updateProjectExperienceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortProjectDescription",
                table: "PreviousExperience",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortProjectDescription",
                table: "PreviousExperience");
        }
    }
}
