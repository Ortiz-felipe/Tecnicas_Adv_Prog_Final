﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VTVApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class Added_InspectionStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Inspections",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Inspections");
        }
    }
}
