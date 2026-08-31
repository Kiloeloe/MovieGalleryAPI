using Microsoft.EntityFrameworkCore;
using MovieGalleryAPI.Data;
using MovieGalleryAPI.Model;

namespace MovieGalleryAPI.Tests;

//seeding data for the test using in memory appdbcontext
public static class TestDbContextFactory
{
    public static AppDbContext Create()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new AppDbContext(options);

        context.Movies.AddRange(
            new Movie { Id = 1, Title = "Inception", Genre = "Sci-Fi", Director = "Christopher Nolan", ReleaseYear = 2010, Rating = 8.8, PopularityScore = 95, PosterUrl = "p1.jpg", Description = "d1", Cast = "c1" },
            new Movie { Id = 2, Title = "The Matrix", Genre = "Sci-Fi", Director = "Lana Wachowski", ReleaseYear = 1999, Rating = 8.7, PopularityScore = 91, PosterUrl = "p2.jpg", Description = "d2", Cast = "c2" },
            new Movie { Id = 3, Title = "Forrest Gump", Genre = "Drama", Director = "Robert Zemeckis", ReleaseYear = 1994, Rating = 8.8, PopularityScore = 92, PosterUrl = "p3.jpg", Description = "d3", Cast = "c3" }
        );

        context.Users.Add(new User { Id = 1, Username = "demo", PasswordHash = "irrelevant-for-these-tests" });

        context.Screenshots.AddRange(
            new MovieScreenshot { Id = 1, MovieId = 1, ImageUrl = "shot-b.jpg", DisplayOrder = 2 },
            new MovieScreenshot { Id = 2, MovieId = 1, ImageUrl = "shot-a.jpg", DisplayOrder = 1 }
        );

        context.SaveChanges();
        return context;
    }
}
