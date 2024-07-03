using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using SoftMarketing.Model;
using SoftMarketing.DAL.UnitOfWork;
using SoftMarketing.DAL.DataAccess.MarketingDAL;
using SoftMarketing.Services.Sales;
using SoftMarketing.Model.SalesModels;

namespace SoftMarketing.Services.Marketing
{

    public class TemplateService
    {
        private IUserService UserService { get; set; }
        public TemplateService(IUserService userService = null)
        {
            if (userService != null)
                UserService = userService;
            else
                UserService = new UserService();
        }
        public List<UserTemplateCountry> GetCountryTemplates(int userId, int countryId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                TemplateDAL TemplateDAL = (TemplateDAL)unitOfWork.Repository<TemplateDAL>();
                return TemplateDAL.GetCountryTemplates(userId, countryId);
            }
        }

        public List<User_Template> GetAllUserTemplates(int userId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                TemplateDAL TemplateDAL = (TemplateDAL)unitOfWork.Repository<TemplateDAL>();
                return TemplateDAL.GetAllUserTemplates(userId);
            }
        }
        public List<TemplateDate> GetTemplateDates(int userId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                TemplateDAL TemplateDAL = (TemplateDAL)unitOfWork.Repository<TemplateDAL>();
                return TemplateDAL.GetTemplatesDate(userId);
            }
        }
        public List<User_Template> GetAllUserTemplatesWithImages(int userId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                TemplateDAL TemplateDAL = (TemplateDAL)unitOfWork.Repository<TemplateDAL>();
                return TemplateDAL.GetAllUserTemplatesWithImages(userId);
            }
        }
        
        public List<User_Template> GetSpecificUserTemplates(int userId, string uTemplateIDs)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                TemplateDAL TemplateDAL = (TemplateDAL)unitOfWork.Repository<TemplateDAL>();
                return TemplateDAL.GetSpecificUserTemplates(userId, uTemplateIDs);
            }
        }

        public List<Templates> Get(int templateId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                TemplateDAL TemplateDAL = (TemplateDAL)unitOfWork.Repository<TemplateDAL>();
                return TemplateDAL.Get(templateId);
            }
        }

        public long SubsicribeToCountryTemplates(List<SubscriptionTemplate> userTemplates,int userId)
        {
            long numOfEffectedRows = -1;
            using (var unitOfWork = new UnitOfWork(true))
            {
                TemplateDAL TemplateDAL = (TemplateDAL)unitOfWork.Repository<TemplateDAL>();
                numOfEffectedRows = TemplateDAL.SubsicribeToCountryTemplates(userTemplates);
                UserService.UpdateLastUpdateTemplate(userId, unitOfWork);
                unitOfWork.SaveChanges();
            }
            return numOfEffectedRows;
        }

        public string SubsicribeToCountryTemplate(SubscriptionTemplate userTemplates,int userId)
        {
            long userTemplateId ;
            using (var unitOfWork = new UnitOfWork(true))
            {
                TemplateDAL TemplateDAL = (TemplateDAL)unitOfWork.Repository<TemplateDAL>();
                userTemplates.sales_userId = userId;
                userTemplateId = TemplateDAL.SubsicribeToCountryTemplate(userTemplates);
                UserService.UpdateLastUpdateTemplate(userId, unitOfWork);
                unitOfWork.SaveChanges();
            }
            return userTemplateId.ToString();
        }
        public string AddCustomTemplate(User_Template Template, int userId)
        {
            string numOfEffectedRows = "-1";
            using (var unitOfWork = new UnitOfWork(true))
            {
                TemplateDAL TemplateDAL = (TemplateDAL)unitOfWork.Repository<TemplateDAL>();
                numOfEffectedRows = TemplateDAL.AddCustomTemplate(Template);
                UserService.UpdateLastUpdateTemplate(userId, unitOfWork);
                unitOfWork.SaveChanges();
            }
            return numOfEffectedRows;
        }
        public int UpdateUserTemplate(User_Template Template, int userId)
        {
            int numOfEffectedRows = -1;
            using (var unitOfWork = new UnitOfWork(true))
            {
                TemplateDAL TemplateDAL = (TemplateDAL)unitOfWork.Repository<TemplateDAL>();
                numOfEffectedRows = TemplateDAL.UpdateUserTemplate(Template);
                UserService.UpdateLastUpdateTemplate(userId, unitOfWork);
                unitOfWork.SaveChanges();
            }
            return numOfEffectedRows;
        }
        public int DeleteUserTemplate(int templateId,int userId)
        {
            int numOfEffectedRows = -1;
            using (var unitOfWork = new UnitOfWork(true))
            {
                TemplateDAL TemplateDAL = (TemplateDAL)unitOfWork.Repository<TemplateDAL>();
                numOfEffectedRows = TemplateDAL.DeleteUserTemplate(templateId, userId);
                UserService.UpdateLastUpdateTemplate(userId, unitOfWork);
                unitOfWork.SaveChanges();
            }
            return numOfEffectedRows;
        }

    }
}