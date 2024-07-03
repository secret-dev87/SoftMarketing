#region using directives
using System.Data;
using SoftMarketing.Model;
using SoftMarketing.Model.MarketingModels;
using MySql.Data.MySqlClient;
using SoftMarketing.DAL.UnitOfWork;
using Dapper;
using Dapper.Contrib.Extensions;
using SoftMarketing.Model.SalesModels;
#endregion

namespace SoftMarketing.DAL.DataAccess
{
    public class LookupDAL : DataAccessBase
    {
        public List<CommonApp> GetCommonApp(int loginPhone)
        {
            //var sql = "select * from common_app where login_phone=" + loginPhone.ToString();
             var sql = "SELECT common_app.common_translationId as id, common_translation.english as name, common_app.login_phone as type FROM common_app INNER JOIN common_translation ON common_app.common_translationId = common_translation.id WHERE common_app.login_phone =" + loginPhone.ToString();
            return Connection.Query<CommonApp>(sql).ToList();
        }
        public List<CommonApp> GetSocialApp()
        {
            var sql = "select * from common_app where type ="+ "'Social'";
            return Connection.Query<CommonApp>(sql).ToList();
        }
        public List<CategoryType> GetCategoryTypes()
        {
            return Connection.GetAll<CategoryType>().ToList();
        }

		public List<MainCategory> GetMainCategoryList(int categoryTypeId)
		{
			return Connection.Query<MainCategory>("listing_category_main", new { category_type = categoryTypeId }, commandType: CommandType.StoredProcedure).ToList();
		}
		public List<ChildCategory> GetChildCategoryList(int categoryDetailId)
		{
			return Connection.Query<ChildCategory>("listing_category_child", new { pr_category_detail_id = categoryDetailId }, commandType: CommandType.StoredProcedure).ToList();
		}
		
    }
}
