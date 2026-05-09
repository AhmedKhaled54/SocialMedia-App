using AutoMapper;
using Core.Feature.Notifications.Query.Results;
using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapping.Notifications
{
    public  class NotificationProfile:Profile
    {
        public NotificationProfile()
        {
            CreateMap<Notification, GetNotificationQueryResult>()
               .ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => src.Sender.UserName))
               .ForMember(dest => dest.SenderImage, opt => opt.MapFrom(src => src.Sender.Image));
        }
    }
}
