using NSMangament.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSMangament.Application.Services
{
    public interface IUserMananger
    {
        UserModel User { get; set; }
    }
}
