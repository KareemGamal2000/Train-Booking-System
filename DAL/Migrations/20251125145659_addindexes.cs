using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class addindexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StationNameEN",
                table: "Stations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "StationNameAR",
                table: "Stations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_TripStops_TripID_StationID",
                table: "TripStops",
                columns: new[] { "TripID", "StationID" });

            migrationBuilder.CreateIndex(
                name: "IX_Stations_StationNameAR",
                table: "Stations",
                column: "StationNameAR");

            migrationBuilder.CreateIndex(
                name: "IX_Stations_StationNameEN",
                table: "Stations",
                column: "StationNameEN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TripStops_TripID_StationID",
                table: "TripStops");

            migrationBuilder.DropIndex(
                name: "IX_Stations_StationNameAR",
                table: "Stations");

            migrationBuilder.DropIndex(
                name: "IX_Stations_StationNameEN",
                table: "Stations");

            migrationBuilder.AlterColumn<string>(
                name: "StationNameEN",
                table: "Stations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "StationNameAR",
                table: "Stations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
