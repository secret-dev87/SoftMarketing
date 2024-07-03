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
	
	public class SubscriptionTypeDAL
	{
		
		public SubscriptionTypeDAL(){
		}

		public DynamicParameters FillParams(SubscriptionType SubscriptionType)
		{
			var parms = new DynamicParameters();
			parms.Add("Subscriptiontypeid", SubscriptionType.subscriptiontypeid);
			parms.Add("Name", SubscriptionType.name);
			parms.Add("Duration", SubscriptionType.duration);
			return parms;
		}

		public Int32 Add(SubscriptionType SubscriptionType, DbTransaction transaction)
		{
			var parms = FillParams(SubscriptionType);
			var connection = transaction.Connection;
			var data = connection.Query<SubscriptionType>("SubscriptiontypeInsert", new
			{
				parms,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);

			return data.Count();
		}

		public Int32 Update(SubscriptionType SubscriptionType, DbTransaction transaction)
		{
			var parms = FillParams(SubscriptionType);
			var connection = transaction.Connection;
			var data = connection.Query<SubscriptionType>("SubscriptiontypeUpdate", new
			{
				parms,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);

			return data.FirstOrDefault().subscriptiontypeid;
		}

		public void Delete(Int32 subscriptiontypeid, DbTransaction transaction)
		{

			var parms = new DynamicParameters();
			parms.Add("Subscriptiontypeid", subscriptiontypeid);
			var connection = transaction.Connection;
			var data = connection.Query<SubscriptionType>("SubscriptiontypeDelete", new
			{
				parms,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);
		}

		public IEnumerable<SubscriptionType> GetPagedData(Int32 pageFirstRow, Int32 pageRowCount, String toPageOn, String toSortOn, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			var parms = new DynamicParameters();
			parms.Add("pageFirstRow", pageFirstRow);
			parms.Add("pageRowCount", pageRowCount);
			parms.Add("toPageOn", toPageOn);
			parms.Add("toSortOn", toSortOn);
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<SubscriptionType>("SubscriptiontypeGetPagingData", parms, commandType: CommandType.StoredProcedure);
			}
		}
		//public virtual void DeleteAll(DbTransaction transaction){
		//		Database database =DatabaseFactory.CreateDatabase();
		//		DbCommand command = database.GetStoredProcCommand("SubscriptiontypeDeleteAll");
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

		public SubscriptionType Get(Int32 subscriptiontypeid, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			var parms = new DynamicParameters();
			parms.Add("Subscriptiontypeid", subscriptiontypeid);
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return (SubscriptionType)connection.Query<SubscriptionType>("SubscriptiontypeGet", parms, commandType: CommandType.StoredProcedure);
			}
		}


		public Int32 CountAll(string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<SubscriptionType>("SubscriptiontypeCountAll", null, commandType: CommandType.StoredProcedure).Count();
			}
		}

		public IEnumerable<SubscriptionType> GetAll(string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<SubscriptionType>("SubscriptiontypeGetAll", null, commandType: CommandType.StoredProcedure);
			}
		}

		public IEnumerable<SubscriptionType> GetByPk(Int32 eventid, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			var parms = new DynamicParameters();
			parms.Add("Eventid", eventid);
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<SubscriptionType>("SubscriptiontypeGetByPk", parms, commandType: CommandType.StoredProcedure);
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
		//	str_cmdview=str_cmdview+" from subscriptiontype";
			
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