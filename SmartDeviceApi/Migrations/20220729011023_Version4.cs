using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartDeviceApi.Migrations
{
    public partial class Version4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProduitsStr",
                table: "Factures");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProduitsStr",
                table: "Factures",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
