using MahdeFooald.Common;
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
        private readonly WebClient _client;
        private bool _disposed;
        private static ILogger _logger;
        public WebRequestService(ILogger logger)
        {
            _logger = logger;
            _client = new WebClient();
            _logger.Information($"object type of  {typeof(WebRequestService)} is created  and Private Feild {typeof(WebClient)} is initialized");
        }

        public async Task<string> DownlaodStringData(DownloadStringModel downloadModel)
        {
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool dispose)
        {

            if (_disposed) return;

            if (dispose)
                _client?.Dispose();

            _disposed = true;

        }

    }
}
