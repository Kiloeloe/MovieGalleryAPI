using MovieGalleryAPI.Services.Movie;
using FluentAssertions;
using MovieGalleryAPI.Services;
using Xunit;

namespace MovieGalleryAPI.Tests;

public class MovieServiceTests
{
    //testing popularity order
    [Fact]
    public async Task GetPopularAsync_ReturnsMoviesOrderedByPopularityDescending()
    {
        using var context = TestDbContextFactory.Create();
        var service = new MovieService(context);

        var result = await service.GetPopularAsync(userId: 1);

        result.Should().HaveCount(3);
        result.Select(m => m.Title).Should().ContainInOrder("Inception", "Forrest Gump", "The Matrix");
    }

    //testing search
    [Fact]
    public async Task SearchAsync_IsCaseInsensitiveAndMatchesGenre()
    {
        using var context = TestDbContextFactory.Create();
        var service = new MovieService(context);

        var result = await service.SearchAsync("sci-fi", userId: 1);

        result.Should().HaveCount(2);
        result.Select(m => m.Title).Should().Contain(new[] { "Inception", "The Matrix" });
    }

    //testing no match search
    [Fact]
    public async Task SearchAsync_WithNoMatches_ReturnsEmptyList()
    {
        using var context = TestDbContextFactory.Create();
        var service = new MovieService(context);

        var result = await service.SearchAsync("nonexistent-keyword-xyz", userId: 1);

        result.Should().BeEmpty();
    }

    //test movie detail
    [Fact]
    public async Task GetByIdAsync_WithValidId_ReturnsMovieDetail()
    {
        using var context = TestDbContextFactory.Create();
        var service = new MovieService(context);

        var result = await service.GetByIdAsync(1, userId: 1);

        result.Should().NotBeNull();
        result!.Title.Should().Be("Inception");
        result.Director.Should().Be("Christopher Nolan");
    }

    //test invalid movie id
    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ReturnsNull()
    {
        using var context = TestDbContextFactory.Create();
        var service = new MovieService(context);

        var result = await service.GetByIdAsync(999, userId: 1);

        result.Should().BeNull();
    }

    //testing screenshot order
    [Fact]
    public async Task GetByIdAsync_ReturnsScreenshotsOrderedByDisplayOrder()
    {
        using var context = TestDbContextFactory.Create();
        var service = new MovieService(context);

        var result = await service.GetByIdAsync(1, userId: 1);

        result!.ScreenshotUrls.Should().ContainInOrder("shot-a.jpg", "shot-b.jpg");
    }


    //testing isfavorite
    [Fact]
    public async Task GetByIdAsync_MarksIsFavoriteWhenUserFavoritedIt()
    {
        using var context = TestDbContextFactory.Create();
        context.Favorites.Add(new MovieGalleryAPI.Model.Favorite { UserId = 1, MovieId = 2 });
        await context.SaveChangesAsync();

        var service = new MovieService(context);
        var result = await service.GetByIdAsync(2, userId: 1);

        result!.IsFavorite.Should().BeTrue();
    }
}
