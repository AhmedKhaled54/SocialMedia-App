using Data.Entity;
using Data.Helper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RealTimeServices.MessageServices
{
    public  interface IPrivateMessageServices
    {
        Task<PrivateMessage> GetMessageById(int MessageId);
        Task<PrivateMessage> SendMessage(int senderid, int reciveid, string message);
        IQueryable<PrivateMessage> GetConversation(int senderid, int reciveid,string? Search=null);
        Task<bool> UpdateMessage (PrivateMessage message);
        Task<bool> RemoveMessage(PrivateMessage message);
        string GetConversationId (int senderid , int reciveid);
        Task<bool> MarkMessageAsRead(int senderid, int reciveid);
        Task<int> GetCountUnReadMessageGlobal(int receiveid);
        Task<int> GetCountUnReadMessagePerConvarsation(int senderid,int receiveid );
        IQueryable<ChatsDto> GetAllChats(int currentuser,string? Search);

    }
}
