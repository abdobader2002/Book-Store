using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Author", "CategoryId", "Description", "ISBN", "ImgUrl", "ListPrice", "Price100Plus", "Price50Plus", "Title", "price" },
                values: new object[,]
                {
                    { 2, "Nancy Hoover", 1, "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.", "CAW777777701", "", 40.0, 20.0, 25.0, "Dark Skies", 30.0 },
                    { 3, "Julian Button", 1, "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.", "RITO5555501", "", 55.0, 35.0, 40.0, "Vanish in the Sunset", 50.0 },
                    { 4, "Abby Muscles", 2, "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", "WS3333333301", "", 70.0, 55.0, 60.0, "Cotton Candy", 65.0 },
                    { 5, "Ron Parker", 2, "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam.", "SOTJ1111111101", "", 30.0, 20.0, 25.0, "Rock in the Ocean", 27.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
