using MosadRest.DtoModels;
using MosadRest.Models;

namespace MosadRest.Services
{
    public interface ITargetService
    {
        public Task<ResIdDto?> CreateTargetAsync(TargetDto targetDto);
        public Task<List<TargetModel>?> GetAllTargets();
        public Task PinTargetAsync(int id, locationDto location);
        public Task MoveTargetAsync(int id, DirectionDto directionDto);
        public Task<TargetModel> GetTargetById(int id);
        
    }
}
