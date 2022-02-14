using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSMangament.Application.Services
{
    public interface IWebRequest : IDisposable
    {
        Task<string> DownlaodStringData(string url);
        Task<string> DownlaodStringData(string url, string password);
    }
}
