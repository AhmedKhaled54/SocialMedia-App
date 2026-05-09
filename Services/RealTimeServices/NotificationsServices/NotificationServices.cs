using Data.Entity;
using Data.Enums.Notifacation;
using Infrastructure.Abstract;
using Infrastructure.Specification.NotificationsSpecifications;
using Microsoft.EntityFrameworkCore;
using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RealTimeServices.NotificationsServices
{
    public class NotificationServices : INotificationServices
    {
        private readonly IUnitOfWork _UOW;

        public NotificationServices(IUnitOfWork UOW)
        {
            _UOW = UOW;
        }
        public async Task CreateNotification(int Senderid, int RecipientId, NotifacationType type)
        {
            var Not = new Notification
            {
                CreatedAt = DateTime.UtcNow,
                SenderId = Senderid,
                RecipientId = RecipientId,
                Type = type,
                IsRead = false
            };
            await _UOW.Repository<Notification>().AddAsync(Not);
            await _UOW.Complete();
        }

        public Task<bool> DeleteNotification(Notification notification)
        {
            _UOW.Repository<Notification>().Delete(notification);
            return _UOW.Complete().ContinueWith(t => t.IsCompletedSuccessfully);
        }

        public async Task<Notification> GetNotificationById(int NotificationId)
            => await _UOW.Repository<Notification>().GetById(NotificationId);


        public IQueryable<Notification> GetUserNotifications(int UserId)
        {
            var sepcs =new NotificationWithSenderSepcification(UserId);
            var notifications = _UOW.Repository<Notification>().GetEntitiesWithSpecs(sepcs).AsNoTracking();
            return notifications;
        }

        public async Task<bool> MarkAllRead(int UserId)
        {
            var notification = await _UOW.Repository<Notification>()
                .GetAllpridicated(n => n.RecipientId == UserId && !n.IsRead)
                .ExecuteUpdateAsync(c => c.SetProperty(c => c.IsRead, true));
            return true;
        }

        public async Task<bool> MarkAsRead(int NotificationId, int Userid)
        {
            var notification = await _UOW.Repository<Notification>().GetById(NotificationId);
            if (notification == null || notification.RecipientId != Userid)
                throw new Exception("Notification not found or access denied.");
            notification.IsRead = true;
            _UOW.Repository<Notification>().Update(notification);
            await _UOW.Complete();
            return true;
        }

        public async Task<int> UnReadCount(int UserId)
        {
            var notications = await _UOW.Repository<Notification>()
                .GetAllpridicated(n => n.RecipientId == UserId && !n.IsRead).CountAsync();
            return notications;
        }
    }
}
