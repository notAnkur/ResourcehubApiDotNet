using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ResourcehubApiDotNet.Migrations
{
    public partial class UserOAuth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OAuthTwitter",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    usernameFK = table.Column<string>(nullable: true),
                    platformUserId = table.Column<int>(nullable: false),
                    platformUsername = table.Column<string>(nullable: true),
                    platformAvatar = table.Column<string>(nullable: true),
                    accessToken = table.Column<string>(nullable: true),
                    accessTokenSecret = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAuthTwitter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    username = table.Column<string>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(nullable: true),
                    isEmailVerified = table.Column<bool>(nullable: false),
                    avatar = table.Column<string>(nullable: true),
                    primaryOauthProvider = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.username);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OAuthTwitter");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
