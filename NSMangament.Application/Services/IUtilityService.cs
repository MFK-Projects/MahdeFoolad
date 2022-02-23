using NSMangament.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSMangament.Application.Services
{
    public interface IUtilityService
    {
        void SetApplicationSetting(ApplicationSettingModel model);
        Task<List<TaskModel>> RetriveData();
    }
}
