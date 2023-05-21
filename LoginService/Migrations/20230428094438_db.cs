using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginService.Migrations
{
    public partial class db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Containers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ContainerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Containers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Containers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Username" },
                values: new object[] { 1, "Password1", "User1" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Username" },
                values: new object[] { 2, "Password2", "User2" });

            migrationBuilder.InsertData(
                table: "Containers",
                columns: new[] { "Id", "ContainerName", "Image", "UserId" },
                values: new object[,]
                {
                    { 1, "727069e9-dae9-4c54-a9e0-6b5a87aa1e67", "alpine", 1 },
                    { 2, "1d17cdf0-98f6-4ca9-b164-861994099a76", "ubuntu", 1 },
                    { 3, "a4765804-ea33-4c47-a8c2-38bf47b7ac30", "debian", 1 },
                    { 4, "92ee1493-216f-4b2e-9e0e-f23b660647e7", "fedora/memcached", 1 },
                    { 5, "4b5e099f-ea50-41d1-8bf6-c9b34d13b1d3", "alpine", 2 },
                    { 6, "c64f9e6c-abc5-499a-9d9b-d8d691eb474a", "ubuntu", 2 },
                    { 7, "9fcfe6dc-ab67-46a1-ba4e-2302ac880402", "debian", 2 },
                    { 8, "89058d55-dec9-4375-8a34-6df302b75d43", "fedora/memcached", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Containers_UserId",
                table: "Containers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Containers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
