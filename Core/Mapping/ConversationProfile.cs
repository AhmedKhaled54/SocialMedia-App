using AutoMapper;
using Core.Feature.conversation.Command.Models;
using Core.Feature.conversation.Command.Results;
using Core.Feature.conversation.Query.Results;
using Data.Entity;
using Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapping
{
    public class ConversationProfile:Profile
    {
        public ConversationProfile()
        {
            
            CreateMap<PrivateMessage, DirectMessageCommandResult>()
               .ForMember(d => d.MessageId, c => c.MapFrom(s => s.Id))
               .ForMember(d => d.Date, c => c.MapFrom(s => s.CreatedAt))
               .ForMember(d => d.Body, c => c.MapFrom(s => s.Body))
               .ForMember(d => d.IsRead, c => c.MapFrom(s => s.IsRead));



            CreateMap<PrivateMessage, GetConvesationQueryResult>()
                .ForMember(d => d.MessageId, c => c.MapFrom(s => s.Id))
               .ForMember(d => d.Date, c => c.MapFrom(s => s.CreatedAt))
               .ForMember(d => d.Body, c => c.MapFrom(s => s.Body))
               .ForMember(d => d.IsRead, c => c.MapFrom(s => s.IsRead))
               .ForMember(d => d.SenderId, c => c.MapFrom(s => s.SendId))
               .ForMember(d => d.SenderName, c => c.MapFrom(s => s.Sender.UserName))
               .ForMember(d => d.ReceiveiId, c => c.MapFrom(s => s.RecivedId))
               .ForMember(d => d.ReceiveName, c => c.MapFrom(s => s.Recived.UserName));

            CreateMap<PrivateMessage, EditMessageCommand>()
                .ForMember(d => d.MessageId, c => c.MapFrom(s => s.Id))
                .ForMember(d => d.Body, c => c.MapFrom(s => s.Body))
                .ReverseMap();

            CreateMap<dynamic, GetAllCahtsQueryResult>();//get data is a object 
            CreateMap<ChatsDto, GetAllCahtsQueryResult>();//get data is a object 


        }
    }
}
