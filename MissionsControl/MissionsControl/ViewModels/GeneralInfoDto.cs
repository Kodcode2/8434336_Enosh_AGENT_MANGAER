namespace MissionsControl.ViewModels
{
    public class GeneralInfoDto
    {
        public int AmountAgents { get; set; }
        public int AmountAgentsActivity { get; set; }
        public int AmountTargets { get; set; }
        public int AmountTargetsKild { get; set; }
        public int AmountMissions { get; set; }
        public int AmountMissionsActivity { get; set; }
        public double? RelationAgentsTargets { get; set; }
        public double RelationAgentsTargetsTeamable { get; set; }

    }
}