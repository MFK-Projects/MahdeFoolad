using Microsoft.Toolkit.Uwp.Notifications;
using NSMangament.Application.Models;
using NSMangament.Application.Services;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Serilog;
using System.Threading;

namespace NSManagament.Infrastrucure.Impelementions
{
    public class NotificationService : INotificationService
    {

        private bool _disposed = false;
        private readonly ILogger _logger;
        private static int _notificationsendCount;
        private static readonly object _locker = new object();
        public NotificationService(ILogger logger)
        {
            _logger = logger;
        }



        public Task SendNotification(CreationNotificationModel creationModel)
        {
            if (creationModel == null)
            {
                _logger.Error($"the argument passed to the method typeof {typeof(void)} name : {nameof(SendNotification)} is null argument name {nameof(creationModel)} argumenttype :{typeof(CreationNotificationModel)}");
                return Task.CompletedTask;
            }

            lock(_locker)
            {
                ToastCreator(creationModel);
                _notificationsendCount++;
            }

            return Task.CompletedTask;
        }

        public Task SendNotification(List<CreationNotificationModel> creationListModel)
        {
            if (creationListModel == null)
            {
                _logger.Error($"the argument passed to the method typeof {typeof(void)} name : {nameof(SendNotification)} is null argument name {nameof(creationListModel)} argumenttype :{typeof(CreationNotificationModel)}");

                return Task.CompletedTask;
            }

            foreach (var toast in creationListModel)
            {
                lock (_locker)
                {
                    ToastCreator(toast);
                    _notificationsendCount++;
                }

                Thread.Sleep(10000);
            }

            return Task.CompletedTask;
        }


        private void ToastCreator(CreationNotificationModel model)
        {
            var toast = new ToastContentBuilder();


            if (!string.IsNullOrEmpty(model.Titel))
                toast.AddText(model.Titel);

            if (model.Text.Length > 0)
                foreach (var item in model.Text)
                    toast.AddText(item);


            if (!string.IsNullOrEmpty(model.NotificationArgument))
            {
                toast.SetBackgroundActivation();
                toast.AddArgument(model.NotificationArgument);
            }

            toast.SetToastScenario(model.ToastScenario);
            toast.SetToastDuration(ToastDuration.Long);


            if (!string.IsNullOrEmpty(model.TaskUrl))
                toast.SetProtocolActivation(new System.Uri(model.TaskUrl));
            toast.Show();
        }



        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool dispose)
        {
            if (_disposed) return;
            _disposed = true;
        }
    }
}
