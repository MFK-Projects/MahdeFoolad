using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSMangament.Application.Models;
using Serilog;
using System.IO;
using Newtonsoft.Json;
using NSMangament.Application.Services;
using MahdeFooald.Common;

namespace NSManagament.Infrastrucure.Impelementions
{
    public class UtilityService : IUtilityService
    {
        private static readonly string _settitngFilePath = AppDomain.CurrentDomain.BaseDirectory + @"\appSetting.json";
        private readonly ILogger _logger;
        private bool? _isSet;
        private readonly IWebRequest _webRequest;
        private readonly IUserMananger _userManager;
        public bool? IsSet
        {
            get => IsSet;
        }
        public UtilityService(IWebRequest webRequest, IUserMananger userMananger)
        {
            _webRequest = webRequest;
            _userManager = userMananger;
        }

        public void SetApplicationSetting(ApplicationSettingModel model)
        {
            try
            {
                if (model == null)
                {
                    _logger.Error($"the model passed to the {nameof(SetApplicationSetting)} method typeof :{typeof(void)} is null model name :{nameof(model)} type {typeof(ApplicationSettingModel)}");
                    _isSet = false;
                    return;
                }


                if (!File.Exists(_settitngFilePath))
                    File.Create(_settitngFilePath)?.Dispose();

                string _jsondata = JsonConvert.SerializeObject(model);

                File.WriteAllText(_settitngFilePath, _jsondata);
                _isSet = true;
            }
            catch
            {
                _logger.Error("faild to create the setting File for the Application");
                _isSet = false;
            }

        }


        public async Task ResetData()
        {
            var _jsondata = await _webRequest.DownlaodStringData(new DownloadStringModel
            {
                DomainName = RequestUrl.DomainName,
                Password = _userManager.User.Password,
                Url = RequestUrl.BuildUrl(UrlBuilderMode.SingleUser, _userManager.User.UserName, null, null),
                UserName = _userManager.User.CredentialName
            });

            var userdata = JsonConvert.DeserializeObject<RootListModel<UserModel>>(_jsondata);



        }

        public async Task<List<TaskModel>> RetriveData()
        {
            var _jsondata = await _webRequest.DownlaodStringData(new DownloadStringModel
            {
                DomainName = RequestUrl.DomainName,
                Password = _userManager.User.Password,
                UserName = _userManager.User.CredentialName,
                Url = RequestUrl.BuildUrl(UrlBuilderMode.MultipuleTasks, null, null, _userManager.User.UserId)
            });

            var tasksdata = JsonConvert.DeserializeObject<RootListModel<TaskModel>>(_jsondata);


            if (tasksdata.Entities.Count > 0)
                return await Task.FromResult(tasksdata.Entities);
            else
                return await Task.FromResult<List<TaskModel>>(null);
        }
    }
}
