using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using SoftMarketing.Model;
using SoftMarketing.DAL.UnitOfWork;
using SoftMarketing.DAL.DataAccess.MarketingDAL;
using SoftMarketing.DAL.Helper;
using SoftMarketing.Model.SalesModels;

namespace SoftMarketing.Services.Sales
{
    public interface IUserService
    {
        public string Add(User user);
        public string Register(User user);
        public User Get(string phone, int countryId);
        public User GetById(int id);
        public User GetSalesUserAndRefreshTokensByUserId(int id);
        public User GetByRefreshToken(string rToken);
        public User GetSalesUserAndRefreshTokensByToken(string rToken);
        public int UpdateRefreshToken(RefreshToken rToken);
        public string AddRefreshToken(RefreshToken rToken);
        public List<RefreshToken> GetRefreshTokensByUserId(int userId);
        public int RemoveRefreshTokenByTokenId(int tokenId);
        public int RemoveRefreshTokenByUserId(int userId);
        public void RevokeRefreshToken(User user, RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null);
        public SyncedData Sync(User user, List<string> toBeSynced);
        public int SetCustomerTS(int userId, string timestamp);
        public int SetMessageTS(int userId, string timestamp);
        public int SetSettingTS(int userId, string timestamp);
        public int SetTemplateTS(int userId, string timestamp);
        public int UpdateLastUpdateCustomer(int userId, UnitOfWork unitOfWork);
        public int UpdateLastUpdateTemplate(int userId, UnitOfWork unitOfWork);
        public int UpdateLastUpdateMessage(int userId, UnitOfWork unitOfWork);
        public int UpdateLastUpdateSetting(int userId, UnitOfWork unitOfWork);
        //public bool UpdateHubConnections(int userId, int appSourceId, string connectionId);
    }
    public class UserService : IUserService
    {
        UserDAL userDAL { get; set; }
        public UserService()
        {
            userDAL = new UserDAL();
        }
        //public bool UpdateHubConnections(int userId, int appSourceId, string connectionId)
        //{
        //    int result = 0;
        //    using (var unitOfWork = new UnitOfWork(true))
        //    {
        //        UserDAL UserDAL = (UserDAL)unitOfWork.Repository<UserDAL>();
        //        UserDAL.UpdateHubConnections(userId, appSourceId, connectionId);
        //        unitOfWork.SaveChanges();
        //    }
        //    return result == 1;
        //}
        public string Add(User user)
        {
            string id = string.Empty;
            using (var unitOfWork = new UnitOfWork(true))
            {
                UserDAL UserDAL = (UserDAL)unitOfWork.Repository<UserDAL>();
                id = UserDAL.Add(user);
                unitOfWork.SaveChanges();
            }

            return id;
        }
        public string Register(User user)
        {
            string id = string.Empty;
            using (var unitOfWork = new UnitOfWork(true))
            {
                UserDAL UserDAL = (UserDAL)unitOfWork.Repository<UserDAL>();
                id = UserDAL.Register(user);
                unitOfWork.SaveChanges();
            }

            return id;
        }
        public int SetCustomerTS(int userId, string timestamp)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                UserDAL UserDAL = (UserDAL)unitOfWork.Repository<UserDAL>();
                return UserDAL.SetCustomerTS(userId, timestamp);
            }
        }
        public int SetMessageTS(int userId, string timestamp)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                UserDAL UserDAL = (UserDAL)unitOfWork.Repository<UserDAL>();
                return UserDAL.SetMessageTS(userId, timestamp);
            }
        }
        public int SetSettingTS(int userId, string timestamp)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                UserDAL UserDAL = (UserDAL)unitOfWork.Repository<UserDAL>();
                return UserDAL.SetSettingTS(userId, timestamp);
            }
        }
        public int SetTemplateTS(int userId, string timestamp)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                UserDAL UserDAL = (UserDAL)unitOfWork.Repository<UserDAL>();
                return UserDAL.SetTemplateTS(userId, timestamp);
            }
        }
        public User Get(string phone, int countryId)
        {
            User user = null;
            using (var unitOfWork = new UnitOfWork())
            {
                UserDAL UserDAL = (UserDAL)unitOfWork.Repository<UserDAL>();
                user = UserDAL.Get(phone, countryId);
            }
            return user;
        }
        public User GetById(int id)
        {
            User user = null;
            using (var unitOfWork = new UnitOfWork())
            {
                UserDAL UserDAL = (UserDAL)unitOfWork.Repository<UserDAL>();
                user = UserDAL.GetById(id);
                if (user == null)
                {
                    throw new Exception("user Not found!");
                }
            }
            return user;
        }

        public User GetSalesUserAndRefreshTokensByUserId(int id)
        {
            User user = null;
            using (var unitOfWork = new UnitOfWork())
            {
                UserDAL UserDAL = (UserDAL)unitOfWork.Repository<UserDAL>();
                user = UserDAL.GetSalesUserAndRefreshTokensByUserId(id);
                if (user == null)
                {
                    throw new Exception("user Not found!");
                }
            }
            return user;
        }

        public User GetByRefreshToken(string rToken)
        {
            User user = null;
            using (var unitOfWork = new UnitOfWork())
            {
                UserDAL UserDAL = (UserDAL)unitOfWork.Repository<UserDAL>();
                user = UserDAL.GetByRefreshToken(rToken);
                if (user == null)
                    throw new Exception("Invalid token");

            }
            return user;
        }

        public User GetSalesUserAndRefreshTokensByToken(string rToken)
        {
            User user = null;
            using (var unitOfWork = new UnitOfWork())
            {
                UserDAL UserDAL = (UserDAL)unitOfWork.Repository<UserDAL>();
                user = UserDAL.GetSalesUserAndRefreshTokensByToken(rToken);
                if (user.id <= 0)
                    return null;

            }
            return user;
        }

        public string GetRTokenByToken(string rToken)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                UserDAL UserDAL = (UserDAL)unitOfWork.Repository<UserDAL>();
                return UserDAL.GetRTokenByToken(rToken)?.Token;

            }
        }

        public int UpdateRefreshToken(RefreshToken rToken)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                UserDAL UserDAL = (UserDAL)unitOfWork.Repository<UserDAL>();
                return UserDAL.UpdateRefreshToken(rToken);
            }
        }

        public void RevokeRefreshToken(User user, RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replacedByToken;

            using (var unitOfWork = new UnitOfWork(true))
            {
                UserDAL UserDAL = (UserDAL)unitOfWork.Repository<UserDAL>();
                UserDAL.UpdateRefreshToken(token);
                unitOfWork.SaveChanges();
            }
        }

        public string AddRefreshToken(RefreshToken rToken)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                UserDAL UserDAL = (UserDAL)unitOfWork.Repository<UserDAL>();
                return UserDAL.AddRefreshToken(rToken);
            }
        }

        public List<RefreshToken> GetRefreshTokensByUserId(int userId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                UserDAL UserDAL = (UserDAL)unitOfWork.Repository<UserDAL>();
                return UserDAL.GetRefreshTokensByUserId(userId);
            }
        }
        public List<RefreshToken> GetRefreshTokensByUserIdAndConnectionId(int userId, string connId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                UserDAL UserDAL = (UserDAL)unitOfWork.Repository<UserDAL>();
                return UserDAL.GetRefreshTokensByUserIdAndConnectionId(userId,connId);
            }
        }

        public int RemoveRefreshTokenByTokenId(int tokenId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                UserDAL UserDAL = (UserDAL)unitOfWork.Repository<UserDAL>();
                return UserDAL.RemoveRefreshTokenByTokenId(tokenId);
            }
        }

        public int RemoveRefreshTokenByUserId(int userId)
        {
            int result = 0;
            using (var unitOfWork = new UnitOfWork(true))
            {
                UserDAL UserDAL = (UserDAL)unitOfWork.Repository<UserDAL>();
                UserDAL.UpdateOTP(userId, "");
                result = UserDAL.RemoveRefreshTokenByUserId(userId);
                unitOfWork.SaveChanges();
            }
            return result;
        }
        public SyncedData Sync(User user, List<string> toBeSynced)
        {
            SyncedData syncedData = new SyncedData();

            // to get the latest last-update values
            user = GetById(user.id);
            syncedData.last_update_customer = user.last_update_customer;
            syncedData.last_update_message = user.last_update_message;
            syncedData.last_update_template = user.last_update_template;
            syncedData.last_update_template_date = user.last_update_template_date;
            syncedData.last_update_setting = user.last_update_setting;

            using (var unitOfWork = new UnitOfWork())
            {
                if (toBeSynced.Contains("customer"))
                {
                    CustomerDAL CustomerDAL = (CustomerDAL)unitOfWork.Repository<CustomerDAL>();
                    syncedData.Customers = CustomerDAL.GetAll(user.id);
                }
                if (toBeSynced.Contains("message"))
                {
                    MessageDAL MessageDAL = (MessageDAL)unitOfWork.Repository<MessageDAL>();
                    syncedData.Messages = MessageDAL.GetAllScheduledMessages(user.id);
                }
                if (toBeSynced.Contains("template"))
                {
                    TemplateDAL TemplateDAL = (TemplateDAL)unitOfWork.Repository<TemplateDAL>();
                    syncedData.Templates = TemplateDAL.GetAllUserTemplatesWithImages(user.id);
                }
                if (toBeSynced.Contains("template-date"))
                {
                    TemplateDAL TemplateDAL = (TemplateDAL)unitOfWork.Repository<TemplateDAL>();
                    syncedData.TemplateDates = TemplateDAL.GetTemplatesDate(user.id);
                }
                if (toBeSynced.Contains("setting"))
                {
                    UserSettingsDAL UserSettingsDAL = (UserSettingsDAL)unitOfWork.Repository<UserSettingsDAL>();
                    syncedData.Settings = UserSettingsDAL.GetUserSettings(user.id);
                }
            }
            return syncedData;
        }

        public int UpdateLastUpdateCustomer(int userId, UnitOfWork unitOfWork)
        {
            UserDAL UserDAL = (UserDAL)unitOfWork.Repository<UserDAL>();
            return UserDAL.UpdateLastUpdateCustomer(userId);
        }
        public int UpdateLastUpdateTemplate(int userId, UnitOfWork unitOfWork)
        {
            UserDAL UserDAL = (UserDAL)unitOfWork.Repository<UserDAL>();
            return UserDAL.UpdateLastUpdateTemplate(userId);
        }
        public int UpdateLastUpdateMessage(int userId, UnitOfWork unitOfWork)
        {
            UserDAL UserDAL = (UserDAL)unitOfWork.Repository<UserDAL>();
            return UserDAL.UpdateLastUpdateMessage(userId);
        }
        public int UpdateLastUpdateSetting(int userId, UnitOfWork unitOfWork)
        {
            UserDAL UserDAL = (UserDAL)unitOfWork.Repository<UserDAL>();
            return UserDAL.UpdateLastUpdateSetting(userId);
        }
    }
}