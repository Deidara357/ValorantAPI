using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ValorantAPI.Models;
using ValorantAPI.WeaponsModels;

namespace ValorantAPI.Clients
{
    public class ValorantClient
    {
        private HttpClient _client;
        private static string _address;

        public ValorantClient()
        {
            _address = Constants.Address;

            _client = new HttpClient();
            _client.BaseAddress = new Uri(_address);
        }

        public async Task<AgentsValorantModel> GetAgent(string agentName)
        {
            var response = await _client.GetAsync($"/v1/agents?language=ru-RU&isPlayableCharacter=true&displayName={agentName}");
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<AgentsValorantModel>(content);

            return result;
        }

        public async Task<MapsModel> GetMap(string mapName)
        {
            var response = await _client.GetAsync($"/v1/maps?language=ru-RU&displayName={mapName}");
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<MapsModel>(content);

            return result;
        }

        public async Task<WeaponsModel> GetWeapon(string weaponName)
        {
            var response = await _client.GetAsync($"/v1/weapons?language=ru-RU&displayName={weaponName}");
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;

            var result = JsonConvert.DeserializeObject<WeaponsModel>(content);

            return result;
        }
    }
}
