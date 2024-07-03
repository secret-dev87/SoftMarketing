#region using directives

using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using SoftMarketing.Model;
using MySql.Data.MySqlClient;
using Dapper;
using SoftMarketing.DAL.MySQL.Helper;
#endregion

namespace SoftMarketing.DAL
{
	
	public class Template_DatesDAL
	{
		
		public Template_DatesDAL(){
		}
		public DynamicParameters FillParams(Template_Dates Template_Dates)
		{
			var parms = new DynamicParameters();
			parms.Add("Country_or_year", Template_Dates.country_or_year);
			parms.Add("Name", Template_Dates.name);
			parms.Add("Date", Template_Dates.date);
			parms.Add("Id", Template_Dates.id);
			return parms;
		}

		public Int32 Add(Template_Dates Template_Dates, DbTransaction transaction)
		{
			var parms = FillParams(Template_Dates);
			var connection = transaction.Connection;
			var data = connection.Query<Template_Dates>("Template_datesInsert", new
			{
				parms,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);

			return data.Count();
		}

		public Int32 Update(Template_Dates Template_Dates, DbTransaction transaction)
		{
			var parms = FillParams(Template_Dates);
			var connection = transaction.Connection;
			var data = connection.Query<Template_Dates>("Template_datesUpdate", new
			{
				parms,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);

			return data.FirstOrDefault().id;
		}

		public void Delete(Int32 id, DbTransaction transaction)
		{

			var parms = new DynamicParameters();
			parms.Add("Id", id);
			var connection = transaction.Connection;
			var data = connection.Query<Template_Dates>("Template_datesDelete", new
			{
				parms,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);
		}

		public IEnumerable<Template_Dates> GetPagedData(Int32 pageFirstRow, Int32 pageRowCount, String toPageOn, String toSortOn, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			var parms = new DynamicParameters();
			parms.Add("pageFirstRow", pageFirstRow);
			parms.Add("pageRowCount", pageRowCount);
			parms.Add("toPageOn", toPageOn);
			parms.Add("toSortOn", toSortOn);
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<Template_Dates>("Template_datesGetPagingData", parms, commandType: CommandType.StoredProcedure);
			}
		}
		//public virtual void DeleteAll(DbTransaction transaction){
		//		Database database =DatabaseFactory.CreateDatabase();
		//		DbCommand command = database.GetStoredProcCommand("Template_datesDeleteAll");
		//		try{
		//			if(transaction!=null){
		//				database.ExecuteNonQuery(command,transaction);
		//			}
		//			else{
		//				database.ExecuteNonQuery(command);	
		//			}
		//		}
		//		catch(DbException ex){
		//			throw new DataException("An data access error occured, please check inner SqlException.", ex);
		//		}	
		//}

		public Template_Dates Get(Int32 id, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			var parms = new DynamicParameters();
			parms.Add("Id", id);
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return (Template_Dates)connection.Query<Template_Dates>("Template_datesGet", parms, commandType: CommandType.StoredProcedure);
			}
		}

		public Int32 CountAll(string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<Template_Dates>("Template_datesCountAll", null, commandType: CommandType.StoredProcedure).Count();
			}
		}

		public IEnumerable<Template_Dates> GetAll(string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<Template_Dates>("Template_datesGetAll", null, commandType: CommandType.StoredProcedure);
			}
		}
		public IEnumerable<Template_Dates> GetByPk(Int32 id, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			var parms = new DynamicParameters();
			parms.Add("Id", id);
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<Template_Dates>("Template_datesGetByPk", parms, commandType: CommandType.StoredProcedure);
			}
		}
		
		//public virtual IDataReader GetCustomView(IList list){
		//	string str_cmdview="";
		//	str_cmdview="select";
		//	int TotalCols=list.Count,index;
		//	for(index=0;index<TotalCols;index++){
		//			if(index==TotalCols){
		//			str_cmdview=str_cmdview+list[index];
		//			}
		//			str_cmdview=str_cmdview+" "+list[index]+",";
		//		}
		//	str_cmdview=str_cmdview+" from template_dates";
			
		//	Database database = DatabaseFactory.CreateDatabase();
		//	DbCommand command = database.GetStoredProcCommand(str_cmdview);
		//	IDataReader reader = null;
		//	try{
		//		 reader = database.ExecuteReader(command);
		//	}
		//	catch(DbException ex) {	
		//		throw new DataException("An data access error occured, please check inner exception.", ex);
		//	}
		//	return reader;
		//}
	}
}