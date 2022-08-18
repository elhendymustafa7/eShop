using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication4.Data.Migrations
{
    public partial class Adduseradmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName]) VALUES (N'dad71d86-a8d6-4441-accc-b96a2ea455fd', N'elhendymustafa7', N'ELHENDYMUSTAFA7', N'elhendymustafa7@gmail.com', N'ELHENDYMUSTAFA7@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEAUCJ2GVDbGsbMLYZkSGZyKOoelgzWDGL8slGF7XH22D77xfUi9O4PlR6VmPw7ARXg==', N'U2LSY2CZ42CWOIJKB2S3GKHM7Y32PSMD', N'a5544599-d88a-4102-a3f5-6251a60b7df6', NULL, 0, 0, NULL, 1, 0, N'Mustafa', N'Elhendy')\r\n");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[AspNetUsers] WHERE Id = 'dad71d86-a8d6-4441-accc-b96a2ea455fd'");
        }
    }
}
