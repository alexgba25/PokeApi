using PokeApi.Models;

namespace PokeApi.Services
{
    public interface IPokemonService
    {
        // Paso 4: Services(Interfaz, Clase)
        Task <Pokemon> GetPokemonAsync (int id);
        Task<Pokemon[]> GetFirstPokemonAsync(int count);
    }
}
