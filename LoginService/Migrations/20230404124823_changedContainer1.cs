using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginService.Migrations
{
    public partial class changedContainer1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Containers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ContainerName",
                value: "80b8d683-86d3-4360-8303-e2184933f064");

            migrationBuilder.UpdateData(
                table: "Containers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ContainerName", "Image" },
                values: new object[] { "2de33b71-08eb-4932-9118-b37972246344", "ubuntu" });

            migrationBuilder.UpdateData(
                table: "Containers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ContainerName", "Image" },
                values: new object[] { "29468db0-95c6-4776-9a79-a30f7c358cb0", "debian" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Containers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ContainerName",
                value: "e28112fb-c324-4356-b73f-5e62e1c8e274");

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
    }
}
