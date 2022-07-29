using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartDeviceApi.Migrations
{
    public partial class Version5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProduitsStr",
                table: "Factures",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProduitsStr",
                table: "Factures");
        }
    }
}
