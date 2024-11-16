using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelliTect.SyncUp.Data.Migrations
{
    /// <inheritdoc />
    public partial class CommentLikeCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LikeCount",
                table: "Comments",
                newName: "CommentLikeCount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CommentLikeCount",
                table: "Comments",
                newName: "LikeCount");
        }
    }
}
