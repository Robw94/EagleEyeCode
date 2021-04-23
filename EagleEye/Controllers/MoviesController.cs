using EagleEye.Dtos;
using EagleEye.Queries;
using EagleEye.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EagleEye.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly IMovieQueries _movieQueries;
        private readonly IMoviesService _moviesService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="movieQueries"></param>
        /// <param name="moviesService"></param>
        public MoviesController(ILogger<MoviesController> logger, IMovieQueries movieQueries, IMoviesService moviesService)
        {
            _logger = logger;
            this._movieQueries = movieQueries;
            this._moviesService = moviesService;
        }


        /// <summary>
        /// Get List of Movies by MovieId
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        [HttpGet("metadata/:movieid")]
        [SwaggerOperation(nameof(GetMovies))]
        [ProducesResponseType(typeof(List<MovieDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public ActionResult<List<MovieDto>> GetMovies([FromQuery]int movieId)
        {
            try
            {
                var movies = _movieQueries.GetMovies(movieId);

                if (movies.Any())
                {
                    return movies;

                } else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        /// <summary>
        /// Get Movie Stats
        /// </summary>
        /// <returns></returns>
        [HttpGet("movies/stats")]
        [SwaggerOperation(nameof(GetMovies))]
        [ProducesResponseType(typeof(List<MovieWithStatsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public ActionResult<List<MovieWithStatsDto>> GetMoviesStats()
        {
            try
            {
                var movies = _movieQueries.GetMovieStats();

                if (movies.Any())
                {
                    return movies;

                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Add a New Movie
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        [HttpPost("movie")]
        [SwaggerOperation(nameof(AddMovie))]
        [ProducesResponseType(typeof(List<MovieDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public ActionResult<List<MovieDto>> AddMovie([FromBody]MovieDto movie)
        {
            try
            {
                var movies = _moviesService.AddMovie(movie);

                return movies;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
