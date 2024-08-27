using MissionsControl.ViewModels;

namespace MissionsControl.Services
{
    public interface IControlService
    {
        public Task<GeneralInfoDto> GetGeneralInfo();
    }
}
