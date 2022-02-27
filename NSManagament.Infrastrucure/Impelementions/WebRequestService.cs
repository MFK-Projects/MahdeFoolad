using MahdeFooald.Common;
using Newtonsoft.Json;
using NSMangament.Application.Models;
using NSMangament.Application.Services;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NSManagament.Infrastrucure.Impelementions
{
    public class WebRequestService : IWebRequest
    {
        private WebClient _client;
        private static ILogger _logger;
        public WebRequestService(ILogger logger)
        {
            _logger = logger;
        }
        public async Task<string> DownlaodStringData(DownloadStringModel downloadModel)
        {
            try
            {
                _client = new WebClient();

                if (downloadModel == null)
                {
                    _logger.Error($"the argument which is passed to the method is null \n method name :{ nameof(DownlaodStringData)} \t methodType :{typeof(Task<string>)}");
                    throw new Exception(ExceptionMessages.NUllArgumentException(typeof(DownloadStringModel), nameof(downloadModel)));
                }
                else if (string.IsNullOrEmpty(downloadModel.UserName) || string.IsNullOrEmpty(downloadModel.Password) || string.IsNullOrEmpty(downloadModel.Url) || string.IsNullOrEmpty(downloadModel.DomainName))
                {
                    _logger.Error($"one or more items passed to the method {nameof(DownlaodStringData)} are null and dont have the value paramtertype :{typeof(DownloadStringModel)} \n paramtername {nameof(downloadModel)}");
                    throw new Exception($"one or more items passed to the method {nameof(DownlaodStringData)} are null and dont have the value paramtertype :{typeof(DownloadStringModel)} \n paramtername {nameof(downloadModel)}");
                }

                _client.Credentials = new NetworkCredential(
                    downloadModel.UserName,
                    downloadModel.Password,
                    downloadModel.DomainName
                    );

                _client.Headers.Add("OData-Version", "4.0");

                return await Task.FromResult(_client?.DownloadString(new Uri(downloadModel.Url)));
            }
            catch(Exception ex)
            {
                _logger.Error($"Error ouccer while DownlaodStringData Excute :{ex.Message}");
                throw new Exception("failed to excute the downloadStringData");
                
            }
            finally
            {
                _client?.Dispose();
            }
        }

        public async Task<string> UploadStringData(UpdteTaskModel uploadModel)
        {
            try
            {
                _client = new WebClient();

                if (uploadModel == null)
                {
                    _logger.Error($"the argument which is passed to the method is null \n method name :{ nameof(DownlaodStringData)} \t methodType :{typeof(Task<string>)}");
                    throw new Exception(ExceptionMessages.NUllArgumentException(typeof(DownloadStringModel), nameof(uploadModel)));
                }
                else if (string.IsNullOrEmpty(uploadModel.UserName) || string.IsNullOrEmpty(uploadModel.Password) || string.IsNullOrEmpty(uploadModel.Url) || string.IsNullOrEmpty(uploadModel.DomainName))
                {
                    _logger.Error($"one or more items passed to the method {nameof(DownlaodStringData)} are null and dont have the value paramtertype :{typeof(DownloadStringModel)} \n paramtername {nameof(uploadModel)}");
                    throw new Exception($"one or more items passed to the method {nameof(DownlaodStringData)} are null and dont have the value paramtertype :{typeof(DownloadStringModel)} \n paramtername {nameof(uploadModel)}");
                }

                _client.Credentials = new NetworkCredential(
                    uploadModel.UserName,
                    uploadModel.Password,
                    uploadModel.DomainName
                    );


                _client.Headers.Add("OData-Version", "4.0");
                _client.Headers.Add("Accept", "application/json");

                var _jsondata = JsonConvert.SerializeObject(uploadModel.Data);
                return await Task.FromResult(_client?.UploadString(new Uri(uploadModel.Url), "PATCH",_jsondata));
            }
            catch (Exception ex)
            {
                _logger.Error($"Error ouccer while DownlaodStringData Excute :{ex.Message}");
                throw new Exception("failed to excute the downloadStringData");

            }
            finally
            {
                _client?.Dispose();
            }
        }

        public async Task<string> UploadStringData(UpdateStringModel uploadModel)
        {
            try
            {
                _client = new WebClient();

                if (uploadModel == null)
                {
                    _logger.Error($"the argument which is passed to the method is null \n method name :{ nameof(DownlaodStringData)} \t methodType :{typeof(Task<string>)}");
                    throw new Exception(ExceptionMessages.NUllArgumentException(typeof(DownloadStringModel), nameof(uploadModel)));
                }
                else if (string.IsNullOrEmpty(uploadModel.UserName) || string.IsNullOrEmpty(uploadModel.Password) || string.IsNullOrEmpty(uploadModel.Url) || string.IsNullOrEmpty(uploadModel.DomainName))
                {
                    _logger.Error($"one or more items passed to the method {nameof(DownlaodStringData)} are null and dont have the value paramtertype :{typeof(DownloadStringModel)} \n paramtername {nameof(uploadModel)}");
                    throw new Exception($"one or more items passed to the method {nameof(DownlaodStringData)} are null and dont have the value paramtertype :{typeof(DownloadStringModel)} \n paramtername {nameof(uploadModel)}");
                }

                _client.Credentials = new NetworkCredential(
                    uploadModel.UserName,
                    uploadModel.Password,
                    uploadModel.DomainName
                    );


                _client.Headers.Add("OData-Version", "4.0");
                _client.Headers.Add("Accept", "application/json");
                _client.Headers.Add("Content-Type", "application/json; charset=utf-8");

                var _jsondata = "{" + "'new_task_status' :" + uploadModel.Data + "}";
                var result = _client?.UploadString(new Uri(uploadModel.Url), "PATCH", _jsondata);

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"Error ouccer while DownlaodStringData Excute :{ex.Message}");
                throw new Exception("failed to excute the downloadStringData");

            }
            finally
            {
                _client?.Dispose();
            }
        }
    }
}
