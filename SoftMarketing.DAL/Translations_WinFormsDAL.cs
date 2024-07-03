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
	
	public class Translations_WinFormsDAL
	{
		
		public Translations_WinFormsDAL(){
		}
		public DynamicParameters FillParams(Translation Translation)
		{
			var parms = new DynamicParameters();
			parms.Add("English", Translation.English);
			parms.Add("Trial_french_2", Translation.Trial_french_2);
			parms.Add("German", Translation.German);
			return parms;
		}

		public Int32 Add(Translation Translation, DbTransaction transaction)
		{
			var parms = FillParams(Translation);
			var connection = transaction.Connection;
			var data = connection.Query<Translation>("Translations_winformsInsert", new
			{
				parms,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);

			return data.Count();
		}

		public string Update(Translation Translation, DbTransaction transaction)
		{
			var parms = FillParams(Translation);
			var connection = transaction.Connection;
			var data = connection.Query<Translation>("Translations_winformsUpdate", new
			{
				parms,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);

			return data.FirstOrDefault().English;
		}

		public void Delete(string english, DbTransaction transaction)
		{

			var parms = new DynamicParameters();
			parms.Add("English", english);
			var connection = transaction.Connection;
			var data = connection.Query<Translation>("Translations_winformsDelete", new
			{
				parms,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);
		}

		public IEnumerable<Translation> GetPagedData(Int32 pageFirstRow, Int32 pageRowCount, String toPageOn, String toSortOn, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			var parms = new DynamicParameters();
			parms.Add("pageFirstRow", pageFirstRow);
			parms.Add("pageRowCount", pageRowCount);
			parms.Add("toPageOn", toPageOn);
			parms.Add("toSortOn", toSortOn);
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<Translation>("Translations_winformsGetPagingData", parms, commandType: CommandType.StoredProcedure);
			}
		}

		//public virtual void DeleteAll(DbTransaction transaction){
		//		Database database =DatabaseFactory.CreateDatabase();
		//		DbCommand command = database.GetStoredProcCommand("Translations_winformsDeleteAll");
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

		public Translation Get(String english, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			var parms = new DynamicParameters();
			parms.Add("English", english);
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return (Translation)connection.Query<Translation>("Translations_winformsGet", parms, commandType: CommandType.StoredProcedure);
			}
		}

		public Int32 CountAll(string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<Translation>("Translations_winformsCountAll", null, commandType: CommandType.StoredProcedure).Count();
			}
		}

		public IEnumerable<Translation> GetAll(string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<Translation>("Translations_winformsGetAll", null, commandType: CommandType.StoredProcedure);
			}
		}

		public IEnumerable<Translation> GetByPk(string english, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			var parms = new DynamicParameters();
			parms.Add("English", english);
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<Translation>("Translations_winformsGetByPk", parms, commandType: CommandType.StoredProcedure);
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
		//	str_cmdview=str_cmdview+" from translations_winforms";
			
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