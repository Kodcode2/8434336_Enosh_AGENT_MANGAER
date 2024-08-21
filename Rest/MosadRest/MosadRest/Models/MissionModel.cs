using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MosadRest.Models
{
    public class MissionModel
    {
        public int Id { get; set; }
        [Required] 
        public required int AgentId { get; set; }
        public AgentModel Agent { get; set; }
        [Required]
        public required int TargetId { get; set; }
        public TargetModel Target { get; set; }
        [Required]
        public required float TimeLeft { get; set; }
        [AllowNull]
        public float TotalExecutionTime { get; set; }
        [Required]
        public required MissionStstus MissionStstus { get; set; } = MissionStstus.offer;
    }

}
public enum MissionStstus
{
    offer,
    team,
    finished
}