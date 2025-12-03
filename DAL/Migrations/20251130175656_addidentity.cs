using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class addidentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Seats_SeatID",
                table: "Tickets");

            // Drop the unique index on SeatID in Tickets table
            migrationBuilder.DropIndex(
                name: "IX_Tickets_SeatID",
                table: "Tickets");

            // Drop primary key on Seats
            migrationBuilder.DropPrimaryKey(
                name: "PK_Seats",
                table: "Seats");

            // Drop and recreate the SeatID column with IDENTITY
            migrationBuilder.DropColumn(
                name: "SeatID",
                table: "Seats");

            migrationBuilder.AddColumn<int>(
                name: "SeatID",
                table: "Seats",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            // Recreate primary key
            migrationBuilder.AddPrimaryKey(
                name: "PK_Seats",
                table: "Seats",
                column: "SeatID");

            // Recreate the index on Tickets
            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SeatID",
                table: "Tickets",
                column: "SeatID",
                unique: true);

            // Recreate the foreign key
            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Seats_SeatID",
                table: "Tickets",
                column: "SeatID",
                principalTable: "Seats",
                principalColumn: "SeatID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Seats_SeatID",
                table: "Tickets");

            // Drop the index
            migrationBuilder.DropIndex(
                name: "IX_Tickets_SeatID",
                table: "Tickets");

            // Drop primary key
            migrationBuilder.DropPrimaryKey(
                name: "PK_Seats",
                table: "Seats");

            // Drop and recreate column without identity
            migrationBuilder.DropColumn(
                name: "SeatID",
                table: "Seats");

            migrationBuilder.AddColumn<int>(
                name: "SeatID",
                table: "Seats",
                type: "int",
                nullable: false);

            // Recreate primary key
            migrationBuilder.AddPrimaryKey(
                name: "PK_Seats",
                table: "Seats",
                column: "SeatID");

            // Recreate index
            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SeatID",
                table: "Tickets",
                column: "SeatID",
                unique: true);

            // Recreate foreign key
            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Seats_SeatID",
                table: "Tickets",
                column: "SeatID",
                principalTable: "Seats",
                principalColumn: "SeatID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
