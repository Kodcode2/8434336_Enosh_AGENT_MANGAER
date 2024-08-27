using MissionsControl.DtoModels;
using MissionsControl.Models;
using MissionsControl.ViewModels;

namespace MissionsControl.Services
{
    public interface IMissionsService
    {
        public Task<List<MissionDto>?> GetAllMissionsAsync();
        public Task<MissionFullDetailsVm> GetMissionDetailsById(int id);
        public Task<List<MissionFullDetailsDto>> GetAllMissionsFullAsync();




    }
}
