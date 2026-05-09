using Data.Entity;
using Data.Enums.Notifacation;
using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RealTimeServices.NotificationsServices
{
    public interface INotificationServices
    {
        Task CreateNotification(int Senderid, int RecipientId, NotifacationType type);
        IQueryable<Notification> GetUserNotifications(int UserId);
        Task<bool> MarkAsRead(int NotificationId, int UserId);
        Task<bool>MarkAllRead(int UserId);
        Task<int> UnReadCount (int UserId);
        Task<bool> DeleteNotification(Notification notification);
        Task<Notification> GetNotificationById(int NotificationId);


    }
}
