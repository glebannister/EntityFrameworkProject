using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkProject.Migrations
{
    /// <inheritdoc />
    public partial class FixedAddresstypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Adress",
                table: "Manufactures",
                newName: "Address");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Manufactures",
                newName: "Adress");
        }
    }
}
