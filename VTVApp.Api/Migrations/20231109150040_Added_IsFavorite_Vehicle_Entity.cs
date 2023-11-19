using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VTVApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class Added_IsFavorite_Vehicle_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "Vehicles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "Vehicles");
        }
    }
}
