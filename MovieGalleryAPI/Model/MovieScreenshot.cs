namespace MovieGalleryAPI.Model;

public class MovieScreenshot
{
    public int Id { get; set; }

    public int MovieId { get; set; }
    public Movie Movie { get; set; }

    public string ImageUrl { get; set; } 

    public int DisplayOrder { get; set; }
}
