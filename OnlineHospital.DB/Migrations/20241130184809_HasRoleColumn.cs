using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineHospital.DB.Migrations
{
    /// <inheritdoc />
    public partial class HasRoleColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasRole",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasRole",
                table: "AspNetUsers");
        }
    }
}
