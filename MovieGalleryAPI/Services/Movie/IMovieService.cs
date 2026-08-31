using MovieGalleryAPI.DTO;
using MovieGalleryAPI.DTO.Response;

namespace MovieGalleryAPI.Services.Movie;

public interface IMovieService
{
    Task<List<MovieResponse>> GetPopularAsync(int userId);
    Task<List<MovieResponse>> SearchAsync(string keyword, int userId);

    Task<MovieDetailDto> GetByIdAsync(int movieId, int userId);
}
