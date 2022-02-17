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

namespace NSManagament.Infrastrucure.Impelementions
{
    public class UtilityService : IUtilityService
    {
        private static readonly string _settitngFilePath = AppDomain.CurrentDomain.BaseDirectory + @"\appSetting.json";
        private readonly ILogger _logger;
        private bool? _isSet;

        public bool? IsSet
        {
            get => IsSet;
        }
        public UtilityService(ILogger logger)
        {
            _logger = logger;
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

        private string UrlBuilder(UrlBuilderModel model) { return string.Empty; }
        private string TaskUrlBuilder(UrlBuilderModel model) { return string.Empty; }
    }
}
