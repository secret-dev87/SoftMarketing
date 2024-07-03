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
	
	public class Global_SettingsDAL
	{
		
		public Global_SettingsDAL(){
		}
		public DynamicParameters FillParams(Global_Settings settings)
		{
			var parms = new DynamicParameters();
			parms.Add("Name", settings.name);
			parms.Add("Value", settings.value);
			return parms;
		}

		public Int32 Add(Global_Settings setting, DbTransaction transaction)
		{
			var parms = FillParams(setting);
			var connection = transaction.Connection;
			var data = connection.Query<Global_Settings>("Global_settingsInsert", new
			{
				parms,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);

			return data.Count();
		}

		public string Update(Global_Settings setting, DbTransaction transaction)
		{
			var parms = FillParams(setting);
			var connection = transaction.Connection;
			var data = connection.Query<Global_Settings>("Global_settingsUpdate", new
			{
				parms,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);

			return data.FirstOrDefault().value;
		}

		public void Delete(Int32 name, DbTransaction transaction)
		{

			var parms = new DynamicParameters();
			parms.Add("Name", name);
			var connection = transaction.Connection;
			var data = connection.Query<Global_Settings>("Global_settingsDelete", new
			{
				parms,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);
		}

		public IEnumerable<Global_Settings> GetPagedData(Int32 pageFirstRow, Int32 pageRowCount, String toPageOn, String toSortOn, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			var parms = new DynamicParameters();
			parms.Add("pageFirstRow", pageFirstRow);
			parms.Add("pageRowCount", pageRowCount);
			parms.Add("toPageOn", toPageOn);
			parms.Add("toSortOn", toSortOn);
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<Global_Settings>("Global_settingsGetPagingData", parms, commandType: CommandType.StoredProcedure);
			}
		}

		//public virtual void DeleteAll(DbTransaction transaction){
		//		Database database =DatabaseFactory.CreateDatabase();
		//		DbCommand command = database.GetStoredProcCommand("Global_settingsDeleteAll");
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

		public Global_Settings Get(Int32 name, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			var parms = new DynamicParameters();
			parms.Add("Name", name);
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return (Global_Settings)connection.Query<Global_Settings>("Global_settingsGet", parms, commandType: CommandType.StoredProcedure);
			}
		}

		public Int32 CountAll(string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<Global_Settings>("Global_settingsCountAll", null, commandType: CommandType.StoredProcedure).Count();
			}
		}

		public IEnumerable<Global_Settings> GetAll(string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<Global_Settings>("Global_settingsGetAll", null, commandType: CommandType.StoredProcedure);
			}
		}

		public IEnumerable<Global_Settings> GetByPk(Int32 name, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			var parms = new DynamicParameters();
			parms.Add("Name", name);
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<Global_Settings>("Global_settingsGetByPk", parms, commandType: CommandType.StoredProcedure);
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
		//	str_cmdview=str_cmdview+" from global_settings";
			
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