#region using directives

using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using SoftMarketing.Model;
using MySql.Data.MySqlClient;
using Dapper;
using SoftMarketing.DAL.MySQL.Helper;
using SoftMarketing.DAL.UnitOfWork;
#endregion

namespace SoftMarketing.DAL.DataAccess.MarketingDAL
{

    public class MessageDAL : DataAccessBase
    {
        public List<SchedulMessage> GetAllScheduledMessages(int userId)
        {
            return Connection.Query<SchedulMessage>("marketing_user_message_scheduled_getall", new { userId= userId},commandType:CommandType.StoredProcedure).ToList();
        }
        public List<TodayScheduledMessages> GetTodayScheduledMessages(int userId)
        {
            return Connection.Query<TodayScheduledMessages>("get_sch_msg_by_date", new { userId= userId},commandType:CommandType.StoredProcedure).ToList();
        }
        public List<User_Template> GetAllTemplates(int userId)
        {
            return Connection.Query<User_Template>("marketing_user_template_getall", new { userId= userId},commandType:CommandType.StoredProcedure).ToList();
        }
        public SchedulMessage InsertUserMessage(SchedulMessage message)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@salesuserid", message.sales_userId);
            parameter.Add("@marketingusertemplateid", message.usertemplate_id);
            parameter.Add("@scheduledtime", message.scheduled_time);
            parameter.Add("@customerid", message.marketing_user_customerId);
            var result = Connection.ExecuteScalar("marketing_user_message_insert", parameter, commandType: CommandType.StoredProcedure);
            message.id = Convert.ToInt64(result);
            return message;
        }
        public int UpdateUserMessage(SchedulMessage message)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@usermessage_id", message.id);
            parameter.Add("@salesuserid", message.sales_userId);
            parameter.Add("@marketingusertemplateid", message.usertemplate_id);
            parameter.Add("@scheduledtime", message.scheduled_time);
            parameter.Add("@customerid", message.marketing_user_customerId);
            var result = Connection.Execute("marketing_user_message_update", parameter, commandType: CommandType.StoredProcedure);
            return result;
        }
        public int DeleteUserMessage(long usermessage_id, int userId)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@salesuserid", userId);
            parameter.Add("@usermessage_id", usermessage_id);
            var result = Connection.Execute("marketing_user_message_delete", parameter, commandType: CommandType.StoredProcedure);
            return result;
        }
        public int UpdateSentFlag(string messageIds, int userId, int? sent)
        {
            var sqlCommand = "UPDATE marketing_user_message SET sent =1 where id in (" + messageIds+");";
            //var sqlCommand = "DELETE FROM common_refresh_token where userId = @value";
            return Connection.Execute(sqlCommand);
        }
        public string GetReminders(int usermessage_id, int userId)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@usermessage_id", usermessage_id);
            parameter.Add("@usermessage_id", usermessage_id);
            var result = Connection.Execute("marketing_user_customer_send_reminder", parameter, commandType: CommandType.StoredProcedure).ToString();
            return result;
        }
        public List<Templates> Get(int templateId)
        {
            return Connection.Query<Templates>("marketing_user_template_getsingle", new { usertemplate_id = templateId }, commandType: CommandType.StoredProcedure).ToList();
        }
    }
}