using MovieGalleryAPI.Data;
using MovieGalleryAPI.Model;
using Microsoft.EntityFrameworkCore;
using MovieGalleryAPI.DTO;


namespace MovieGalleryAPI.Services.TMDB;

//extra service to populate db with fetched tmdb screenshots
public class TMDBPopulateService : ITMDBPopulateService
{
    private const string ImageBaseUrl = "https://image.tmdb.org/t/p";
    private const string PosterSize = "w500";
    private const string BackdropSize = "w780";

    private readonly AppDbContext _context;
    private readonly ITMDBImageService _tmdbService;
    private readonly IConfiguration _config;
    private readonly ILogger<TMDBPopulateService> _logger;

    public TMDBPopulateService(
        AppDbContext context,
        ITMDBImageService tmdbService,
        IConfiguration config,
        ILogger<TMDBPopulateService> logger)
    {
        _context = context;
        _tmdbService = tmdbService;
        _config = config;
        _logger = logger;
    }

    public async Task<int> EnrichMoviesAsync()
    {
        var apiKey = _config["Tmdb:ApiKey"];
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            _logger.LogInformation(
                "Tmdb:ApiKey is not configured - skipping TMDB enrichment; movies keep their placeholder images.");
            return 0;
        }

        var pendingMovies = await _context.Movies
            .Where(m => m.TmdbId == null)
            .ToListAsync();

        var enrichedCount = 0;

        foreach (var movie in pendingMovies)
        {
            try
            {
                var match = await _tmdbService.FindMovieAsync(movie.Title, movie.ReleaseYear);
                if (match is null)
                {
                    _logger.LogWarning("TMDB: no match found for '{Title}' ({Year})", movie.Title, movie.ReleaseYear);
                    continue;
                }

                var images = await _tmdbService.GetImagesAsync(match.TmdbId);
                ApplyImages(movie, match.TmdbId, images);
                enrichedCount++;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "TMDB enrichment failed for '{Title}'", movie.Title);
            }
        }

        if (enrichedCount > 0)
            await _context.SaveChangesAsync();

        return enrichedCount;
    }

    private void ApplyImages(MovieGalleryAPI.Model.Movie movie, int tmdbId, TmdbMovieImages images)
    {
        movie.TmdbId = tmdbId;

        if (!string.IsNullOrEmpty(images.PosterPath))
            movie.PosterUrl = $"{ImageBaseUrl}/{PosterSize}{images.PosterPath}";

        var oldScreenshots = _context.Screenshots.Where(s => s.MovieId == movie.Id);
        _context.Screenshots.RemoveRange(oldScreenshots);

        var order = 1;
        foreach (var backdropPath in images.BackdropPaths)
        {
            _context.Screenshots.Add(new MovieScreenshot
            {
                MovieId = movie.Id,
                ImageUrl = $"{ImageBaseUrl}/{BackdropSize}{backdropPath}",
                DisplayOrder = order++
            });
        }
    }
}
