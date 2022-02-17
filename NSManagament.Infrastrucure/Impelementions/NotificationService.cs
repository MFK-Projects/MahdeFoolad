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

        public NotificationService(ILogger logger)
        {
            _logger = logger;
        }



        public Task SendNotification(CreationNotificationModel creationModel)
        {
            if (creationModel == null)
            {
                throw new ArgumentNullException($"{nameof(creationModel)} are send null to the method {nameof(SendNotification)}");
            }

            ToastCreator(creationModel);
            return Task.CompletedTask;
        }

        public Task SendNotification(List<CreationNotificationModel> creationListModel)
        {
            foreach (var toast in creationListModel)
            {
                ToastCreator(toast);
                Thread.Sleep(10000);
            }

            return Task.CompletedTask;
        }


        private void ToastCreator(CreationNotificationModel model)
        {
            if (model == null)
                throw new ArgumentNullException($"{typeof(NotificationCreationModel)} is null while passing it to Toast Creation Filter");


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

            toast = null;
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
