using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineHospital.DB.Migrations
{
    /// <inheritdoc />
    public partial class RemoveForeignKeyInAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpenAppointmentSlots_AspNetUsers_UserId",
                table: "OpenAppointmentSlots");

            migrationBuilder.DropIndex(
                name: "IX_OpenAppointmentSlots_UserId",
                table: "OpenAppointmentSlots");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OpenAppointmentSlots");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "OpenAppointmentSlots",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_OpenAppointmentSlots_UserId",
                table: "OpenAppointmentSlots",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OpenAppointmentSlots_AspNetUsers_UserId",
                table: "OpenAppointmentSlots",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
