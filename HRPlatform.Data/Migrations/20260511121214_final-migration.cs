using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPlatform.Data.Migrations
{
    /// <inheritdoc />
    public partial class finalmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_Skill_Name_NotEmpty",
                table: "Skills",
                sql: "\"Name\" <> ''");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Skill_Name_NotEmpty",
                table: "Skills");
        }
    }
}
