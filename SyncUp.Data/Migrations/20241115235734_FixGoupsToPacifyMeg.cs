using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelliTect.SyncUp.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixGoupsToPacifyMeg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Groups_Id",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Groups",
                newName: "GroupId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Groups_GroupId",
                table: "Groups",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Groups_GroupId",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "Groups",
                newName: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Groups_Id",
                table: "Groups",
                column: "Id");
        }
    }
}
