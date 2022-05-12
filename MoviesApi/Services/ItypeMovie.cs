using MoviesApi.Model;
using System.Collections;

namespace MoviesApi.Services
{
    public interface ItypeMovie
    {
        Task<IEnumerable<TypeMovie>> GetAll();
        Task<TypeMovie> GetId(byte id);
        Task <TypeMovie> Add(TypeMovie typeMovie);
        TypeMovie Update(TypeMovie typeMovie);   
        TypeMovie Delete(TypeMovie typeMovie);
        Task<bool> Isvalidtype(byte id);

    }
}
