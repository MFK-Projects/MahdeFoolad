using NSMangament.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSMangament.Application.Services
{
    public interface IWebRequest : IDisposable
    {
        Task<string> DownlaodStringData(DownloadStringModel downlaodModel);
    }
}
