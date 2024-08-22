using Microsoft.EntityFrameworkCore;
using MosadRest.Data;
using MosadRest.DtoModels;
using MosadRest.Models;
using System.Reflection;


namespace MosadRest.Services
{
    public class TargetService(ApplicationDbContext _DbContext) : ITargetService
    {
        public async Task<ResIdDto?> CreateTarget(TargetDto targetDto)
        {
            TargetModel newTarget = new()
            {
                Name = targetDto.name,
                Position = targetDto.position,
                PhotoUrl = targetDto.photo_url
            };
            await _DbContext.AddAsync(newTarget);
            await _DbContext.SaveChangesAsync();
            return (new()
            {
                Id = newTarget.Id,
            });
        }
        public async Task<List<TargetModel>> GetAllTargets()
        {
            return _DbContext.Targets.ToList() ?? new();
        }
        public async Task<bool> PinTarget(int id, locationDto location)
        {
            TargetModel? target = await _DbContext.Targets.FindAsync(id);
            if (target == null)
                throw new Exception("Not Found");
            if (target.Status == TargetModel.TargetStatus.dead)
                return false;
            if (target.XWaypoint == 201 || target.YWaypoint == 201)
            {
                target.XWaypoint = location.x;
                target.YWaypoint = location.y;
                await _DbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> MoveTarget(int id, DirectionDto directionDto)
        {
            var numDirection = directionDto.NumDirection[directionDto.direction];
            TargetModel? target = await _DbContext.Targets.FindAsync(id);
            if (target == null)
                throw new Exception("Not Found");
            if (target.Status != TargetModel.TargetStatus.live)
                throw new Exception("Target is ded");
            if (target.XWaypoint == -201 || target.YWaypoint == -201)
                throw new Exception("Not Pozition yet");
            target.XWaypoint += numDirection.Value.x;
            target.YWaypoint += numDirection.Value.y;
            if (IsLocationValid(target.XWaypoint, target.YWaypoint))
            {
                await _DbContext.SaveChangesAsync();
                return true;
            }
            throw new Exception("InValidLocation");
        }
        public async Task<List<MissionModel>?> GetOffersBiAgentId(int id) =>
                await _DbContext.Missions.Where(x => x.AgentId == id)
               .Where(x => x.MissionStatus == MissionStatus.offer).ToListAsync();


        public bool IsLocationValid(int x, int y) =>
            x >= 0 && x < 1000 && y >= 0 && y < 1000;



    }
}


/*
        public bool IsInKillZone(AgentModel agent, TargetModel target)
        {
            return Math.Sqrt(Math.Pow(agent.XWaypoint - target.XWaypoint, 2)
                + Math.Pow(agent.YWaypoint - target.YWaypoint, 2)) <= 200;

        }



*/
