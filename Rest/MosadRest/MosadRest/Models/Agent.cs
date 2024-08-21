using System.ComponentModel.DataAnnotations;

namespace MosadRest.Models
{
    public class Agent
    {
        [Required]
        public required string  NicName { get; set; }
        [Required]
        public required string photo_url { get; set; }     
        public (int x, int y) Waypoint { get; set; } = (-1,-1);
        public Enum ststus { get; set; }
    }
}
