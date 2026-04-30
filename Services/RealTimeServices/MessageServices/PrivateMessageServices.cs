using Data.Entity;
using Data.Helper;
using Data.Identity;
using Infrastructure.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.RealTimeServices.MessageServices
{
    public class PrivateMessageServices : IPrivateMessageServices
    {
        private readonly IUnitOfWork _UOW;
        public PrivateMessageServices(IUnitOfWork uOW)
        {
            _UOW = uOW;
        }
        public async Task<PrivateMessage> GetMessageById(int MessageId)
            => await _UOW.Repository<PrivateMessage>().GetById(MessageId);
        public async Task<PrivateMessage> SendMessage(int senderid, int reciveid, string message)
        {
            var trans =await _UOW.BeginTransactionAsync();
            try
            {
                var recived = await _UOW.Repository<User>().FindAsync(c => c.Id == reciveid);
                if (recived == null)
                    return null;

                var privatemessage = new PrivateMessage
                {
                    SendId = senderid,
                    RecivedId = reciveid,
                    Body = message,
                    IsRead = false,
                    CreatedAt = DateTime.UtcNow
                };

                await _UOW.Repository<PrivateMessage>().AddAsync(privatemessage);
                await _UOW.Complete();
                await trans.CommitAsync();
                return privatemessage;

            }
            catch (Exception ex )
            {
                await trans.RollbackAsync();
                throw new Exception(ex.Message);
            }
           

        }

        public IQueryable<PrivateMessage> GetConversation(int senderid, int reciveid,string?Search=null)
        {
            //var chat = _UOW.Repository<PrivateMessage>()
            //    .GetTableAsNoTracking()
            //    .Where(c => c.SendId == senderid && c.RecivedId == reciveid ||
            //          c.RecivedId == senderid && c.SendId == reciveid)
            //    .OrderBy(c => c.CreatedAt).AsQueryable();

            var chat = _UOW.Repository<PrivateMessage>().GetAllpridicated
                (c => c.SendId == senderid && c.RecivedId == reciveid ||
                c.RecivedId == senderid && c.SendId == reciveid, new[] { "Sender","Recived" })
                .OrderBy(c => c.CreatedAt).AsQueryable();
            if (!string.IsNullOrEmpty(Search))
            {
                chat = chat.Where(c => EF.Functions.Like(c.Body,$"%{Search.Trim()}%"));
            }

            return chat;
        }
        public async Task<bool> UpdateMessage(PrivateMessage message)
        {

            _UOW.Repository<PrivateMessage>().Update(message);
            await _UOW.Complete();
            return true;

        }
       

        public async Task<bool> RemoveMessage(PrivateMessage message)
        {
           _UOW.Repository<PrivateMessage>().Delete(message);
            await _UOW.Complete();
            return true;
        }

        public string GetConversationId(int senderid, int reciveid)
            => senderid < reciveid ? $"{senderid}_{reciveid}" : $"{reciveid}_{senderid}";

        public async  Task<bool> MarkMessageAsRead(int senderid, int reciveid)
        {
            var query =_UOW.Repository<PrivateMessage>()
                .GetTable(c=>c.
                SendId == senderid &&
                c.RecivedId == reciveid && 
                !c.IsRead)
                .ExecuteUpdateAsync(s => s.SetProperty(m => m.IsRead, true));//sql one query 
            //await _UOW.Complete();
            return true;
        }

        public async Task<int> GetCountUnReadMessageGlobal(int receiveid)//all message :To Do
        {
            var resutlt = await _UOW.Repository<PrivateMessage>()
                .GetAllpridicated(c =>c.RecivedId==receiveid&&!c.IsRead).CountAsync();
            return resutlt;
        }

        public async Task<int> GetCountUnReadMessagePerConvarsation(int senderid, int receiveid)//From User
        {
            //var result = await _UOW.Repository<PrivateMessage>()
            //    .GetAllpridicated(c => (
            //    c.SendId == senderid && c.RecivedId == receiveid) //receiveid cuurentuser
            //    && !c.IsRead)
            //    .CountAsync();
            var result =await  _UOW.Repository<PrivateMessage>()
                .GetTable(c => c.SendId == senderid && c.RecivedId == receiveid && !c.IsRead).CountAsync();
            return result;

        }

        public  IQueryable<ChatsDto> GetAllChats(int currentuser, string? Search)
        {
            var query = _UOW.Repository<PrivateMessage>()//id ,name ,lastmessage,lastmessagedate/unreadcount /=>using temp dto
                .GetAllpridicated(c => c.SendId == currentuser || c.RecivedId == currentuser)
                .Select(m => new
                {
                    Message = m,
                    otheruser = m.SendId == currentuser ? m.RecivedId : m.SendId
                })
                .Join(_UOW.Context.Users,
                c => c.otheruser,
                x => x.Id,
                (c, x) => new
                {
                    c.Message,
                    user = x
                });
            if (!string.IsNullOrEmpty(Search))
                query=query.Where(c => EF.Functions.Like(c.user.UserName, $"%{Search.Trim()}%"));

            var result = query.
                GroupBy(c => new { c.user.Id, c.user.UserName })
                .Select(c => new ChatsDto 
                {
                    UserId = c.Key.Id,
                    UserName = c.Key.UserName,
                    LastMessage = c
                .OrderByDescending(x => x.Message.CreatedAt)
                .Select(x => x.Message.Body)
                .FirstOrDefault(),
                    LastMessageDate = c.Max(c => c.Message.CreatedAt),
                    UnReadMessage = c.Count(c => c.Message.RecivedId == currentuser && !c.Message.IsRead)
                }).OrderByDescending(c => c.LastMessageDate);

            return result;
        }

     
    }
}
