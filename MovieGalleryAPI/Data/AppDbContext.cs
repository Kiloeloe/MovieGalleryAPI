using Microsoft.EntityFrameworkCore;
using MovieGalleryAPI.Model;

namespace MovieGalleryAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<Favorite> Favorites => Set<Favorite>();

    public DbSet<MovieScreenshot> Screenshots => Set<MovieScreenshot>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

        modelBuilder.Entity<Favorite>()
            .HasIndex(f => new { f.UserId, f.MovieId })
            .IsUnique(); 

        modelBuilder.Entity<Favorite>()
            .HasOne(f => f.User)
            .WithMany(u => u.Favorites)
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Favorite>()
            .HasOne(f => f.Movie)
            .WithMany(m => m.Favorites)
            .HasForeignKey(f => f.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<MovieScreenshot>()
           .HasOne(s => s.Movie)
           .WithMany(m => m.Screenshots)
           .HasForeignKey(s => s.MovieId)
           .OnDelete(DeleteBehavior.Cascade);

        SeedData.Apply(modelBuilder);
    }
}
