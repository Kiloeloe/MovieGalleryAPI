namespace MovieGalleryAPI.DTO.Response;

public class MovieResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; } 
    public int ReleaseYear { get; set; }
    public double Rating { get; set; }
    public string PosterUrl { get; set; }
    public bool IsFavorite { get; set; }
}

public class MovieDetailDto : MovieResponse
{
    public string Description { get; set; }
    public string Director { get; set; }
    public string Cast { get; set; }
    public List<string> ScreenshotUrls { get; set; }
}

public class FavoriteActionResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}


