using Microsoft.EntityFrameworkCore;
using MovieGalleryAPI.Data;
using MovieGalleryAPI.DTO.Response;
using MovieGalleryAPI.Model;

namespace MovieGalleryAPI.Services.Favorites;


public class FavoritesService : IFavoritesService
{
    private readonly AppDbContext _context;

    public FavoritesService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<FavoriteActionResponse> AddFavoriteAsync(int userId, int movieId)
    {
        var movieExists = await _context.Movies.AnyAsync(m => m.Id == movieId);
        if (!movieExists)
            return new FavoriteActionResponse { Success = false, Message = "Movie not found." };

        var alreadyFavorited = await _context.Favorites
            .AnyAsync(f => f.UserId == userId && f.MovieId == movieId);

        if (alreadyFavorited)
            return new FavoriteActionResponse { Success = true, Message = "Movie is already in favorites." };

        _context.Favorites.Add(new Favorite { UserId = userId, MovieId = movieId });
        await _context.SaveChangesAsync();

        return new FavoriteActionResponse { Success = true, Message = "Movie added to favorites." };
    }

    public async Task<FavoriteActionResponse> RemoveFavoriteAsync(int userId, int movieId)
    {
        var favorite = await _context.Favorites
            .FirstOrDefaultAsync(f => f.UserId == userId && f.MovieId == movieId);

        if (favorite is null)
            return new FavoriteActionResponse { Success = false, Message = "Movie was not in favorites." };

        _context.Favorites.Remove(favorite);
        await _context.SaveChangesAsync();

        return new FavoriteActionResponse { Success = true, Message = "Movie removed from favorites." };
    }

    public async Task<List<MovieResponse>> GetFavoritesAsync(int userId)
    {
        return await _context.Favorites
            .Where(f => f.UserId == userId)
            .Select(f => new MovieResponse
            {
                Id = f.Movie.Id,
                Title = f.Movie.Title,
                Genre = f.Movie.Genre,
                ReleaseYear = f.Movie.ReleaseYear,
                Rating = f.Movie.Rating,
                PosterUrl = f.Movie.PosterUrl,
                IsFavorite = true
            })
            .ToListAsync();
    }
}
