using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MosadRest.Models
{
    public class TargetModel
    {
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Position { get; set; }
        [AllowNull]
        public string PhotoUrl { get; set; }
        [Required]
        public int XWaypoint { get; set; } = -201;
        [Required]
        public int YWaypoint { get; set; } = -201;
        public TargetStatus Status { get; set; } = TargetStatus.live;
        public List<MissionModel> Missions { get; set; } = [];
        public bool IsHunted { get; set; } = false;

    }
        public enum TargetStatus
        {
            live,
            dead
        }

}



