using MovieGalleryAPI.DTO.Response;

namespace MovieGalleryAPI.Services.Favorites;

public interface IFavoritesService
{
    Task<FavoriteActionResponse> AddFavoriteAsync(int userId, int movieId);
    Task<FavoriteActionResponse> RemoveFavoriteAsync(int userId, int movieId);
    Task<List<MovieResponse>> GetFavoritesAsync(int userId);
}
