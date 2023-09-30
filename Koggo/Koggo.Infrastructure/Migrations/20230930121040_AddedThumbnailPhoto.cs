using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Koggo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedThumbnailPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThumbNailPhoto",
                table: "UserServices",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbNailPhoto",
                table: "UserServices");
        }
    }
}
