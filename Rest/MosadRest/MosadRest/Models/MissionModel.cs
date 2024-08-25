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
        public required double TimeLeft { get; set; }
        public double? TotalExecutionTime { get; set; }
        [Required]
        public required MissionStatus MissionStatus { get; set; } = MissionStatus.offer;
        public DateTime _StartTime { get; set; }
    }
}


public enum MissionStatus
{
    offer,
    team,
    finished
}