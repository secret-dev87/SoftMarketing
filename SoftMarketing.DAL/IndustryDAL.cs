#region using directives

using System.Data;
using System.Data.Common;
using SoftMarketing.Model;
using MySql.Data.MySqlClient;
using Dapper;
using SoftMarketing.DAL.MySQL.Helper;

#endregion

namespace SoftMarketing.DAL
{

    public class IndustryDAL
	{
		
		public IndustryDAL(){
		}

		public Int32 Add(Industry industry, DbTransaction transaction)
		{
			if (transaction == null) throw new ArgumentNullException("trans");

			var parms = new DynamicParameters();
			parms.Add("Id", industry.IndustryID, DbType.Int32, ParameterDirection.Output);
			parms.Add("Name", industry.Name);
			parms.Add("Description", industry.Description);

			var connection = transaction.Connection;
			var data = connection.Query<Industry>("[dbo].[IndustryInsert]", new
			{
                parms,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);

			return data.Count();
		}


		public  Int32 Update(Industry industry, DbTransaction transaction)
		{
			var parms = new DynamicParameters();
			parms.Add("IndustryID", industry.IndustryID);
			parms.Add("Name", industry.Name);
			parms.Add("Description", industry.Description);
			var connection = transaction.Connection;
			var data = connection.Query<Industry>("[dbo].[IndustryUpdate]", new
			{
               parms,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);

			return data.FirstOrDefault().IndustryID;
		}

		public  void Delete(Int32 industryId, DbTransaction transaction)
		{
			var parms = new DynamicParameters();
			parms.Add("IndustryID", industryId);
			var connection = transaction.Connection;
			var data = connection.Query<Industry>("[dbo].[IndustryDelete]", new
			{
				parms,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);

		}

		public virtual Industry GetById(Int32 industryId, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;

			var parms = new DynamicParameters();
			parms.Add("Id", industryId);

			using (var connection = Database.GetNewConnection(connString, true))
			{
				return (Industry)connection.Query<Industry>("IndustryGetByID", parms, commandType: CommandType.StoredProcedure);
			}
		}
	}
}