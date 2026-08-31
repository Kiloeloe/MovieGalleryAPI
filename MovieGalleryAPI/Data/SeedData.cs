using Microsoft.EntityFrameworkCore;
using MovieGalleryAPI.Model;

namespace MovieGalleryAPI.Data;

// Movie data has no dependency on runtime services, so it's safe to seed
// via HasData (applied through migrations). User accounts are seeded
// separately at startup because password hashing needs a DI service - see DbInitializer.
public class SeedData
{
    public static void Apply(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>().HasData(
            new Movie { Id = 1, Title = "The Odyssey", Description = "Odysseus, the legendary King of Ithaca, embarks on a long and perilous journey home following the Trojan War. Throughout his voyage, he is forced to confront the whims of gods, mythological monsters, and trials that stretch both his cunning and his humanity to the breaking point.", Genre = "Adventure", ReleaseYear = 2026, Director = "Christopher Nolan", Cast = "Matt Damon, Anne Hathaway", Rating = 9.3, PosterUrl = "https://image.tmdb.org/t/p/original/pe5cCoX5iIb5IWKPsbPkCwjLFHt.jpg", PopularityScore = 98 },
            new Movie { Id = 2, Title = "Obsession", Description = "After breaking the mysterious “One Wish Willow” to win his crush’s heart, a hopeless romantic finds himself getting exactly what he asked for but soon discovers that some desires come at a dark, sinister price.", Genre = "Horror", ReleaseYear = 2025, Director = "Curry Barker", Cast = "Michael Johnston, Inde Navarette", Rating = 8.2, PosterUrl = "https://image.tmdb.org/t/p/original/bRwnj8WEKBCvmfeUNOukJPwB43K.jpg", PopularityScore = 97},
            new Movie { Id = 3, Title = "Scott Pilgrim vs. the World", Description = "As bass guitarist for a garage-rock band, Scott Pilgrim has never had trouble getting a girlfriend; usually, the problem is getting rid of them. But when Ramona Flowers skates into his heart, he finds she has the most troublesome baggage of all: an army of ex-boyfriends who will stop at nothing to eliminate him from her list of suitors.", Genre = "Comedy", ReleaseYear = 2010, Director = "Edgar Wright", Cast = "Michael Cera, Mary Elizabeth Winstead", Rating = 8.8, PosterUrl = "https://image.tmdb.org/t/p/original/g5IoYeudx9XBEfwNL0fHvSckLBz.jpg", PopularityScore = 87 },
            new Movie { Id = 4, Title = "Superman", Description = "Superman, a journalist in Metropolis, embarks on a journey to reconcile his Kryptonian heritage with his human upbringing as Clark Kent.", Genre = "Action", ReleaseYear = 2025, Director = "James Gunn", Cast = "David Corenswet, Rachel Brosnahan", Rating = 8.4, PosterUrl = "https://image.tmdb.org/t/p/original/wPLysNDLffQLOVebZQCbXJEv6E6.jpg", PopularityScore = 89 },
            new Movie { Id = 5, Title = "Forrest Gump", Description = "A man with a low IQ has accomplished great things in his life and been present during significant historic events—in each case, far exceeding what anyone imagined he could do. But despite all he has achieved, his one true love eludes him.", Genre = "Drama", ReleaseYear = 1994, Director = "Robert Zemeckis", Cast = "Tom Hanks, Robin Wright", Rating = 8.8, PosterUrl = "https://image.tmdb.org/t/p/original/Cw4hIUIAmSYfK9QfaUW5igp9La.jpg", PopularityScore = 92 },
            new Movie { Id = 6, Title = "The Matrix", Description = "A hacker learns the truth about his reality and his role in the war against its controllers.", Genre = "Sci-Fi", ReleaseYear = 1999, Director = "Lana Wachowski", Cast = "Keanu Reeves, Laurence Fishburne", Rating = 8.7, PosterUrl = "https://image.tmdb.org/t/p/original/dXNAPwY7VrqMAo51EKhhCJfaGb5.jpg", PopularityScore = 91 },
            new Movie { Id = 7, Title = "Spider-Man: Brand New Day", Description = "Fighting crime full-time as Spider-Man in a world that doesn’t remember him—and the pressure of seeing his old friends move on without him—sparks a change in Peter Parker he may not have the power to control. But that transformation might also be the only thing that can stop a shocking new threat to the city and those he loves - a powerful villain no one can even see.", Genre = "Action", ReleaseYear = 2026, Director = "Destin Daniel Cretton", Cast = "Tom Holland, Zendaya", Rating = 8.6, PosterUrl = "https://image.tmdb.org/t/p/original/kt3IvI7VDffMFMcQzn8AIi1m2zI.jpg", PopularityScore = 90 },
            new Movie { Id = 8, Title = "The Godfather", Description = "Spanning the years 1945 to 1955, a chronicle of the fictional Italian-American Corleone crime family. When organized crime family patriarch, Vito Corleone barely survives an attempt on his life, his youngest son, Michael steps in to take care of the would-be killers, launching a campaign of bloody revenge.", Genre = "Crime", ReleaseYear = 1972, Director = "Francis Ford Coppola", Cast = "Marlon Brando, Al Pacino", Rating = 9.2, PosterUrl = "https://image.tmdb.org/t/p/original/3bhkrj58Vtu7enYsRolD1fZdja1.jpg", PopularityScore = 96 },
            new Movie { Id = 9, Title = "Fight Club", Description = "A ticking-time-bomb insomniac and a slippery soap salesman channel primal male aggression into a shocking new form of therapy. Their concept catches on, with underground \"fight clubs\" forming in every town, until an eccentric gets in the way and ignites an out-of-control spiral toward oblivion.", Genre = "Drama", ReleaseYear = 1999, Director = "David Fincher", Cast = "Brad Pitt, Edward Norton", Rating = 8.8, PosterUrl = "https://image.tmdb.org/t/p/original/wR5HZWdVpcXx9sevV1bQi7rP4op.jpg", PopularityScore = 89 },
            new Movie { Id = 10, Title = "Spirited Away", Description = "A young girl, Chihiro, becomes trapped in a strange new world of spirits. When her parents undergo a mysterious transformation, she must call upon the courage she never knew she had to free her family.", Genre = "Animation", ReleaseYear = 2001, Director = "Hayao Miyazaki", Cast = "Rumi Hiiragi, Miyu Irino", Rating = 8.6, PosterUrl = "https://image.tmdb.org/t/p/original/39wmItIWsg5sZMyRUHLkWBcuVCM.jpg", PopularityScore = 88 },
            new Movie { Id = 11, Title = "Look Back", Description = "Popular, outgoing Fujino is celebrated by her classmates for her funny comics in the class newspaper. One day, her teacher asks her to share the space with Kyomoto, a truant recluse whose beautiful artwork sparks a competitive fervor in Fujino. What starts as jealousy transforms when Fujino realizes their shared passion for drawing.", Genre = "Animation", ReleaseYear = 2024, Director = "Kiyotaka Oshiyama", Cast = "Yuumi kawai, Mizuki Yoshida", Rating = 8.5, PosterUrl = "https://image.tmdb.org/t/p/original/4f2EcNkp1Mvp9wE5w7HKxcmACWg.jpg", PopularityScore = 87 },
            new Movie { Id = 12, Title = "Kamikaze Girls", Description = "Momoko, a strange and seemingly emotionless girl obsessed with 18th century France, befriends a Yanki biker and the two experience the ups and downs of their unusual lives in a rural Japanese town.", Genre = "Comedy", ReleaseYear = 2004, Director = "Tetsuya Nakashima", Cast = "Kyoko Fukada, Anna Tsuchiya", Rating = 7.2, PosterUrl = "https://image.tmdb.org/t/p/original/43kZBGCXQbydXTDjRuuLTfwpQXh.jpg", PopularityScore = 85 }
        );

        var screenshotId = 1;
        var screenshots = new List<MovieScreenshot>();
        var movieSeeds = new (int MovieId, string Slug)[]
        {
            (1, "shawshank"), (2, "darkknight"), (3, "inception"), (4, "pulpfiction"),
            (5, "forrestgump"), (6, "matrix"), (7, "interstellar"), (8, "godfather"),
            (9, "fightclub"), (10, "spiritedaway"), (11, "parasite"), (12, "gladiator")
        };


        foreach (var (movieId, slug) in movieSeeds)
        {
            for (var i = 1; i <= 3; i++)
            {
                screenshots.Add(new MovieScreenshot
                {
                    Id = screenshotId++,
                    MovieId = movieId,
                    ImageUrl = $"https://picsum.photos/seed/{slug}-shot{i}/800/450",
                    DisplayOrder = i
                });
            }
        }

        modelBuilder.Entity<MovieScreenshot>().HasData(screenshots);
    }
}
