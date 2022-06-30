using Microsoft.EntityFrameworkCore;

namespace ValorantAPI.Models
{
    public class AgentContext : DbContext
    {
        public AgentContext(DbContextOptions<AgentContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<AgentsNewResponse> Agents { get; set; }
    }
}
