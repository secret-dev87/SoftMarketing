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
	
	public class SettingsDAL
	{
		
		public SettingsDAL(){
		}
		public DynamicParameters FillParams(Settings Settings)
		{
			var parms = new DynamicParameters();
			parms.Add("User_country", Settings.user_country);
			parms.Add("Trial_user_phone_2", Settings.user_phone);
			parms.Add("Settings", Settings.settings);
			parms.Add("Value", Settings.value);
			parms.Add("Id", Settings.id);
			return parms;
		}

		public Int32 Add(Settings Settings, DbTransaction transaction)
		{
			var parms = FillParams(Settings);
			var connection = transaction.Connection;
			var data = connection.Query<Settings>("SettingsInsert", new
			{
				parms,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);

			return data.Count();
		}

		public Int32 Update(Settings Settings, DbTransaction transaction)
		{
			var parms = FillParams(Settings);
			var connection = transaction.Connection;
			var data = connection.Query<Settings>("SettingsUpdate", new
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
			var data = connection.Query<Settings>("SettingsDelete", new
			{
				parms,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);
		}


		public IEnumerable<Settings> GetPagedData(Int32 pageFirstRow, Int32 pageRowCount, String toPageOn, String toSortOn, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			var parms = new DynamicParameters();
			parms.Add("pageFirstRow", pageFirstRow);
			parms.Add("pageRowCount", pageRowCount);
			parms.Add("toPageOn", toPageOn);
			parms.Add("toSortOn", toSortOn);
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<Settings>("SettingsGetPagingData", parms, commandType: CommandType.StoredProcedure);
			}
		}

		//public virtual void DeleteAll(DbTransaction transaction){
		//		Database database =DatabaseFactory.CreateDatabase();
		//		DbCommand command = database.GetStoredProcCommand("SettingsDeleteAll");
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

		public Settings Get(Int32 id, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			var parms = new DynamicParameters();
			parms.Add("Id", id);
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return (Settings)connection.Query<Settings>("SettingsGet", parms, commandType: CommandType.StoredProcedure);
			}
		}

		public Int32 CountAll(string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<Settings>("SettingsCountAll", null, commandType: CommandType.StoredProcedure).Count();
			}
		}

		public IEnumerable<Settings> GetAll(string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<Settings>("SettingsGetAll", null, commandType: CommandType.StoredProcedure);
			}
		}

		public IEnumerable<Settings> GetByPk(Int32 id, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			var parms = new DynamicParameters();
			parms.Add("Id", id);
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<Settings>("SettingsGetByPk", parms, commandType: CommandType.StoredProcedure);
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
		//	str_cmdview=str_cmdview+" from settings";
			
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