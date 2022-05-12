using MoviesApi.Model;

namespace MoviesApi.Services
{
    public interface Imovie
    {
        Task<IEnumerable<Movie>> GetAll(byte genreId = 0);
        Task<Movie> GetId(int id);
        Task<Movie> Add(Movie movie);
        Movie Update(Movie movie);
        Movie Delete(Movie movie);

    }
}
