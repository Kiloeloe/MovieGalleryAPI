using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieGalleryAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTmdbEnrichment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TmdbId",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Screenshots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screenshots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Screenshots_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1,
                column: "TmdbId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2,
                column: "TmdbId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3,
                column: "TmdbId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4,
                column: "TmdbId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 5,
                column: "TmdbId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 6,
                column: "TmdbId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 7,
                column: "TmdbId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 8,
                column: "TmdbId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 9,
                column: "TmdbId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 10,
                column: "TmdbId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 11,
                column: "TmdbId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 12,
                column: "TmdbId",
                value: null);

            migrationBuilder.InsertData(
                table: "Screenshots",
                columns: new[] { "Id", "DisplayOrder", "ImageUrl", "MovieId" },
                values: new object[,]
                {
                    { 1, 1, "https://picsum.photos/seed/shawshank-shot1/800/450", 1 },
                    { 2, 2, "https://picsum.photos/seed/shawshank-shot2/800/450", 1 },
                    { 3, 3, "https://picsum.photos/seed/shawshank-shot3/800/450", 1 },
                    { 4, 1, "https://picsum.photos/seed/darkknight-shot1/800/450", 2 },
                    { 5, 2, "https://picsum.photos/seed/darkknight-shot2/800/450", 2 },
                    { 6, 3, "https://picsum.photos/seed/darkknight-shot3/800/450", 2 },
                    { 7, 1, "https://picsum.photos/seed/inception-shot1/800/450", 3 },
                    { 8, 2, "https://picsum.photos/seed/inception-shot2/800/450", 3 },
                    { 9, 3, "https://picsum.photos/seed/inception-shot3/800/450", 3 },
                    { 10, 1, "https://picsum.photos/seed/pulpfiction-shot1/800/450", 4 },
                    { 11, 2, "https://picsum.photos/seed/pulpfiction-shot2/800/450", 4 },
                    { 12, 3, "https://picsum.photos/seed/pulpfiction-shot3/800/450", 4 },
                    { 13, 1, "https://picsum.photos/seed/forrestgump-shot1/800/450", 5 },
                    { 14, 2, "https://picsum.photos/seed/forrestgump-shot2/800/450", 5 },
                    { 15, 3, "https://picsum.photos/seed/forrestgump-shot3/800/450", 5 },
                    { 16, 1, "https://picsum.photos/seed/matrix-shot1/800/450", 6 },
                    { 17, 2, "https://picsum.photos/seed/matrix-shot2/800/450", 6 },
                    { 18, 3, "https://picsum.photos/seed/matrix-shot3/800/450", 6 },
                    { 19, 1, "https://picsum.photos/seed/interstellar-shot1/800/450", 7 },
                    { 20, 2, "https://picsum.photos/seed/interstellar-shot2/800/450", 7 },
                    { 21, 3, "https://picsum.photos/seed/interstellar-shot3/800/450", 7 },
                    { 22, 1, "https://picsum.photos/seed/godfather-shot1/800/450", 8 },
                    { 23, 2, "https://picsum.photos/seed/godfather-shot2/800/450", 8 },
                    { 24, 3, "https://picsum.photos/seed/godfather-shot3/800/450", 8 },
                    { 25, 1, "https://picsum.photos/seed/fightclub-shot1/800/450", 9 },
                    { 26, 2, "https://picsum.photos/seed/fightclub-shot2/800/450", 9 },
                    { 27, 3, "https://picsum.photos/seed/fightclub-shot3/800/450", 9 },
                    { 28, 1, "https://picsum.photos/seed/spiritedaway-shot1/800/450", 10 },
                    { 29, 2, "https://picsum.photos/seed/spiritedaway-shot2/800/450", 10 },
                    { 30, 3, "https://picsum.photos/seed/spiritedaway-shot3/800/450", 10 },
                    { 31, 1, "https://picsum.photos/seed/parasite-shot1/800/450", 11 },
                    { 32, 2, "https://picsum.photos/seed/parasite-shot2/800/450", 11 },
                    { 33, 3, "https://picsum.photos/seed/parasite-shot3/800/450", 11 },
                    { 34, 1, "https://picsum.photos/seed/gladiator-shot1/800/450", 12 },
                    { 35, 2, "https://picsum.photos/seed/gladiator-shot2/800/450", 12 },
                    { 36, 3, "https://picsum.photos/seed/gladiator-shot3/800/450", 12 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Screenshots_MovieId",
                table: "Screenshots",
                column: "MovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Screenshots");

            migrationBuilder.DropColumn(
                name: "TmdbId",
                table: "Movies");
        }
    }
}
