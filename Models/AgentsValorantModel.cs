using System.Collections.Generic;

namespace ValorantAPI.Models
{
    public class AgentsValorantModel
    {
        public List<Data> Data { get; set; }
    }

    public class Data
    {
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string FullPortraitV2 { get; set; }
        public Role Role { get; set; }
        public List<Abilities> Abilities { get; set; }
    }

    public class Role
    {
        public string DisplayName { get; set; }
        public string Description { get; set; }
    }

    public class Abilities
    {
        public string Slot { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
    }
}
