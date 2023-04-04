using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginService.Migrations
{
    public partial class changedContainer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Containers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Containers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ContainerName", "Image" },
                values: new object[] { "e28112fb-c324-4356-b73f-5e62e1c8e274", "alpine" });

            migrationBuilder.UpdateData(
                table: "Containers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ContainerName", "Image" },
                values: new object[] { "757747b3-7a7e-4102-8d7c-f19834c661e0", "Ubuntu" });

            migrationBuilder.UpdateData(
                table: "Containers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ContainerName", "Image" },
                values: new object[] { "09d14f64-c6d1-44b4-b69e-6ea81f13085f", "Ubuntu" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Containers");

            migrationBuilder.UpdateData(
                table: "Containers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ContainerName",
                value: "nostalgic_goodall");

            migrationBuilder.UpdateData(
                table: "Containers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ContainerName",
                value: "Ubuntu");

            migrationBuilder.UpdateData(
                table: "Containers",
                keyColumn: "Id",
                keyValue: 3,
                column: "ContainerName",
                value: "Ubuntu");
        }
    }
}
