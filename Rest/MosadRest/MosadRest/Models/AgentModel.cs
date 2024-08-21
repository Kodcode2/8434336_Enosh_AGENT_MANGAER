using System.ComponentModel.DataAnnotations;

namespace MosadRest.Models
{
    public class AgentModel
    {
        public int Id { get; set; }
        [Required]
        public required string  NicName { get; set; }
        [Required]
        public required string Photo_url { get; set; }
        [Required]
        public required int  X_Waypoint { get; set; } = 201;
        [Required]
        public required int  Y_Waypoint { get; set; } = 201;
        [Required]
        public required AgentStstus Ststus { get; set; } = AgentStstus.InActive;
        public List<MissionModel> Missions { get; set; } = [];


    }
    public enum AgentStstus
    {
        InActive, 
        Active
    }
}
