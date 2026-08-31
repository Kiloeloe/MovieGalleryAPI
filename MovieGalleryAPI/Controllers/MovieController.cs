using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieGalleryAPI.Helpers;
using MovieGalleryAPI.Services;
using MovieGalleryAPI.Services.Movie;

namespace MovieGalleryAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    //2. get list of popular movies (GET 1/3)
    [HttpGet("popular")]
    public async Task<IActionResult> GetPopular()
    {
        var movies = await _movieService.GetPopularAsync(User.GetUserId());
        return Ok(movies);
    }

    /// 2. search movies by title, genre or director keyword. (GET 2/3)
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return BadRequest(new { message = "A 'keyword' query parameter is required." });

        var movies = await _movieService.SearchAsync(keyword, User.GetUserId());
        return Ok(movies);
    }

    ///3. get movies by id/  full details for one movie. (GET 3/3)
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var movie = await _movieService.GetByIdAsync(id, User.GetUserId());
        if (movie is null)
            return NotFound(new { message = $"Movie with id {id} was not found." });

        return Ok(movie);
    }
}
