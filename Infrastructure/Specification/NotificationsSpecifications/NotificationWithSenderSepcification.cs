using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Specification.NotificationsSpecifications
{
    public class NotificationWithSenderSepcification : BaseSepecification<Notification>
    {
        public NotificationWithSenderSepcification(int userid)
            : base(c=>c.RecipientId == userid)
        {

            AddInclude(c => c.Sender);
            AddOrderByDesc(c => c.CreatedAt);

        }
    }
}
