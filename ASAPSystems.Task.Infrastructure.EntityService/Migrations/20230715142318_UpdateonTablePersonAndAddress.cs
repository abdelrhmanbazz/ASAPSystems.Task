using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASAPSystems.Task.Infrastructure.EntityService.Migrations
{
    /// <inheritdoc />
    public partial class UpdateonTablePersonAndAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Person_PersonId",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_PersonId",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "AddressDetails",
                table: "Address",
                newName: "zip");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Person",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Person_AddressId",
                table: "Person",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Address_AddressId",
                table: "Person",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Address_AddressId",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_AddressId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "zip",
                table: "Address",
                newName: "AddressDetails");

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Address",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Address_PersonId",
                table: "Address",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Person_PersonId",
                table: "Address",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
