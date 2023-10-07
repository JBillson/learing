using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Models;
using MoviesAPI.Models.Dto;
using MoviesAPI.Store;

namespace MoviesAPI.Controllers;

[ApiController]
[Route("api/movies")]
public class MoviesApiController : ControllerBase
{
    private readonly ILogger<MoviesApiController> _logger;

    public MoviesApiController(ILogger<MoviesApiController> logger)
    {
        _logger = logger;
    }
    
    // CREATE 
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [Route("", Name = nameof(CreateMovie))]
    public ActionResult<MovieDto> CreateMovie([FromBody] MovieDto movieRequest)
    {
        if (MovieStore.Movies.FirstOrDefault(m => m.Name.ToLower() == movieRequest.Name.ToLower()) != null)
            return Conflict("Movie already exists");

        var movie = new Movie
        {
            Id = MovieStore.Movies.Max(m => m.Id) + 1,
            Name = movieRequest.Name,
            Rating = movieRequest.Rating
        };
        MovieStore.Movies.Add(movie);

        return CreatedAtRoute(nameof(GetMovieById), new { id = movie.Id }, movie);
    }

    // READ
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("", Name = nameof(GetAllMovies))]
    public ActionResult<List<MovieDto>> GetAllMovies()
    {
        _logger.LogInformation("Getting all movies");
        var movies = MovieStore.Movies;
        if (movies.Count == 0)
        {
            _logger.LogError("No movies found");
            return NoContent();
        }
        
        return Ok(movies);
    }

    // READ
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("{id:int}", Name = nameof(GetMovieById))]
    public ActionResult<MovieDto> GetMovieById(int id)
    {
        _logger.LogInformation($"Getting movie by ID {id}");
        var movie = MovieStore.Movies.FirstOrDefault(m => m.Id == id);
        if (movie == null)
        {
            _logger.LogError($"Movie with ID {id} not found");
            return NotFound();
        }
    
        return Ok(movie);
    }
    
    // READ
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("{name}", Name = nameof(GetMovieByName))]
    public ActionResult<MovieDto> GetMovieByName(string name)
    {
        var movie = MovieStore.Movies.FirstOrDefault(m => m.Name == name);
        if (movie == null)
            return NotFound();
        
        return Ok(movie);
    }
    
    // UPDATE
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("rating/{id:int}", Name = nameof(RateMovieById))]
    public ActionResult<MovieDto> RateMovieById(int id, int rating)
    {
        var movie = MovieStore.Movies.FirstOrDefault(m => m.Id == id);
        if (movie == null)
            return NotFound($"Movie with ID {id} does not exist");

        movie.Rating = rating;
        return Ok(movie);
    }
    
    // WHAT IS USEFUL ABOUT USING JSON PATCH DOCUMENTS?
    // WHY NOT JUST UPDATE THE RESOURCE DIRECTLY AS WE DO IN THE PREVIOUS ENDPOINT?
    
    // UPDATE
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("{id:int}", Name = nameof(UpdateMovieById))]
    public ActionResult<MovieDto> UpdateMovieById(int id, JsonPatchDocument<MovieDto> patchDocument)
    {
        var movie = MovieStore.Movies.FirstOrDefault(m => m.Id == id);
        if (movie == null)
            return NotFound($"Movie with ID {id} does not exist");
    
        var movieDto = new MovieDto
        {
            Name = movie.Name,
            Rating = movie.Rating
        };
        
        patchDocument.ApplyTo(movieDto, ModelState);
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return Ok(movie);
    }
    
    // DELETE
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("{id:int}", Name = nameof(DeleteMovieById))]
    public ActionResult<string> DeleteMovieById(int id)
    {
        var movie = MovieStore.Movies.FirstOrDefault(m => m.Id == id);
        if (movie == null)
            return NotFound($"Movie with ID {id} not found");
        
        MovieStore.Movies.Remove(movie);
        return Ok($"Movie ({movie.Name}) deleted");
    }
    
    // DELETE
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("{name}", Name = nameof(DeleteMovieByName))]
    public ActionResult<string> DeleteMovieByName(string name)
    {
        var movie = MovieStore.Movies.FirstOrDefault(m => m.Name == name);
        if (movie == null)
            return NotFound($"Movie ({name}) not found");
        
        MovieStore.Movies.Remove(movie);
        return Ok($"Movie ({movie.Name}) deleted");
    }
}