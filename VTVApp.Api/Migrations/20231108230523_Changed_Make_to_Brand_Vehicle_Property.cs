using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VTVApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class Changed_Make_to_Brand_Vehicle_Property : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Vehicles",
                newName: "Brand");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Brand",
                table: "Vehicles",
                newName: "Name");
        }
    }
}
