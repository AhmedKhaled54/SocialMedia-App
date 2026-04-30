using Data.Enums.Notifacation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RealTimeServices.NotificationsServices
{
    public  interface INotificationServices
    {
        Task CreateNotification(int Senderid, int RecipientId, NotifacationType type);
        
    }
}
