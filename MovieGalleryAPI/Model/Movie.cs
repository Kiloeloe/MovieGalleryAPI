namespace MovieGalleryAPI.Model;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Genre { get; set; }
    public int ReleaseYear { get; set; }
    public string Director { get; set; }
    public string Cast { get; set; }
    public double Rating { get; set; }
    public string PosterUrl { get; set; }
    public int PopularityScore { get; set; }
    public int? TmdbId { get; set; }

    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    public ICollection<MovieScreenshot> Screenshots { get; set; } = new List<MovieScreenshot>();
}

