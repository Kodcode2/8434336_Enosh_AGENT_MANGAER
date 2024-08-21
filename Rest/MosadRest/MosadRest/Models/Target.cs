using System.ComponentModel.DataAnnotations;

namespace MosadRest.Models
{
    public class Target
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string position { get; set; }
        public required string photo_url { get; set; }
        public (int x, int y) Waypoint { get; set; } = (-1, -1);
        public Enum ststus { get; set; }
    }
}

