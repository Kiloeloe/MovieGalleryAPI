using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieGalleryAPI.Model;
using MovieGalleryAPI.Services;
using MovieGalleryAPI.Services.TMDB;

namespace MovieGalleryAPI.Data;

public class DbInit
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();

        if (!context.Users.Any())
        {
            var hasher = new PasswordHasher<User>();
            var demoUser = new User { Username = "demo" };
            demoUser.PasswordHash = hasher.HashPassword(demoUser, "Demo123!");

            context.Users.Add(demoUser);
            context.SaveChanges();
        }

        var enrichmentService = services.GetRequiredService<ITMDBPopulateService>();
        await enrichmentService.EnrichMoviesAsync();
    }
}
