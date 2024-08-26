using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using MissionsControl.Models;

namespace MissionsControl.DtoModels
{
    public class TargetDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public string PhotoUrl { get; set; }

        public int XWaypoint { get; set; }

        public int YWaypoint { get; set; }
        public TargetStatus Status { get; set; } = TargetStatus.live;
        public bool IsHunted { get; set; } = false;

    }
    public enum TargetStatus
    {
        live,
        dead
    }

}


