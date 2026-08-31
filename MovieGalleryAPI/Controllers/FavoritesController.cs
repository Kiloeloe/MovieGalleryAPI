using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieGalleryAPI.Helpers;
using MovieGalleryAPI.Services;
using MovieGalleryAPI.Services.Favorites;

namespace MovieGalleryAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class FavoritesController : ControllerBase
{
    private readonly IFavoritesService _favoritesService;

    public FavoritesController(IFavoritesService favoritesService)
    {
        _favoritesService = favoritesService;
    }

    ///2. adds a movie to the current user's favorites. (POST 2/3)
    [HttpPost("{movieId:int}")]
    public async Task<IActionResult> AddFavorite(int movieId)
    {
        var result = await _favoritesService.AddFavoriteAsync(User.GetUserId(), movieId);
        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }

    /// 3. removes a movie from the current user's favorites. (POST 3/3)
    [HttpPost("{movieId:int}/remove")]
    public async Task<IActionResult> RemoveFavorite(int movieId)
    {
        var result = await _favoritesService.RemoveFavoriteAsync(User.GetUserId(), movieId);
        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }

    //extra endpoint: list user favorites
    [HttpGet]
    public async Task<IActionResult> GetFavorites()
    {
        var favorites = await _favoritesService.GetFavoritesAsync(User.GetUserId());
        return Ok(favorites);
    }
}
