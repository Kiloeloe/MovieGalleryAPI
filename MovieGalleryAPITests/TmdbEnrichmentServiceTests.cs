using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using MovieGalleryAPI.DTO;
using MovieGalleryAPI.Services;
using MovieGalleryAPI.Services.TMDB;
using Xunit;

namespace MovieGalleryAPI.Tests;
//extra unit tests for mocking the screenshot populating service
public class TmdbEnrichmentServiceTests
{
    private readonly Mock<ITMDBImageService> _tmdbServiceMock = new();

    private static IConfiguration ConfigWithKey(string? apiKey)
    {
        var dict = new Dictionary<string, string?> { ["Tmdb:ApiKey"] = apiKey };
        return new ConfigurationBuilder().AddInMemoryCollection(dict).Build();
    }

    //test if no tmdb apikey configured
    [Fact]
    public async Task EnrichMoviesAsync_WithNoApiKeyConfigured_SkipsEntirelyAndReturnsZero()
    {
        using var context = TestDbContextFactory.Create();
        var service = new TMDBPopulateService(
            context, _tmdbServiceMock.Object, ConfigWithKey(null), NullLogger<TMDBPopulateService>.Instance);

        var enrichedCount = await service.EnrichMoviesAsync();

        enrichedCount.Should().Be(0);
        _tmdbServiceMock.Verify(t => t.FindMovieAsync(It.IsAny<string>(), It.IsAny<int?>()), Times.Never);
    }

    //mocking tmdb api call
    [Fact]
    public async Task EnrichMoviesAsync_WithMatchFound_UpdatesPosterAndReplacesScreenshots()
    {
        using var context = TestDbContextFactory.Create();
        _tmdbServiceMock
            .Setup(t => t.FindMovieAsync("Inception", 2010))
            .ReturnsAsync(new TmdbMovieMatch(27205, "/poster.jpg"));
        _tmdbServiceMock
            .Setup(t => t.GetImagesAsync(27205))
            .ReturnsAsync(new TmdbMovieImages("/poster.jpg", new List<string> { "/bd1.jpg", "/bd2.jpg" }));

        //mimicking only one movie id
        var service = new TMDBPopulateService(
            context, _tmdbServiceMock.Object, ConfigWithKey("fake-key"), NullLogger<TMDBPopulateService>.Instance);

        await service.EnrichMoviesAsync();

        var inception = context.Movies.First(m => m.Id == 1);
        inception.TmdbId.Should().Be(27205);
        inception.PosterUrl.Should().Contain("/poster.jpg");

        var screenshots = context.Screenshots.Where(s => s.MovieId == 1).OrderBy(s => s.DisplayOrder).ToList();
        screenshots.Should().HaveCount(2);
        screenshots[0].ImageUrl.Should().Contain("/bd1.jpg");
        screenshots[1].ImageUrl.Should().Contain("/bd2.jpg");
    }

    //skip movie id that are already seeded with tmdb data
    [Fact]
    public async Task EnrichMoviesAsync_IsIdempotent_SkipsMoviesAlreadyEnriched()
    {
        using var context = TestDbContextFactory.Create();
        context.Movies.First(m => m.Id == 1).TmdbId = 999;
        await context.SaveChangesAsync();

        var service = new TMDBPopulateService(
            context, _tmdbServiceMock.Object, ConfigWithKey("fake-key"), NullLogger<TMDBPopulateService>.Instance);

        await service.EnrichMoviesAsync();

        _tmdbServiceMock.Verify(t => t.FindMovieAsync("Inception", It.IsAny<int?>()), Times.Never);
    }

    //testing if tmdb has no match
    [Fact]
    public async Task EnrichMoviesAsync_WhenNoMatchFound_LeavesMovieUntouchedAndContinues()
    {
        using var context = TestDbContextFactory.Create();
        _tmdbServiceMock
            .Setup(t => t.FindMovieAsync(It.IsAny<string>(), It.IsAny<int?>()))
            .ReturnsAsync((TmdbMovieMatch?)null);

        var service = new TMDBPopulateService(
            context, _tmdbServiceMock.Object, ConfigWithKey("fake-key"), NullLogger<TMDBPopulateService>.Instance);

        var enrichedCount = await service.EnrichMoviesAsync();

        enrichedCount.Should().Be(0);
        context.Movies.Should().OnlyContain(m => m.TmdbId == null);
    }
}
