using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ValorantAPI.Clients;

namespace ValorantAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ValorantNewsController : ControllerBase
    {
        private readonly ValorantNewsClient _valorantNewsClient;
        private static string _addressForNews;
        private static string _addressForTournaments;

        public ValorantNewsController(ValorantNewsClient valorantNewsClient)
        {
            _valorantNewsClient = valorantNewsClient;

            _addressForNews = Constants.UrlForNews;
            _addressForTournaments = Constants.UrlForTournaments;
        }

        [HttpGet("ValorantNews")]
        public async Task<ActionResult<string>> GetNews()
        {
            var content = await _valorantNewsClient.NewsParsing(_addressForNews);

            if (content == null)
            {
                return Problem(detail: "No News!");
            }
            else
            {
                string result = "";

                for (int i = 0; i < content.Count; i++)
                {
                    result += content[i];
                }

                return result;
            }
        }

        [HttpGet("ValorantTournaments")]
        public async Task<ActionResult<string>> GetTournaments()
        {
            var content = await _valorantNewsClient.TournamentsParsing(_addressForTournaments);

            if (content == null)
            {
                return Problem(detail: "No Tournaments!");
            }
            else
            {
                string result = "";

                for (int i = 0; i < content.Count; i++)
                {
                    result += content[i];
                }

                return result;
            }
        }
    }
}
