using System.Text.Json.Serialization;

namespace MovieGalleryAPI.DTO;

public class TmdbSearchResponse
{
    [JsonPropertyName("results")]
    public List<TmdbSearchResult> Results { get; set; } = new();
}

public class TmdbSearchResult
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("release_date")]
    public string? ReleaseDate { get; set; }

    [JsonPropertyName("poster_path")]
    public string? PosterPath { get; set; }
}

public class TmdbImagesResponse
{
    [JsonPropertyName("backdrops")]
    public List<TmdbImage> Backdrops { get; set; } = new();

    [JsonPropertyName("posters")]
    public List<TmdbImage> Posters { get; set; } = new();
}

public class TmdbImage
{
    [JsonPropertyName("file_path")]
    public string FilePath { get; set; } = string.Empty;
}

public record TmdbMovieMatch(int TmdbId, string? PosterPath);
public record TmdbMovieImages(string? PosterPath, List<string> BackdropPaths);
