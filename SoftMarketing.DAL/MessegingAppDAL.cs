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
	
	public class MessegingAppDAL
	{
		
		public MessegingAppDAL(){
		}
		public DynamicParameters FillParams(MessegingApp MessegingApp)
		{
			var parms = new DynamicParameters();
			parms.Add("Trial_id_1", MessegingApp.id);
			parms.Add("Trial_appname_2", MessegingApp.appname);
			parms.Add("Appdetails", MessegingApp.appdetails);
			return parms;
		}

		public Int32 Add(MessegingApp MessegingApp, DbTransaction transaction)
		{
			var parms = FillParams(MessegingApp);
			var connection = transaction.Connection;
			var data = connection.Query<MessegingApp>("MessegingappInsert", new
			{
				parms,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);

			return data.Count();
		}

		public Int32 Update(MessegingApp MessegingApp, DbTransaction transaction)
		{
			var parms = FillParams(MessegingApp);
			var connection = transaction.Connection;
			var data = connection.Query<MessegingApp>("MessegingappUpdate", new
			{
				parms,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);

			return data.FirstOrDefault().id;
		}

		public void Delete(Int32 trial_id_1, DbTransaction transaction)
		{

			var parms = new DynamicParameters();
			parms.Add("Trial_id_1", trial_id_1);
			var connection = transaction.Connection;
			var data = connection.Query<MessegingApp>("MessegingappDelete", new
			{
				parms,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);
		}

		public IEnumerable<MessegingApp> GetPagedData(Int32 pageFirstRow, Int32 pageRowCount, String toPageOn, String toSortOn, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			var parms = new DynamicParameters();
			parms.Add("pageFirstRow", pageFirstRow);
			parms.Add("pageRowCount", pageRowCount);
			parms.Add("toPageOn", toPageOn);
			parms.Add("toSortOn", toSortOn);
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<MessegingApp>("MessegingappGetPagingData", parms, commandType: CommandType.StoredProcedure);
			}
		}

		//public virtual void DeleteAll(DbTransaction transaction){
		//		Database database =DatabaseFactory.CreateDatabase();
		//		DbCommand command = database.GetStoredProcCommand("MessegingappDeleteAll");
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

		public IEnumerable<MessegingApp> Get(Int32 trial_id_1, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			var parms = new DynamicParameters();
			parms.Add("Trial_id_1", trial_id_1);
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<MessegingApp>("MessegingappGet", parms, commandType: CommandType.StoredProcedure);
			}
		}

		public Int32 CountAll(string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<MessegingApp>("MessegingappCountAll", null, commandType: CommandType.StoredProcedure).Count();
			}
		}

		public IEnumerable<MessegingApp> GetAll(string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<MessegingApp>("MessegingappGetAll", null, commandType: CommandType.StoredProcedure);
			}
		}

		public IEnumerable<MessegingApp> GetByPk(Int32 trial_id_1, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			var parms = new DynamicParameters();
			parms.Add("Trial_id_1", trial_id_1);
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<MessegingApp>("MessegingappGetByPk", parms, commandType: CommandType.StoredProcedure);
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
		//	str_cmdview=str_cmdview+" from messegingapp";
			
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