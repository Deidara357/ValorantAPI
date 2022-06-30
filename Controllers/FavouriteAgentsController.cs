using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ValorantAPI.Models;
using ValorantAPI.Repositories;

namespace ValorantAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FavouriteAgentsController : ControllerBase
    {
        private readonly IAgentsRepository _agentRepository;

        public FavouriteAgentsController(IAgentsRepository agentRepository)
        {
            _agentRepository = agentRepository;
        }

        [HttpGet("FavAgents")]
        public async Task<IEnumerable<AgentsNewResponse>> GetAgents()
        {
            var result = await _agentRepository.Get();

            if (result == null)
            {
                return null;
            }
            else
            {
                return result;
            }
        }

        [HttpPost("AddFavAgent")]
        public async Task<ActionResult<AgentsNewResponse>> PostAgent([FromQuery] string agentName)
        {
            //var newAgent = await _agentRepository.Add(agentName);

            //return CreatedAtAction(nameof(GetAgents), newAgent);

            var agentToAdd = await _agentRepository.Get(agentName);

            if (agentToAdd == null || agentToAdd.AgentName != agentName)
            {
                var newAgent = await _agentRepository.Add(agentName);

                if (newAgent == null)
                {
                    return Problem(detail: "Wrong Name!");
                }

                return CreatedAtAction(nameof(GetAgents), newAgent);
            }
            else
            {
                return Problem(detail: "This agent is already on the list!");
            }
        }

        [HttpDelete("DeleteAgent")]
        public async Task<ActionResult<AgentsNewResponse>> DeleteAgent(string agentName)
        {
            var agentToDelete = await _agentRepository.Get(agentName);
            if (agentToDelete == null)
            {
                return Problem(detail: "This agent is not on the list!");
            }
            else
            {
                await _agentRepository.Delete(agentToDelete.AgentName);

                return CreatedAtAction(nameof(GetAgents), agentToDelete);
            }
        }
    }
}
