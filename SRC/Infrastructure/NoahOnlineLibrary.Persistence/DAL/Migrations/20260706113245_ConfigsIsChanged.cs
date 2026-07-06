using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoahOnlineLibrary.Persistence.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ConfigsIsChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationStatus",
                table: "Books");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReservationStatus",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
