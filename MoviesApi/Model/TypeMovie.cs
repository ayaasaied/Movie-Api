using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesApi.Model
{
    public class TypeMovie
    {
        //generate id 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }

        [MaxLength(100)]
        [Required]//by default in .net 6 all string is required
        //in setting app nullable is enable 
        public string Name { get; set; }

    }
}
