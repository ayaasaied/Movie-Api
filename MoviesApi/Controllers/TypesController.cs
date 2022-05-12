using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Dtos;
using MoviesApi.Model;
using MoviesApi.Services;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly ItypeMovie TypeMovieRepo;

        public TypesController(ItypeMovie _typeMovieRepo)
        {
            TypeMovieRepo = _typeMovieRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Alltypes = await TypeMovieRepo.GetAll();
            return Ok(Alltypes);
        }
        [HttpPost("s")]
        public async Task<IActionResult> Create(TypesDtos typesDtos)
        {
            var TypesMovie = new TypeMovie { Name = typesDtos.Name };
            await TypeMovieRepo.Add(TypesMovie);
            return Ok(TypesMovie);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> update(byte id,[FromBody]TypesDtos typesDtos)
        { 
            var typeid= await TypeMovieRepo.GetId(id);
            if (typeid == null)
                return NotFound($"no type films with this id :{id}");
            typeid.Name = typesDtos.Name;
            TypeMovieRepo.Update(typeid);
            return Ok(typeid);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(  byte id)
        {
            var typeid = await TypeMovieRepo.GetId(id);
            if (typeid == null)
                return NotFound($"no type films with this id :{id}");
            
            TypeMovieRepo.Delete(typeid);
            return Ok(typeid);
        }



    } }

