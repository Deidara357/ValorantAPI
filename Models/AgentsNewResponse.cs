using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ValorantAPI.Models
{
    public class AgentsNewResponse
    {
        [Key]
        public string AgentName { get; set; }

        public string Description { get; set; }
        public string FullPortrait { get; set; }
        public string Role { get; set; }
    }
}
