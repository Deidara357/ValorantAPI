using System.Collections.Generic;

namespace ValorantAPI.Models
{
    public class AgentsResponse
    {
        public string AgentName { get; set; }
        public string Description { get; set; }
        public string FullPortrait { get; set; }
        public string Role { get; set; }
        public string RoleDescription { get; set; }
        public List<Abilities> AgentsAbilities { get; set; }
    }
}
