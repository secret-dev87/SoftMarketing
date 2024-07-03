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
using Newtonsoft.Json;
using SoftMarketing.Model.SalesModels;
using Dapper.Contrib.Extensions;
using SoftMarketing.Model.MarketingModels;


#endregion

namespace SoftMarketing.DAL.DataAccess.MarketingDAL
{

    public class UserSettingsDAL : DataAccessBase
    {
        #region Reminder Settings

        public UserSettings GetUserSettings(int userId)
        {
            return Connection.Query<UserSettings>("get_user_settings", new { userid = userId }, commandType: CommandType.StoredProcedure).First();
        }
        public List<UserCategory> GetCategoriesWithTemplates(int userId, int? categoryDetailId)
        {
            return Connection.Query<UserCategory>("user_categories_with_templates", new { sales_userId = userId, category_detailId = categoryDetailId }, commandType: CommandType.StoredProcedure).ToList();
        }
        public string InsertUserCategory(int userId, int categoryDetailId)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@p_sales_userId", userId);
            parameter.Add("@p_listing_category_detailId", categoryDetailId);
            var result = Connection.ExecuteScalar("listing_user_category_insert", parameter, commandType: CommandType.StoredProcedure)?.ToString();
            return result;
        }
        public string InsertAdvertisementTemplate(int userId, int templateDetailId)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@p_sales_userId", userId);
            parameter.Add("@p_marketing_template_detailId", templateDetailId);
            var result = Connection.ExecuteScalar("marketing_user_advertise_template_insert", parameter, commandType: CommandType.StoredProcedure)?.ToString();
            return result;
        }
        public string DeleteUserCategory(int userId, int categoryDetailId)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@p_sales_userId", userId);
            parameter.Add("@p_listing_category_detailId", categoryDetailId);
            var result = Connection.ExecuteScalar("listing_user_category_delete", parameter, commandType: CommandType.StoredProcedure)?.ToString();
            return result;
        }
        public int UpdateDeleteCustomerFlag(int userId, int deleteCustomerFlag)
        {
            var sqlCommand = "UPDATE sales_user SET delete_customer = @dcf where id = @id";
            return Connection.Execute(sqlCommand, new { dcf = deleteCustomerFlag, id = userId });
        }

        public int UpdateUserSettings(int userId, UserSettings userSettings)
        {
            var sqlCommand = "UPDATE sales_user SET send_reminders = @send_reminders,delete_customer =@delete_customer,reminder_duration=@reminder_duration,reminder_times=@reminder_times,send_events=@send_events,send_advertisement=@send_advertisement,send_feedback=@send_feedback,send_birthday=@send_birthday where id = @id";
            return Connection.Execute(sqlCommand,
                new
                {
                    send_reminders = userSettings.send_reminders,
                    delete_customer = userSettings.delete_customer,
                    reminder_duration = userSettings.reminder_duration,
                    reminder_times = userSettings.reminder_times,
                    send_events = userSettings.send_events,
                    send_advertisement = userSettings.send_advertisement,
                    send_feedback = userSettings.send_feedback,
                    send_birthday = userSettings.send_birthday,
                    id = userId
                });
        }
        public int UpdateReminderSettings(int userId, UserSettings userSettings)
        {
            var sqlCommand = "UPDATE sales_user SET send_reminders = @send_reminders,delete_customer =@delete_customer,reminder_duration=@reminder_duration,reminder_times=@reminder_times where id = @id";
            return Connection.Execute(sqlCommand,
                new
                {
                    send_reminders = userSettings.send_reminders,
                    delete_customer = userSettings.delete_customer,
                    reminder_duration = userSettings.reminder_duration,
                    reminder_times = userSettings.reminder_times,
                    id = userId
                });
        }

        public int UpdateEventsSetting(int userId, UserSettings userSettings)
        {
            var sqlCommand = "UPDATE sales_user SET send_events = @send_events where id = @id";
            return Connection.Execute(sqlCommand,
                new
                {
                    send_events = userSettings.send_events,
                    id = userId
                });
        }
        public int UpdateAdvertiseSetting(int userId, UserSettings userSettings)
        {
            var sqlCommand = "UPDATE sales_user SET send_advertisement = @send_advertisement where id = @id";
            return Connection.Execute(sqlCommand,
                new
                {
                    send_advertisement = userSettings.send_advertisement,
                    id = userId
                });
        }
        public int UpdateFeedbackSetting(int userId, UserSettings userSettings)
        {
            var sqlCommand = "UPDATE sales_user SET send_feedback = @send_feedback where id = @id";
            return Connection.Execute(sqlCommand,
                new
                {
                    send_feedback = userSettings.send_feedback,
                    id = userId
                });
        }
        public int UpdateBirthdaySetting(int userId, UserSettings userSettings)
        {
            var sqlCommand = "UPDATE sales_user SET send_birthday = @send_birthday where id = @id";
            return Connection.Execute(sqlCommand,
                new
                {
                    send_birthday = userSettings.send_birthday,
                    id = userId
                });
        }

        public int UpdateSendReminderFlag(int userId, int sendReminderFlag)
        {
            var sqlCommand = "UPDATE sales_user SET send_reminders = @srf where id = @id";
            return Connection.Execute(sqlCommand, new { srf = sendReminderFlag, id = userId });
        }
        public int UpdateReminderDuration(int userId, int reminderDuration)
        {
            var sqlCommand = "UPDATE sales_user SET reminder_duration = @rd where id = @id";
            return Connection.Execute(sqlCommand, new { rd = reminderDuration, id = userId });
        }
        public int UpdateReminderTimes(int userId, int reminderTimes)
        {
            var sqlCommand = "UPDATE sales_user SET reminder_times = @rt where id = @id";
            return Connection.Execute(sqlCommand, new { rt = reminderTimes, id = userId });
        }
        #endregion

        public Model.MarketingModels.UserSettings Update(Model.MarketingModels.UserSettings UserSettings)
        {

            var sqlCommand = "UPDATE marketing_user_setting SET value = @value "
                             + "WHERE id = @id";
            Connection.Execute(sqlCommand, UserSettings);
            return UserSettings;
        }
        public List<Model.MarketingModels.UserSettings> GetByUserId(int userId)
        {

            //var sqlCommand = "select * from marketing_user_setting where sales_userId = @userId";
            //return Connection.Query<UserSettings>(sqlCommand, new { userId = userId }).ToList();
            return null;
        }
    }
}