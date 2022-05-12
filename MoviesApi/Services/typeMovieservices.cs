using Microsoft.EntityFrameworkCore;
using MoviesApi.Data;
using MoviesApi.Model;
using System;

namespace MoviesApi.Services
{
    public class typeMovieservices : ItypeMovie
    {
        private readonly movieDbContext  movieDB;
       

        public typeMovieservices(movieDbContext _movieDB)
        {
            movieDB = _movieDB;
        }

        public async Task<IEnumerable<TypeMovie>> GetAll()
        {
            return await movieDB.TypeMovie.OrderBy(s=>s.Name).ToListAsync();
        }

        public async Task<TypeMovie> GetId(byte id)
        {
             return await movieDB.TypeMovie.SingleOrDefaultAsync(x => x.Id == id);
        }
        public async Task<TypeMovie> Add(TypeMovie typeMovie)
        {
            await movieDB.TypeMovie.AddAsync(typeMovie);
            movieDB.SaveChanges();
            return typeMovie;
        }


        public TypeMovie Update(TypeMovie typeMovie)
        {
            movieDB.TypeMovie.Update(typeMovie);
            movieDB.SaveChanges();
            return typeMovie;
        }

        public TypeMovie Delete(TypeMovie typeMovie)
        {
             movieDB.TypeMovie.Remove(typeMovie);
            movieDB.SaveChanges();
            return typeMovie;
        }


        public Task<bool> Isvalidtype(byte id)
        {
            return movieDB.TypeMovie.AnyAsync(g => g.Id == id);
        }
    }
}
