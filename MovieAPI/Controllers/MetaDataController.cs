using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Model;
using MovieAPI.Service;

namespace MovieAPI.Controllers
{
    [ApiController]
    public class MetaDataController : ControllerBase
    {
        //private readonly IMovieService _movieService;

        public MetaDataController(IMovieService movieService)
        {
            MovieService = movieService;
        }

        public IMovieService MovieService { get; }

        [HttpPost("metadata")]
        public async Task<IActionResult> PostMetaData(MetaData metaData)
        {
            await Task.Run(() => MovieService.SaveMetaData(metaData));
            return Ok();
        }

        [HttpGet("metadata/{movieId}")]
        public async Task<IActionResult> GetMetaData(int movieId)
        {
            var result = await Task.Run(() => MovieService.GetMetaDataMovieById(movieId));

            if (result.Any())
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpGet("movies/stats")]
        public async Task<IActionResult> GetMovieStats()
        {
            var result = await Task.Run(() => MovieService.GetMovieStats());
            return Ok(result);
        }
    }
}
