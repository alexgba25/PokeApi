using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokeApi.Models;
using PokeApi.Services;

namespace PokeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        // Paso 5: Controller

        private readonly IPokemonService _pokeService;

        public PokemonController(IPokemonService pokeService)
        {
            _pokeService = pokeService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPokemon(int id)
        {
            try
            {
                var pokemon = await _pokeService.GetPokemonAsync(id);
                return Ok(pokemon);
            }
            catch (HttpRequestException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetFirstPokemon([FromQuery] int count = 121)
        {
            var pokemon = await _pokeService.GetFirstPokemonAsync(count);
            return Ok(pokemon);
        }

    }
}
