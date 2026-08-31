using MovieGalleryAPI.Services.TMDB;
using System.Text.Json.Serialization;
using MovieGalleryAPI.DTO;
using System.Web;
using MovieGalleryAPI.Services.TMDB;

namespace MovieGalleryAPI.Services.TMDB;

//extra feature to populate movie screenshots
public class TMDBImageService : ITMDBImageService
{
    private readonly HttpClient _http;
    private readonly string _apiKey;

    public TMDBImageService(HttpClient http, IConfiguration config)
    {
        _http = http;
        _apiKey = config["Tmdb:ApiKey"] ?? string.Empty;
    }

    public async Task<TmdbMovieMatch?> FindMovieAsync(string title, int? releaseYear)
    {
        if (string.IsNullOrWhiteSpace(_apiKey))
            throw new InvalidOperationException("Tmdb:ApiKey is not configured.");

        var query = HttpUtility.UrlEncode(title);
        var url = $"search/movie?api_key={_apiKey}&query={query}";
        if (releaseYear is > 0)
            url += $"&year={releaseYear}";

        var response = await _http.GetFromJsonAsync<TmdbSearchResponse>(url);
        var best = response?.Results.FirstOrDefault();

        return best is null ? null : new TmdbMovieMatch(best.Id, best.PosterPath);
    }

    public async Task<TmdbMovieImages> GetImagesAsync(int tmdbId)
    {
        if (string.IsNullOrWhiteSpace(_apiKey))
            throw new InvalidOperationException("Tmdb:ApiKey is not configured.");

        var url = $"movie/{tmdbId}/images?api_key={_apiKey}";
        var response = await _http.GetFromJsonAsync<TmdbImagesResponse>(url);

        var posterPath = response?.Posters.FirstOrDefault()?.FilePath;
        var backdropPaths = response?.Backdrops
            .Take(5) 
            .Select(b => b.FilePath)
            .ToList() ?? new List<string>();

        return new TmdbMovieImages(posterPath, backdropPaths);
    }
}
