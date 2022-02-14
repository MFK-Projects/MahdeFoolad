using NSMangament.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace NSMangament.Application.Services
{
    public interface INotificationService:IDisposable
    {
        Task SendNotification(CreationNotificationModel creationModel);
        Task SendNotification(List<CreationNotificationModel> creationListModel);
    }
}
