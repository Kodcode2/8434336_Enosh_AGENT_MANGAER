namespace MissionsControl.ViewModels
{
    public class MissionFullDetailsVm
    {
        public int Id { get; set; }
        public int? AgentId { get; set; }
        public string? NickName { get; set; }
        public int AgentXWaypoint { get; set; }
        public int AgentYWaypoint { get; set; }
        public int? TargetId { get; set; }
        public string? Name { get; set; }
        public string? Position { get; set; }
        public int TargetXWaypoint { get; set; }
        public int TargetYWaypoint { get; set; }
        public double? TimeLeft { get; set; }
        public double? Distans { get; set; }
    }
}