#region using directives

using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using SoftMarketing.Model;
using MySql.Data.MySqlClient;
using Dapper;
using Dapper.Contrib.Extensions;
using SoftMarketing.DAL.UnitOfWork;
#endregion

namespace SoftMarketing.DAL.DataAccess.MarketingDAL
{

    public class TemplateDAL : DataAccessBase
    {
        public List<UserTemplateCountry> GetCountryTemplates(int uId, int countryId)
        {
            return Connection.Query<UserTemplateCountry>("marketing_user_template_country_getall", new { salesuserid = uId, commoncountryid = countryId }, commandType: CommandType.StoredProcedure).ToList();
        }
        public List<User_Template> GetAllUserTemplates(int userId)
        {
            return Connection.Query<User_Template>("marketing_user_template_getall", new { userId = userId }, commandType: CommandType.StoredProcedure).ToList();
        }
        public List<TemplateDate> GetTemplatesDate(int userId)
        {
            return Connection.Query<TemplateDate>("marketing_template_date_getall", new { userid = userId }, commandType: CommandType.StoredProcedure).ToList();
        }
        public List<User_Template> GetAllUserTemplatesWithImages(int userId)
        {
            return Connection.Query<User_Template>("marketing_user_template_getall_images", new { userId = userId }, commandType: CommandType.StoredProcedure).ToList();
        }
        public List<User_Template> GetSpecificUserTemplates(int userId, string uTemplateIDs)
        {
            return Connection.Query<User_Template>("marketing_user_template_get_images", new { userid = userId, usertemplate_id = uTemplateIDs }, commandType: CommandType.StoredProcedure).ToList();
        }
        public List<Templates> Get(int templateId)
        {
            return Connection.Query<Templates>("marketing_user_template_getsingle", new { usertemplate_id = templateId }, commandType: CommandType.StoredProcedure).ToList();
        }
        public long SubsicribeToCountryTemplates(List<SubscriptionTemplate> userTemplate)
        {
            return Connection.Insert<List<SubscriptionTemplate>>(userTemplate);
        }

        public long SubsicribeToCountryTemplate(SubscriptionTemplate userTemplate)
        {
            //var parameter = new DynamicParameters();
            //parameter.Add("@sales_userId", userTemplate.sales_userId);
            //parameter.Add("@marketing_template_countryId", userTemplate.marketing_template_countryId);
            //var result = Connection.ExecuteScalar("subsicribe_user_to_country_templates", parameter, commandType: CommandType.StoredProcedure)?.ToString();
            return Connection.Insert<SubscriptionTemplate>(userTemplate);
        }
        public string AddCustomTemplate(User_Template template)
        {
            ValidateObject(template);
            var parameter = new DynamicParameters();
            parameter.Add("@userid", template.sales_userId);
            parameter.Add("@nameval", template.name);
            parameter.Add("@templateval", template.template);
            parameter.Add("@sending_date", template.sending_date);
            parameter.Add("@sending_year", template.sending_year);
            parameter.Add("@add_customer", template.add_customer);
            parameter.Add("@add_owner", template.add_owner);
            parameter.Add("@add_business", template.add_business);
            parameter.Add("@send_text_with_image", template.send_text_with_image);
            //parameter.Add("@result", dbType: DbType.String, direction: ParameterDirection.ReturnValue);
            var result = Connection.ExecuteScalar("marketing_user_template_insert", parameter, commandType: CommandType.StoredProcedure)?.ToString();
            return result;
        }
        public int UpdateUserTemplate(User_Template template)
        {
            ValidateObject(template);
            var parameter = new DynamicParameters();
            parameter.Add("@usertemplate_id", template.usertemplate_id);
            parameter.Add("@userid", template.sales_userId);
            parameter.Add("@nameval", template.name);
            parameter.Add("@templateval", template.template);
            parameter.Add("@dateval", template.sending_date);
            parameter.Add("@yrval", template.sending_year);
            parameter.Add("@add_customer", template.add_customer);
            parameter.Add("@add_owner", template.add_owner);
            parameter.Add("@add_business", template.add_business);
            parameter.Add("@send_text_with_image", template.send_text_with_image);
            //parameter.Add("@result", dbType: DbType.String, direction: ParameterDirection.ReturnValue);
            var result = Connection.Execute("marketing_user_template_update", parameter,commandType:CommandType.StoredProcedure);
            return result;
        }
        public int DeleteUserTemplate(int templateId,int userId)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@usertemplate_id", templateId); ;
            parameter.Add("@salesuserid", userId); ;
            var result = Connection.Execute("marketing_user_template_delete", parameter,commandType:CommandType.StoredProcedure);
            return result;
        }

        private void ValidateObject(User_Template user_Template)
        {
            user_Template.sending_year = user_Template.sending_year == string.Empty ? null : user_Template.sending_year;
            user_Template.sending_date = user_Template.sending_date == string.Empty ? null : user_Template.sending_date;
        }
    }
}