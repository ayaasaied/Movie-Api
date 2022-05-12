using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;
using MoviesApi.Model;

namespace MoviesApi.Services
{
    public class movieservise : Imovie
    {
        private readonly movieDbContext movieDB;

        public movieservise(movieDbContext movieDB)
        {
            this.movieDB = movieDB;
        }

        public async Task<IEnumerable<Movie>> GetAll(byte typeId=0)
        {
            return await movieDB.movie
            .Include(s => s.TypeMovie).OrderBy(s => s.Rate)
            .Where(s=>s.TypeMovieId== typeId || typeId==0) .ToListAsync();
                }

        public async Task<Movie> GetId(int id)
        {
            return await movieDB.movie.Include(m=>m.TypeMovie)
                                      .SingleOrDefaultAsync(s=>s.Id==id);
        }
        public async Task<Movie> Add(Movie movie)
        { 
          await  movieDB.movie.AddAsync(movie);
            movieDB.SaveChanges();
            return movie;
        }

        public Movie Update(Movie movie)
        {
            movieDB.movie.Update(movie);
            movieDB.SaveChanges();
            return movie;

        }
        public Movie Delete(Movie movie)
        {
             movieDB.movie.Remove(movie);
            movieDB.SaveChanges();
            return movie;

        }

       
    }
}
