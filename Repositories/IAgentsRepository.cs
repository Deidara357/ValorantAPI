using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ValorantAPI.Models;

namespace ValorantAPI.Repositories
{
    public interface IAgentsRepository
    {
        Task<IEnumerable<AgentsNewResponse>> Get();
        Task<AgentsNewResponse> Get(string agentName);
        Task<AgentsNewResponse> Add(string agentName);
        Task Delete(string agentName);
    }
}
