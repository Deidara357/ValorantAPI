using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ValorantAPI.Clients;
using ValorantAPI.Extensions;
using ValorantAPI.Models;

namespace ValorantAPI.Repositories
{
    public class AgentRepository : IAgentsRepository
    {
        private readonly AgentContext _agentContext;
        private readonly ValorantClient _valorantClient;

        public AgentRepository(AgentContext agentContext, ValorantClient valorantClient)
        {
            _agentContext = agentContext;
            _valorantClient = valorantClient;
        }

        public async Task<AgentsNewResponse> Add(string agentName)
        {
            var agent = await _valorantClient.GetAgent(agentName);

            var data = agent.Data.FirstOrDefault(a => a.DisplayName == agentName);

            if (data == null)
            {
                return null;
            }
            else
            {
                var result = new AgentsNewResponse
                {
                    AgentName = data.DisplayName,
                    Description = data.Description,
                    FullPortrait = data.FullPortraitV2,
                    Role = data.Role.DisplayName
                };

                _agentContext.Agents.Add(result);
                await _agentContext.SaveChangesAsync();

                return result;
            }
        }

        public async Task Delete(string agentNmae)
        {
            var agentToDelete = await _agentContext.Agents.FindAsync(agentNmae);
            _agentContext.Agents.Remove(agentToDelete);
            await _agentContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<AgentsNewResponse>> Get()
        {
            return await _agentContext.Agents.ToListAsync();
        }

        public async Task<AgentsNewResponse> Get(string agentName)
        {
            return await _agentContext.Agents.FindAsync(agentName);
        }
    }
}
