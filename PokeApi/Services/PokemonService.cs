using Newtonsoft.Json.Linq;
using PokeApi.Models;

namespace PokeApi.Services
{
    public class PokemonService : IPokemonService
    {
        // Paso 4: Services(Interfaz, Clase)

        private readonly HttpClient _httpClient;
        public PokemonService(HttpClient httpClient) 
        {
          _httpClient = httpClient;
        }
        public async Task<Pokemon[]> GetFirstPokemonAsync(int count)
        {
            var taks = new List<Task<Pokemon>>();
            for (int i = 1; i <= count; i++)
            {
                taks.Add(GetPokemonAsync(i));
            }
            var results = await Task.WhenAll(taks);
            return results;
        }


        public async Task<Pokemon> GetPokemonAsync(int id)
        {
            var response = await _httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{id}");
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(data);

            var pokemon = new Pokemon
            {
                Id = json["id"].Value<int>(),
                Name = json["name"].Value<string>(),
                Types = json["types"].Select(t => t["type"]["name"].Value<string>()).ToArray(),
                ImageUrl = json["sprites"]["front_default"].Value<string>()
            };

            return pokemon;
        }
    }
}
