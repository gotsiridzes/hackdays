using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Koggo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class relationsref : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserServices_ServiceId",
                table: "UserServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserServices_UserId",
                table: "UserServices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserServiceId",
                table: "Reservations",
                column: "UserServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_UserServices_UserServiceId",
                table: "Reservations",
                column: "UserServiceId",
                principalTable: "UserServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserServices_Services_ServiceId",
                table: "UserServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserServices_Users_UserId",
                table: "UserServices",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_UserServices_UserServiceId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserServices_Services_ServiceId",
                table: "UserServices");

            migrationBuilder.DropForeignKey(
                name: "FK_UserServices_Users_UserId",
                table: "UserServices");

            migrationBuilder.DropIndex(
                name: "IX_UserServices_ServiceId",
                table: "UserServices");

            migrationBuilder.DropIndex(
                name: "IX_UserServices_UserId",
                table: "UserServices");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_UserServiceId",
                table: "Reservations");
        }
    }
}
