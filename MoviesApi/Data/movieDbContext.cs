using Microsoft.EntityFrameworkCore;
using MoviesApi.Model;

namespace MoviesApi.Data
{
    public class movieDbContext: DbContext
    {
        public movieDbContext( DbContextOptions<movieDbContext>options):base(options)   
        {       
        }
        
        public DbSet<Movie>movie { get; set; }
        public DbSet<TypeMovie>TypeMovie { get; set; }
    }
}
