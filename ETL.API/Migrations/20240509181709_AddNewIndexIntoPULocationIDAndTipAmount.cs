using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETL.API.Migrations
{
    /// <inheritdoc />
    public partial class AddNewIndexIntoPULocationIDAndTipAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ETLData_PULocationID_TipAmount",
                table: "ETLData",
                columns: new[] { "PULocationID", "TipAmount" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ETLData_PULocationID_TipAmount",
                table: "ETLData");
        }
    }
}
