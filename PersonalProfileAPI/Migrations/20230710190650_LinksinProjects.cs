using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalProfileAPI.Migrations
{
    /// <inheritdoc />
    public partial class LinksinProjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "Projects");
        }
    }
}
