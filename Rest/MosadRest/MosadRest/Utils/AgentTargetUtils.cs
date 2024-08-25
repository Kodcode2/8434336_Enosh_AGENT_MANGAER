using Microsoft.EntityFrameworkCore;
using MosadRest.DtoModels;
using MosadRest.Models;

namespace MosadRest.Utils
{
    public static class AgentTargetUtils
    {
        public static bool IsLocationValid(int x, int y) =>
    x >= 0 && x < 1000 && y >= 0 && y < 1000;

        public static void MuveAgentStep(AgentModel agent, int xToMove, int yToMove)
        {
            agent.XWaypoint += xToMove;
            agent.YWaypoint += yToMove;
            if (!IsLocationValid(agent.XWaypoint, agent.YWaypoint))
                throw new Exception("InValid Location");
        }
        public static void MuveTargetStep(TargetModel target, int xToMove, int yToMove)
        {
            target.XWaypoint += xToMove;
            target.YWaypoint += yToMove;
            if (!IsLocationValid(target.XWaypoint, target.YWaypoint))
                throw new Exception("InValid Location");
        }
    }
}
