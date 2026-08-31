using MovieGalleryAPI.Services.Favorites;
using FluentAssertions;
using MovieGalleryAPI.Services;
using Xunit;

namespace MovieGalleryAPI.Tests;

public class FavoritesServiceTests
{
    //testing add to favorites
    [Fact]
    public async Task AddFavoriteAsync_WithValidMovie_AddsFavoriteAndReturnsSuccess()
    {
        using var context = TestDbContextFactory.Create();
        var service = new FavoritesService(context);

        var result = await service.AddFavoriteAsync(userId: 1, movieId: 1);

        result.Success.Should().BeTrue();
        context.Favorites.Should().ContainSingle(f => f.UserId == 1 && f.MovieId == 1);
    }

    //testing single favorite logic
    [Fact]
    public async Task AddFavoriteAsync_WhenAlreadyFavorited_DoesNotCreateDuplicate()
    {
        using var context = TestDbContextFactory.Create();
        var service = new FavoritesService(context);

        await service.AddFavoriteAsync(userId: 1, movieId: 1);
        var second = await service.AddFavoriteAsync(userId: 1, movieId: 1);

        second.Success.Should().BeTrue();
        context.Favorites.Count(f => f.UserId == 1 && f.MovieId == 1).Should().Be(1);
    }

    //testing remove favorite with invalid id
    [Fact]
    public async Task AddFavoriteAsync_WithNonExistentMovie_ReturnsFailure()
    {
        using var context = TestDbContextFactory.Create();
        var service = new FavoritesService(context);

        var result = await service.AddFavoriteAsync(userId: 1, movieId: 999);

        result.Success.Should().BeFalse();
    }

    //testing remove favorite with valid id
    [Fact]
    public async Task RemoveFavoriteAsync_WithExistingFavorite_RemovesIt()
    {
        using var context = TestDbContextFactory.Create();
        var service = new FavoritesService(context);
        await service.AddFavoriteAsync(userId: 1, movieId: 1);

        var result = await service.RemoveFavoriteAsync(userId: 1, movieId: 1);

        result.Success.Should().BeTrue();
        context.Favorites.Should().BeEmpty();
    }

    [Fact]
    public async Task RemoveFavoriteAsync_WhenNotFavorited_ReturnsFailure()
    {
        using var context = TestDbContextFactory.Create();
        var service = new FavoritesService(context);

        var result = await service.RemoveFavoriteAsync(userId: 1, movieId: 1);

        result.Success.Should().BeFalse();
    }

    //testing favorites list
    [Fact]
    public async Task GetFavoritesAsync_ReturnsOnlyThatUsersFavorites()
    {
        using var context = TestDbContextFactory.Create();
        var service = new FavoritesService(context);
        await service.AddFavoriteAsync(userId: 1, movieId: 1);
        await service.AddFavoriteAsync(userId: 1, movieId: 2);

        var result = await service.GetFavoritesAsync(userId: 1);

        result.Should().HaveCount(2);
        result.Should().OnlyContain(m => m.IsFavorite);
    }
}
