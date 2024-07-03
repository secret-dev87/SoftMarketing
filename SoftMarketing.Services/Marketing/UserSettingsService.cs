using SoftMarketing.DAL.UnitOfWork;
using SoftMarketing.Model.MarketingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftMarketing.DAL.DataAccess.MarketingDAL;
using SoftMarketing.Model;
using SoftMarketing.Model.SalesModels;

namespace SoftMarketing.Services.Marketing
{
    public class UserSettingsService
    {
		public UserSettings GetUserSettings(int userId)
		{
			using (var unitOfWork = new UnitOfWork())
			{
				UserSettingsDAL UserSettingsDAL = (UserSettingsDAL)unitOfWork.Repository<UserSettingsDAL>();
                return UserSettingsDAL.GetUserSettings(userId);
			}
		}
		public List<UserCategory> GetCategoriesWithTemplates(int userId, int? categoryDetailId)
        {
			using (var unitOfWork = new UnitOfWork())
			{
				UserSettingsDAL UserSettingsDAL = (UserSettingsDAL)unitOfWork.Repository<UserSettingsDAL>();
                return UserSettingsDAL.GetCategoriesWithTemplates(userId, categoryDetailId);
			}
		}
		public int UpdateReminderSettings(int userId, UserSettings ReminderSettings)
		{
			int result = 0;
			using (var unitOfWork = new UnitOfWork())
			{
				UserSettingsDAL UserSettingsDAL = (UserSettingsDAL)unitOfWork.Repository<UserSettingsDAL>();
				result = UserSettingsDAL.UpdateReminderSettings(userId, ReminderSettings);
			}
			return result;
		}
		public int UpdateUserSettings(int userId, UserSettings UserSettings)
		{
			int result = 0;
			using (var unitOfWork = new UnitOfWork())
			{
				UserSettingsDAL UserSettingsDAL = (UserSettingsDAL)unitOfWork.Repository<UserSettingsDAL>();
				result = UserSettingsDAL.UpdateUserSettings(userId, UserSettings);
			}
			return result;
		}
		public int UpdateEventsSetting(int userId, UserSettings ReminderSettings)
		{
			int result = 0;
			using (var unitOfWork = new UnitOfWork())
			{
				UserSettingsDAL UserSettingsDAL = (UserSettingsDAL)unitOfWork.Repository<UserSettingsDAL>();
				result = UserSettingsDAL.UpdateEventsSetting(userId, ReminderSettings);
			}
			return result;
		}
		public int UpdateAdvertiseSetting(int userId, UserSettings ReminderSettings)
		{
			int result = 0;
			using (var unitOfWork = new UnitOfWork())
			{
				UserSettingsDAL UserSettingsDAL = (UserSettingsDAL)unitOfWork.Repository<UserSettingsDAL>();
				result = UserSettingsDAL.UpdateAdvertiseSetting(userId, ReminderSettings);
			}
			return result;
		}
		public int UpdateFeedbackSetting(int userId, UserSettings ReminderSettings)
		{
			int result = 0;
			using (var unitOfWork = new UnitOfWork())
			{
				UserSettingsDAL UserSettingsDAL = (UserSettingsDAL)unitOfWork.Repository<UserSettingsDAL>();
				result = UserSettingsDAL.UpdateFeedbackSetting(userId, ReminderSettings);
			}
			return result;
		}
		public int UpdateBirthdaySetting(int userId, UserSettings ReminderSettings)
		{
			int result = 0;
			using (var unitOfWork = new UnitOfWork())
			{
				UserSettingsDAL UserSettingsDAL = (UserSettingsDAL)unitOfWork.Repository<UserSettingsDAL>();
				result = UserSettingsDAL.UpdateBirthdaySetting(userId, ReminderSettings);
			}
			return result;
		}
		public int UpdateDeleteCustomerFlag(int userId, int deleteCustomerFlag)
		{
			int result = 0;
			using (var unitOfWork = new UnitOfWork())
			{
				UserSettingsDAL UserSettingsDAL = (UserSettingsDAL)unitOfWork.Repository<UserSettingsDAL>();
				result = UserSettingsDAL.UpdateDeleteCustomerFlag(userId, deleteCustomerFlag);
			}
			return result;
		}
		public int UpdateSendReminderFlag(int userId, int sendReminderFlag)
		{
			int result = 0;
			using (var unitOfWork = new UnitOfWork())
			{
				UserSettingsDAL UserSettingsDAL = (UserSettingsDAL)unitOfWork.Repository<UserSettingsDAL>();
				result = UserSettingsDAL.UpdateSendReminderFlag(userId, sendReminderFlag);
			}
			return result;
		}
		public int UpdateReminderDuration(int userId, int reminderDuration)
		{
			int result = 0;
			using (var unitOfWork = new UnitOfWork())
			{
				UserSettingsDAL UserSettingsDAL = (UserSettingsDAL)unitOfWork.Repository<UserSettingsDAL>();
				result = UserSettingsDAL.UpdateReminderDuration(userId, reminderDuration);
			}
			return result;
		}
		public int UpdateReminderTimes(int userId, int reminderTimes)
		{
			int result = 0;
			using (var unitOfWork = new UnitOfWork())
			{
				UserSettingsDAL UserSettingsDAL = (UserSettingsDAL)unitOfWork.Repository<UserSettingsDAL>();
				result = UserSettingsDAL.UpdateReminderTimes(userId, reminderTimes);
			}
			return result;
		}



		public string InsertUserCategory(int userId, int categoryDetailId)
		{
			string result = string.Empty;
			using (var unitOfWork = new UnitOfWork())
			{
				UserSettingsDAL UserSettingsDAL = (UserSettingsDAL)unitOfWork.Repository<UserSettingsDAL>();
				result = UserSettingsDAL.InsertUserCategory(userId, categoryDetailId);
			}
			return result;
		}
		

		public string InsertAdvertisementTemplate(int userId, int templateDetailId)
		{
			string result = string.Empty;
			using (var unitOfWork = new UnitOfWork())
			{
				UserSettingsDAL UserSettingsDAL = (UserSettingsDAL)unitOfWork.Repository<UserSettingsDAL>();
				result = UserSettingsDAL.InsertAdvertisementTemplate(userId, templateDetailId);
			}
			return result;
		}

		public string DeleteUserCategory(int userId, int categoryDetailId)
		{
			string result = string.Empty;
			using (var unitOfWork = new UnitOfWork())
			{
				UserSettingsDAL UserSettingsDAL = (UserSettingsDAL)unitOfWork.Repository<UserSettingsDAL>();
				result = UserSettingsDAL.DeleteUserCategory(userId, categoryDetailId);
			}
			return result;
		}
	}
}
