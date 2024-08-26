using MosadRest.Models;

namespace MosadRest.DtoModels
{
    public class MissionFullDetailsDto
    {
        public int Id { get; set; }

        public int AgentId { get; set; }
        public AgentDatailDto Agent { get; set; }

        public int TargetId { get; set; }
        public TargetDatailDto Target { get; set; }

        public double TimeLeft { get; set; }
        public double? TotalExecutionTime { get; set; }
        public MissionStatus MissionStatus { get; set; } = MissionStatus.offer;
        public DateTime _StartTime { get; set; }

    }
}
