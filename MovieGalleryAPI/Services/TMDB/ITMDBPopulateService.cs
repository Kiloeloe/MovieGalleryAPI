namespace MovieGalleryAPI.Services.TMDB;

public interface ITMDBPopulateService
{
    Task<int> EnrichMoviesAsync();
}
