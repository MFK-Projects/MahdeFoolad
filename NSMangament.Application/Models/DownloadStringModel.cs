﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSMangament.Application.Models
{
    public class DownloadStringModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DomainName { get; set; }
        public string Url { get; set; }

    }

    public class UpdteTaskModel:DownloadStringModel
        
    {
        public TaskModel Data { get; set; }
    }


    public class UpdateStringModel : DownloadStringModel
    {
        public string Data { get; set; }
    }
}
