using System.ComponentModel.DataAnnotations;

namespace ValorantAPI.MapModels
{
    public class MapsResponse
    {
        [Key]
        public string MapName { get; set; }

        public string Photo { get; set; }
    }
}
