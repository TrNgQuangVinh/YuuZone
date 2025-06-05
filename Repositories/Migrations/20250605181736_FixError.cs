using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class FixError : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Drop the foreign key constraints (if any reference Comments.Id)
            // (Skip if Comments is not referenced by others)

            // 2. Drop the primary key
            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            // 3. Rename old column (optional, to preserve data if needed)
            // Or skip and let it drop the column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Comments");

            // 4. Add new string Id column
            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Comments",
                type: "nvarchar(450)", // PK needs to be <900 bytes, so 450 is max safe
                nullable: false,
                defaultValue: ""); // Adjust if you're generating GUIDs manually

            // 5. Re-add the primary key
            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "GenderTitle",
                table: "Genders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropPrimaryKey(
        name: "PK_Comments",
        table: "Comments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "GenderTitle",
                table: "Genders",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

        }
    }
}
