using MovieGalleryAPI.DTO;

namespace MovieGalleryAPI.Services.TMDB;

public interface ITMDBImageService
{
    Task<TmdbMovieMatch?> FindMovieAsync(string title, int? releaseYear);
    Task<TmdbMovieImages> GetImagesAsync(int tmdbId);
}
