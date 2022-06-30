using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ValorantAPI.Clients;
using ValorantAPI.MapModels;
using ValorantAPI.Models;
using ValorantAPI.WeaponsModels;

namespace ValorantAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValorantController : ControllerBase
    {
        private readonly ILogger<ValorantController> _logger;
        private readonly ValorantClient _valorantClient;

        public ValorantController(ILogger<ValorantController> logger, ValorantClient valorantClient)
        {
            _logger = logger;
            _valorantClient = valorantClient;
        }

        [HttpGet("Agents")]
        public async Task<ActionResult<AgentsResponse>> GetAgent([FromQuery] string agentName)
        {
            var agent = await _valorantClient.GetAgent(agentName);

            var data = agent.Data.FirstOrDefault(a => a.DisplayName == agentName);

            if (data == null)
            {
                return Problem(detail: "Wrong Name!");
            }
            else
            {
                var result = new AgentsResponse
                {
                    AgentName = data.DisplayName,
                    Description = data.Description,
                    FullPortrait = data.FullPortraitV2,
                    Role = data.Role.DisplayName,
                    RoleDescription = data.Role.Description,
                    AgentsAbilities = data.Abilities
                };

                return result;
            }
        }

        [HttpGet("Maps")]
        public async Task<ActionResult<MapsResponse>> GetMap([FromQuery] string mapName)
        {
            var map = await _valorantClient.GetMap(mapName);

            var data = map.Data.FirstOrDefault(a => a.DisplayName == mapName);

            if (data == null)
            {
                return Problem(detail: "Wrong Name!");
            }
            else
            {
                var result = new MapsResponse
                {
                    MapName = data.DisplayName,
                    Photo = data.Splash
                };

                return result;
            }
        }

        [HttpGet("Weapons")]
        public async Task<ActionResult<WeaponsResponse>> GetWeapon([FromQuery] string weaponName)
        {
            var weapon = await _valorantClient.GetWeapon(weaponName);

            var data = weapon.Data.FirstOrDefault(a => a.DisplayName == weaponName);

            if (data == null)
            {
                return Problem(detail: "Wrong Name!");
            }
            else
            {
                if (weaponName == "Холодное оружие")
                {
                    var result = new WeaponsResponse
                    {
                        WeaponName = data.DisplayName,
                        Photo = data.DisplayIcon,
                    };

                    return result;
                }
                else
                {
                    var result = new WeaponsResponse
                    {
                        WeaponName = data.DisplayName,
                        Photo = data.DisplayIcon,
                        FireRate = data.WeaponStats.FireRate.ToString(),
                        MagazineSize = data.WeaponStats.MagazineSize.ToString(),
                        EquipTimeSeconds = data.WeaponStats.EquipTimeSeconds.ToString(),
                        ReloadTimeSeconds = data.WeaponStats.ReloadTimeSeconds.ToString(),
                        Damage = data.WeaponStats.DamageRanges,
                        Cost = data.ShopData.Cost.ToString(),
                        WeaponType = data.ShopData.CategoryText
                    };

                    return result;
                }
            }
        }

        [HttpGet("Skins")]
        public async Task<ActionResult<WeaponSkinsModel>> GetSkins([FromQuery] string weaponName)
        {
            var weapon = await _valorantClient.GetWeapon(weaponName);

            var data = weapon.Data.FirstOrDefault(a => a.DisplayName == weaponName);

            if (data == null)
            {
                return Problem(detail: "Wrong Name!");
            }
            else
            {
                var result = new WeaponSkinsModel
                {
                    WeaponName = data.DisplayName,
                    WeaponSkins = data.Skins
                };

                return result;
            }
        }
    }
}
