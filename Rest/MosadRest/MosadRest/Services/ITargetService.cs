using MosadRest.DtoModels;
using MosadRest.Models;

namespace MosadRest.Services
{
    public interface ITargetService
    {
        public Task<ResIdDto?> CreateTarget(TargetDto targetDto);
        public Task<List<TargetModel>?> GetAllTargets();
        public Task<bool> PinTarget(int id, locationDto location);
        public Task<bool> MoveTarget(int id, DirectionDto directionDto);
    }
}
