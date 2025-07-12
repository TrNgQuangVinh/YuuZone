using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddGoogleExternalLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentVote_Comments_CommentId",
                table: "CommentVote");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentVote_Users_UserId",
                table: "CommentVote");

            migrationBuilder.DropForeignKey(
                name: "FK_PostVote_Posts_PostId",
                table: "PostVote");

            migrationBuilder.DropForeignKey(
                name: "FK_PostVote_Users_UserId",
                table: "PostVote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostVote",
                table: "PostVote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentVote",
                table: "CommentVote");

            migrationBuilder.RenameTable(
                name: "PostVote",
                newName: "PostVotes");

            migrationBuilder.RenameTable(
                name: "CommentVote",
                newName: "CommentVotes");

            migrationBuilder.RenameIndex(
                name: "IX_PostVote_PostId",
                table: "PostVotes",
                newName: "IX_PostVotes_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentVote_CommentId",
                table: "CommentVotes",
                newName: "IX_CommentVotes_CommentId");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "GoogleId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsGoogle",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "ImageIcon",
                table: "Communities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ImageBanner",
                table: "Communities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostVotes",
                table: "PostVotes",
                columns: new[] { "UserId", "PostId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentVotes",
                table: "CommentVotes",
                columns: new[] { "UserId", "CommentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CommentVotes_Comments_CommentId",
                table: "CommentVotes",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentVotes_Users_UserId",
                table: "CommentVotes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostVotes_Posts_PostId",
                table: "PostVotes",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostVotes_Users_UserId",
                table: "PostVotes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentVotes_Comments_CommentId",
                table: "CommentVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentVotes_Users_UserId",
                table: "CommentVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_PostVotes_Posts_PostId",
                table: "PostVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_PostVotes_Users_UserId",
                table: "PostVotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostVotes",
                table: "PostVotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentVotes",
                table: "CommentVotes");

            migrationBuilder.DropColumn(
                name: "GoogleId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsGoogle",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "PostVotes",
                newName: "PostVote");

            migrationBuilder.RenameTable(
                name: "CommentVotes",
                newName: "CommentVote");

            migrationBuilder.RenameIndex(
                name: "IX_PostVotes_PostId",
                table: "PostVote",
                newName: "IX_PostVote_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentVotes_CommentId",
                table: "CommentVote",
                newName: "IX_CommentVote_CommentId");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageIcon",
                table: "Communities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageBanner",
                table: "Communities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostVote",
                table: "PostVote",
                columns: new[] { "UserId", "PostId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentVote",
                table: "CommentVote",
                columns: new[] { "UserId", "CommentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CommentVote_Comments_CommentId",
                table: "CommentVote",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentVote_Users_UserId",
                table: "CommentVote",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostVote_Posts_PostId",
                table: "PostVote",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostVote_Users_UserId",
                table: "PostVote",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
