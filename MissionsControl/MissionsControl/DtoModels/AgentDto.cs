using MissionsControl.Models;
using System.ComponentModel.DataAnnotations;

namespace MissionsControl.DtoModels
{
    
    public class AgentDto
    {
        public int Id { get; set; }
        
        public string NickName { get; set; }
        
        public string PhotoUrl { get; set; }
        
        public int XWaypoint { get; set; }
        
        public int YWaypoint { get; set; }
        
        public AgentStatus Status { get; set; } = AgentStatus.InActive;


    }
    public enum AgentStatus
    {
        InActive,
        Active
    }
}



