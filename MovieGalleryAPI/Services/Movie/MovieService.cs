using Microsoft.EntityFrameworkCore;
using MovieGalleryAPI.Data;
using MovieGalleryAPI.DTO.Response;

namespace MovieGalleryAPI.Services.Movie;

public class MovieService: IMovieService
{
    private readonly AppDbContext _context;

    public MovieService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<MovieResponse>> GetPopularAsync(int userId)
    {
        var favoriteIds = await GetFavoriteIdSetAsync(userId);

        return await _context.Movies
            .OrderByDescending(m => m.PopularityScore)
            .Select(m => new MovieResponse
            {
                Id = m.Id,
                Title = m.Title,
                Genre = m.Genre,
                ReleaseYear = m.ReleaseYear,
                Rating = m.Rating,
                PosterUrl = m.PosterUrl,
                IsFavorite = favoriteIds.Contains(m.Id)
            })
            .ToListAsync();
    }

    public async Task<List<MovieResponse>> SearchAsync(string keyword, int userId)
    {
        var favoriteIds = await GetFavoriteIdSetAsync(userId);
        var normalized = keyword.Trim().ToLower();

        return await _context.Movies
            .Where(m => m.Title.ToLower().Contains(normalized)
                     || m.Genre.ToLower().Contains(normalized)
                     || m.Director.ToLower().Contains(normalized))
            .OrderByDescending(m => m.PopularityScore)
            .Select(m => new MovieResponse
            {
                Id = m.Id,
                Title = m.Title,
                Genre = m.Genre,
                ReleaseYear = m.ReleaseYear,
                Rating = m.Rating,
                PosterUrl = m.PosterUrl,
                IsFavorite = favoriteIds.Contains(m.Id)
            })
            .ToListAsync();
    }

    public async Task<MovieDetailDto?> GetByIdAsync(int movieId, int userId)
    {
        var movie = await _context.Movies
            .Include(m => m.Screenshots)
            .FirstOrDefaultAsync(m => m.Id == movieId);

        if (movie is null)
            return null;

        var isFavorite = await _context.Favorites
            .AnyAsync(f => f.UserId == userId && f.MovieId == movieId);

        return new MovieDetailDto
        {
            Id = movie.Id,
            Title = movie.Title,
            Genre = movie.Genre,
            ReleaseYear = movie.ReleaseYear,
            Rating = movie.Rating,
            PosterUrl = movie.PosterUrl,
            Description = movie.Description,
            Director = movie.Director,
            Cast = movie.Cast,
            IsFavorite = isFavorite,
            ScreenshotUrls = movie.Screenshots
            .OrderBy(s => s.DisplayOrder)
            .Select(s => s.ImageUrl)
            .ToList()
        };
    }

    private async Task<HashSet<int>> GetFavoriteIdSetAsync(int userId)
    {
        var ids = await _context.Favorites
            .Where(f => f.UserId == userId)
            .Select(f => f.MovieId)
            .ToListAsync();

        return ids.ToHashSet();
    }
}
