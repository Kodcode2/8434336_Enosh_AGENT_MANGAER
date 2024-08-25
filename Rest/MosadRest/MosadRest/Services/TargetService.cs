using Microsoft.EntityFrameworkCore;
using MosadRest.Data;
using MosadRest.DtoModels;
using MosadRest.Models;
using MosadRest.Utils;
using System.Reflection;


namespace MosadRest.Services
{
    public class TargetService(ApplicationDbContext _DbContext) : ITargetService
    {
        public async Task<ResIdDto?> CreateTargetAsync(TargetDto targetDto)
        {
            TargetModel newTarget = new()
            {
                Name = targetDto.name,
                Position = targetDto.position,
                PhotoUrl = targetDto.photoUrl
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
            var a = await _DbContext.Targets.ToListAsync() ?? new();

            return await _DbContext.Targets.ToListAsync() ?? new();
        }
        public async Task PinTargetAsync(int id, locationDto location)
        {
            TargetModel? target = await _DbContext.Targets.FindAsync(id);
            if (target == null)
                throw new Exception("Not Found");
            if (target.Status == TargetStatus.dead)
                throw new Exception("Is Ded");
            try
            {
                target.XWaypoint = location.x;
                target.YWaypoint = location.y;
                if(!AgentTargetUtils.IsLocationValid(target.XWaypoint, target.YWaypoint))
                    throw new Exception("InValid Location");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            await _DbContext.SaveChangesAsync();
        }
    
        public async Task MoveTargetAsync(int id, DirectionDto directionDto)
        {
            var numDirection = directionDto.NumDirection[directionDto.direction];
            TargetModel? target = await _DbContext.Targets.FindAsync(id);
            if (target == null)
                throw new Exception("Not Found");
            //if (target.Status != TargetModel.TargetStatus.live)
            //throw new Exception("Target is ded");
            try
            {
                AgentTargetUtils.MuveTargetStep(target, numDirection.Value.x, numDirection.Value.y);
                await _DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TargetModel> GetTargetById(int id)
        {
            return await _DbContext.Targets.FindAsync(id);
        }
    }
}

