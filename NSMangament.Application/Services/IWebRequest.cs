using NSMangament.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSMangament.Application.Services
{
    public interface IWebRequest 
    {
        Task<string> DownlaodStringData(DownloadStringModel downlaodModel);
        Task<string> UploadStringData(UpdteTaskModel uploadModel);
        Task<string> UploadStringData(UpdateStringModel uploadModel);
    }
}
