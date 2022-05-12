using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Dtos;
using MoviesApi.Model;
using MoviesApi.Services;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly Imovie movierepo;
        private readonly ItypeMovie typeMovieRepo;


        private new readonly List<string> _allowedExtenstions = new List<string> { ".jpg", ".png" };
        private long _maxAllowedPosterSize = 1048576;

        public bool Isvalidtype { get; private set; }

        public MovieController(Imovie movierepo, ItypeMovie itypeMovieRepo)
        {
            this.movierepo = movierepo;
            this.typeMovieRepo = itypeMovieRepo;
        }

     

        [HttpGet]
        public async Task<IActionResult>  GetAll()
        {
            var movies= await movierepo.GetAll();
            return Ok(movies);
        }
        [HttpGet ("{id}")]
        public async Task<IActionResult> GetbyId(int id)
        {
            var movie = await movierepo.GetId(id);

            if (movie == null)
                return NotFound("not found id :{id}");

            return Ok(movie);
        }

        [HttpGet("AllmovieId")]
        public async Task<IActionResult> GetByAllmovieId(byte typeid)
        {
            var movies = await movierepo.GetAll(typeid);

            return Ok(movies);
        }



        [HttpPost]
        public async Task<IActionResult> create([FromForm] MovieDots movieDots)
        {
            if (movieDots.Poster == null)
                return BadRequest("Poster is required!");

            if (!_allowedExtenstions.Contains(Path.GetExtension(movieDots.Poster.FileName).ToLower()))
                return BadRequest("Only .png and .jpg images are allowed!");

            if (movieDots.Poster.Length > _maxAllowedPosterSize)
                return BadRequest("Max allowed size for poster is 1MB!");
            //check if typemovie is found
            var isValidmovietype = await typeMovieRepo.Isvalidtype(movieDots.GenreId);

            if (!isValidmovietype)
                return BadRequest("Invalid type movie ID!");

            using var dataStream = new MemoryStream();

            await movieDots.Poster.CopyToAsync(dataStream);
            var Movie = new Movie
            {
                TypeMovieId = movieDots.GenreId,
                Title = movieDots.Title,
                Poster = dataStream.ToArray(),
                Rate = movieDots.Rate,
                Storeline = movieDots.Storeline,
                Year = movieDots.Year,
            };

            await movierepo.Add(Movie);
            return Ok(Movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> update(int id ,[FromForm] MovieDots dto )
        {
            var movie = await movierepo.GetId(id);

            if (movie == null)
                return NotFound($"No movie was found with ID {id}");

            var isValidGenre = await typeMovieRepo.Isvalidtype(dto.GenreId);

            if (!isValidGenre)
                return BadRequest("Invalid genere ID!");

            if (dto.Poster != null)
            {
                if (!_allowedExtenstions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("Only .png and .jpg images are allowed!");

                if (dto.Poster.Length > _maxAllowedPosterSize)
                    return BadRequest("Max allowed size for poster is 1MB!");

                using var dataStream = new MemoryStream();

                await dto.Poster.CopyToAsync(dataStream);

                movie.Poster = dataStream.ToArray();
            }
            //old=dtonew
            movie.Title = dto.Title;
            movie.TypeMovieId = dto.GenreId;
            movie.Year = dto.Year;
            movie.Storeline = dto.Storeline;
            movie.Rate = dto.Rate;

            movierepo.Update(movie);

            return Ok(movie);
        }
    


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var movie = await movierepo.GetId(id);
            if (movie == null)
            return NotFound($"No movie was found with ID {id}");
             movierepo.Delete(movie);
            return Ok(movie);
            
        }
    }
}
