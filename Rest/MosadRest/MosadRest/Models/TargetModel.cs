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
        public string Photo_url { get; set; }
        [Required]
        public required int X_Waypoint { get; set; } = 201;
        [Required]
        public required int Y_Waypoint { get; set; } = 201;
        public required TargetStstus Ststus  { get; set; } = TargetStstus.live;
        public List<MissionModel> Missions { get; set; } = [];
    }
    public enum TargetStstus
    {
        live,
        dead
    }

}



