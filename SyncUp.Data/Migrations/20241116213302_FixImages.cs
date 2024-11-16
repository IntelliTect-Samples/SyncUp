using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelliTect.SyncUp.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Tenants_TenantId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_TenantId",
                table: "Images");

            migrationBuilder.AlterColumn<string>(
                name: "BannerImageId",
                table: "Tenants",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvatarImageId",
                table: "Groups",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_TenantId_BannerImageId",
                table: "Tenants",
                columns: new[] { "TenantId", "BannerImageId" },
                unique: true,
                filter: "[BannerImageId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_TenantId_AvatarImageId",
                table: "Groups",
                columns: new[] { "TenantId", "AvatarImageId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Images_TenantId_AvatarImageId",
                table: "Groups",
                columns: new[] { "TenantId", "AvatarImageId" },
                principalTable: "Images",
                principalColumns: new[] { "TenantId", "ImageId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_Images_TenantId_BannerImageId",
                table: "Tenants",
                columns: new[] { "TenantId", "BannerImageId" },
                principalTable: "Images",
                principalColumns: new[] { "TenantId", "ImageId" },
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Images_TenantId_AvatarImageId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_Images_TenantId_BannerImageId",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Tenants_TenantId_BannerImageId",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Groups_TenantId_AvatarImageId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "AvatarImageId",
                table: "Groups");

            migrationBuilder.AlterColumn<string>(
                name: "BannerImageId",
                table: "Tenants",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_TenantId",
                table: "Images",
                column: "TenantId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Tenants_TenantId",
                table: "Images",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
