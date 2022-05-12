//using System.ComponentModel.DataAnnotations;
//max lenght is fire on global class
namespace MoviesApi.Model
{
    public class Movie
    {
        public int Id { get; set; }

        [MaxLength(250)]

        public string Title { get; set; }

        public int Year { get; set; }

        public double Rate { get; set; }

        [MaxLength(2500)]
        public string Storeline { get; set; }

        public byte[] Poster { get; set; }

        public byte TypeMovieId { get; set; }

        public TypeMovie TypeMovie { get; set; }
    }
}
