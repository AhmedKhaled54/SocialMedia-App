using Data.Entity;
using Data.Enums.Notifacation;
using Infrastructure.Abstract;
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

        public NotificationServices(IUnitOfWork UOW )
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
            await _UOW.Repository<Notification>().AddAsync( Not );
            await _UOW.Complete();
        }
    }
}
