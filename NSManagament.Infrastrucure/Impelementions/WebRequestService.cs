using NSMangament.Application.Services;
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
        private readonly IUserMananger _userManager;

        public WebRequestService(IUserMananger userManager)
        {
            _userManager = userManager;
            _client = new WebClient();
        }
        public async Task<string> DownlaodStringData(string url)
        {
            if (string.IsNullOrEmpty(url))
                return await Task.FromResult(string.Empty);

            _client.Credentials = new NetworkCredential(
                _userManager.User.CredentialName,
                _userManager.User.Password,
                "KIAN"
                );
            _client.Headers.Add("OData-Version", "4.0");

            return await Task.FromResult(_client?.DownloadString(new Uri(url)));
        }

        public async Task<string> DownlaodStringData(string url, string password)
        {
            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(password))
                return await Task.FromResult(string.Empty);

            _client.Credentials = new NetworkCredential(
                _userManager.User.CredentialName,
                password,
                "KIAN"
                );
            _client.Headers.Add("OData-Version", "4.0");

            return await Task.FromResult(_client?.DownloadString(new Uri(url)));
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
