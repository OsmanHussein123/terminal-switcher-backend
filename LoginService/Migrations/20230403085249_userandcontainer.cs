using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginService.Migrations
{
    public partial class userandcontainer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Containers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ContainerName = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                table: "Containers",
                columns: new[] { "Id", "ContainerName", "UserId" },
                values: new object[] { 1, "nostalgic_goodall", 1 });

            migrationBuilder.InsertData(
                table: "Containers",
                columns: new[] { "Id", "ContainerName", "UserId" },
                values: new object[] { 2, "Ubuntu", 1 });

            migrationBuilder.InsertData(
                table: "Containers",
                columns: new[] { "Id", "ContainerName", "UserId" },
                values: new object[] { 3, "Ubuntu", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Containers_UserId",
                table: "Containers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Containers");
        }
    }
}
