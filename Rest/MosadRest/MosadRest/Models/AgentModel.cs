using System.ComponentModel.DataAnnotations;

namespace MosadRest.Models
{
    public class AgentModel
    {
        public int Id { get; set; }
        [Required]
        public required string  NickName { get; set; }
        [Required]
        public required string PhotoUrl { get; set; }
        [Required]
        public  int  XWaypoint { get; set; } = -201;
        [Required]
        public  int  YWaypoint { get; set; } = -201;
        [Required]
        public  AgentStatus Status { get; set; } = AgentStatus.InActive;


    }
    public enum AgentStatus
    {
        InActive, 
        Active
    }
}
