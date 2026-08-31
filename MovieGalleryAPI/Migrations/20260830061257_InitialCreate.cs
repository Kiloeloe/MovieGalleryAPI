using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieGalleryAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cast = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    PosterUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PopularityScore = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorites_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Cast", "Description", "Director", "Genre", "PopularityScore", "PosterUrl", "Rating", "ReleaseYear", "Title" },
                values: new object[,]
                {
                    { 1, "Matt Damon, Anne Hathaway", "Odysseus, the legendary King of Ithaca, embarks on a long and perilous journey home following the Trojan War. Throughout his voyage, he is forced to confront the whims of gods, mythological monsters, and trials that stretch both his cunning and his humanity to the breaking point.", "Christopher Nolan", "Adventure", 98, "https://image.tmdb.org/t/p/original/pe5cCoX5iIb5IWKPsbPkCwjLFHt.jpg", 9.3000000000000007, 2026, "The Odyssey" },
                    { 2, "Michael Johnston, Inde Navarette", "After breaking the mysterious “One Wish Willow” to win his crush’s heart, a hopeless romantic finds himself getting exactly what he asked for but soon discovers that some desires come at a dark, sinister price.", "Curry Barker", "Horror", 97, "https://image.tmdb.org/t/p/original/bRwnj8WEKBCvmfeUNOukJPwB43K.jpg", 8.1999999999999993, 2025, "Obsession" },
                    { 3, "Michael Cera, Mary Elizabeth Winstead", "As bass guitarist for a garage-rock band, Scott Pilgrim has never had trouble getting a girlfriend; usually, the problem is getting rid of them. But when Ramona Flowers skates into his heart, he finds she has the most troublesome baggage of all: an army of ex-boyfriends who will stop at nothing to eliminate him from her list of suitors.", "Edgar Wright", "Comedy", 87, "https://image.tmdb.org/t/p/original/g5IoYeudx9XBEfwNL0fHvSckLBz.jpg", 8.8000000000000007, 2010, "Scott Pilgrim vs. the World" },
                    { 4, "David Corenswet, Rachel Brosnahan", "Superman, a journalist in Metropolis, embarks on a journey to reconcile his Kryptonian heritage with his human upbringing as Clark Kent.", "James Gunn", "Action", 89, "https://image.tmdb.org/t/p/original/wPLysNDLffQLOVebZQCbXJEv6E6.jpg", 8.4000000000000004, 2025, "Superman" },
                    { 5, "Tom Hanks, Robin Wright", "The presidencies of Kennedy and Johnson unfold through the eyes of an Alabama man.", "Robert Zemeckis", "Drama", 92, "https://image.tmdb.org/t/p/original/Cw4hIUIAmSYfK9QfaUW5igp9La.jpg", 8.8000000000000007, 1994, "Forrest Gump" },
                    { 6, "Keanu Reeves, Laurence Fishburne", "A hacker learns the truth about his reality and his role in the war against its controllers.", "Lana Wachowski", "Sci-Fi", 91, "https://image.tmdb.org/t/p/original/dXNAPwY7VrqMAo51EKhhCJfaGb5.jpg", 8.6999999999999993, 1999, "The Matrix" },
                    { 7, "Tom Holland, Zendaya", "Fighting crime full-time as Spider-Man in a world that doesn’t remember him—and the pressure of seeing his old friends move on without him—sparks a change in Peter Parker he may not have the power to control. But that transformation might also be the only thing that can stop a shocking new threat to the city and those he loves - a powerful villain no one can even see.", "Destin Daniel Cretton", "Action", 90, "https://image.tmdb.org/t/p/original/kt3IvI7VDffMFMcQzn8AIi1m2zI.jpg", 8.5999999999999996, 2026, "Spider-Man: Brand New Day" },
                    { 8, "Marlon Brando, Al Pacino", "The aging patriarch of an organized crime dynasty transfers control to his son.", "Francis Ford Coppola", "Crime", 96, "https://image.tmdb.org/t/p/original/3bhkrj58Vtu7enYsRolD1fZdja1.jpg", 9.1999999999999993, 1972, "The Godfather" },
                    { 9, "Brad Pitt, Edward Norton", "An insomniac office worker and a soap maker form an underground fight club.", "David Fincher", "Drama", 89, "https://image.tmdb.org/t/p/original/wR5HZWdVpcXx9sevV1bQi7rP4op.jpg", 8.8000000000000007, 1999, "Fight Club" },
                    { 10, "Rumi Hiiragi, Miyu Irino", "A young girl wanders into a world ruled by gods and witches.", "Hayao Miyazaki", "Animation", 88, "https://image.tmdb.org/t/p/original/39wmItIWsg5sZMyRUHLkWBcuVCM.jpg", 8.5999999999999996, 2001, "Spirited Away" },
                    { 11, "Yuumi kawai, Mizuki Yoshida", "Popular, outgoing Fujino is celebrated by her classmates for her funny comics in the class newspaper. One day, her teacher asks her to share the space with Kyomoto, a truant recluse whose beautiful artwork sparks a competitive fervor in Fujino. What starts as jealousy transforms when Fujino realizes their shared passion for drawing.", "Kiyotaka Oshiyama", "Animation", 87, "https://image.tmdb.org/t/p/original/4f2EcNkp1Mvp9wE5w7HKxcmACWg.jpg", 8.5, 2024, "Look Back" },
                    { 12, "Kyoko Fukada, Anna Tsuchiya", "Momoko, a strange and seemingly emotionless girl obsessed with 18th century France, befriends a Yanki biker and the two experience the ups and downs of their unusual lives in a rural Japanese town.", "Tetsuya Nakashima", "Comedy", 85, "https://image.tmdb.org/t/p/original/43kZBGCXQbydXTDjRuuLTfwpQXh.jpg", 7.2000000000000002, 2004, "Kamikaze Girls" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_MovieId",
                table: "Favorites",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId_MovieId",
                table: "Favorites",
                columns: new[] { "UserId", "MovieId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
