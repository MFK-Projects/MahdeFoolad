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
        public WebRequestService(IUserMananger userManager)
        {
            _client = new WebClient();
        }


        public Task<bool> SendHttpRequest(string url)
        {
            if (string.IsNullOrEmpty(url))
                return Task.FromResult(false);

            
        }

        public Task<bool> SendHttpRequest(string , string password)
        {
            throw new NotImplementedException();
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
