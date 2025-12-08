using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AddTripSearchIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Stations_NameSearch",
                table: "Stations",
                columns: new[] { "StationNameAR", "StationNameEN" });

            migrationBuilder.CreateIndex(
                name: "IX_TripStops_TripSequence",
                table: "TripStops",
                columns: new[] { "TripID", "StopSequence" });

            migrationBuilder.CreateIndex(
                name: "IX_TripStops_StationID_Optimized",
                table: "TripStops",
                column: "StationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stations_NameSearch",
                table: "Stations");

            migrationBuilder.DropIndex(
                name: "IX_TripStops_TripSequence",
                table: "TripStops");

            migrationBuilder.DropIndex(
                name: "IX_TripStops_StationID_Optimized",
                table: "TripStops");
        }
    }
}