using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using SoftMarketing.Model;
using SoftMarketing.DAL.DataAccess.MarketingDAL;
using SoftMarketing.DAL.UnitOfWork;

namespace SoftMarketing.Services.Marketing
{

    public class MessageService
    {
        MessageDAL MessageDAL { get; set; }
        public MessageService()
        {
            MessageDAL = new MessageDAL();
        }

        public List<SchedulMessage> GetAllScheduledMessages(int userId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                MessageDAL MessageDAL = (MessageDAL)unitOfWork.Repository<MessageDAL>();
                return MessageDAL.GetAllScheduledMessages(userId);
            }
        }
        public List<TodayScheduledMessages> GetTodayScheduledMessages(int userId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                MessageDAL MessageDAL = (MessageDAL)unitOfWork.Repository<MessageDAL>();
                return MessageDAL.GetTodayScheduledMessages(userId);
            }
        }
        public List<User_Template> GetAllTemplates(int userId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                MessageDAL MessageDAL = (MessageDAL)unitOfWork.Repository<MessageDAL>();
                return MessageDAL.GetAllTemplates(userId);
            }
        }
        public int UpdateUserMessage(SchedulMessage message)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                MessageDAL MessageDAL = (MessageDAL)unitOfWork.Repository<MessageDAL>();
                return MessageDAL.UpdateUserMessage(message);
            }
        }
        public SchedulMessage InsertUserMessage(SchedulMessage message)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                MessageDAL MessageDAL = (MessageDAL)unitOfWork.Repository<MessageDAL>();
                return MessageDAL.InsertUserMessage(message);
            }
        }
        public int DeleteUserMessage(long userMessageId, int userId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                MessageDAL MessageDAL = (MessageDAL)unitOfWork.Repository<MessageDAL>();
                return MessageDAL.DeleteUserMessage(userMessageId, userId);
            }
        }
        public int UpdateSentFlag(string msgIds, int userId, int? sent)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                MessageDAL MessageDAL = (MessageDAL)unitOfWork.Repository<MessageDAL>();
                return MessageDAL.UpdateSentFlag(msgIds, userId, sent);
            }
        }
    }
}