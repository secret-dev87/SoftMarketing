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
	
	public class SubscriptionDAL
	{
		
		public SubscriptionDAL(){
		}
		public DynamicParameters FillParams(Subscription Subscription)
		{
			var parms = new DynamicParameters();
			parms.Add("Subscriptionid", Subscription.subscriptionid);
			parms.Add("Subscriptiontypeid", Subscription.subscriptiontypeid);
			parms.Add("Trial_customerid_3", Subscription.customerid);
			parms.Add("Startdate", Subscription.startdate);
			parms.Add("Trial_enddate_5", Subscription.enddate);
			return parms;
		}

		public Int32 Add(Subscription Subscription, DbTransaction transaction)
		{
			var parms = FillParams(Subscription);
			var connection = transaction.Connection;
			var data = connection.Query<Subscription>("SubscriptionInsert", new
			{
				parms,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);

			return data.Count();
		}

		public Int32 Update(Subscription Subscription, DbTransaction transaction)
		{
			var parms = FillParams(Subscription);
			var connection = transaction.Connection;
			var data = connection.Query<Subscription>("SubscriptionUpdate", new
			{
				parms,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);

			return data.FirstOrDefault().subscriptionid;
		}

		public void Delete(Int32 subscriptionid, DbTransaction transaction)
		{

			var parms = new DynamicParameters();
			parms.Add("Subscriptionid", subscriptionid);
			var connection = transaction.Connection;
			var data = connection.Query<Subscription>("SubscriptionDelete", new
			{
				parms,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);
		}

		public IEnumerable<Subscription> GetPagedData(Int32 pageFirstRow, Int32 pageRowCount, String toPageOn, String toSortOn, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			var parms = new DynamicParameters();
			parms.Add("pageFirstRow", pageFirstRow);
			parms.Add("pageRowCount", pageRowCount);
			parms.Add("toPageOn", toPageOn);
			parms.Add("toSortOn", toSortOn);
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<Subscription>("SubscriptionGetPagingData", parms, commandType: CommandType.StoredProcedure);
			}
		}

		//public virtual void DeleteAll(DbTransaction transaction){
		//		Database database =DatabaseFactory.CreateDatabase();
		//		DbCommand command = database.GetStoredProcCommand("SubscriptionDeleteAll");
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

		public Subscription Get(Int32 subscriptionid, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			var parms = new DynamicParameters();
			parms.Add("Subscriptionid", subscriptionid);
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return (Subscription)connection.Query<Subscription>("SubscriptionGet", parms, commandType: CommandType.StoredProcedure);
			}
		}

		public Int32 CountAll(string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<Subscription>("SubscriptionCountAll", null, commandType: CommandType.StoredProcedure).Count();
			}
		}

		public IEnumerable<Subscription> GetAll(string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<Subscription>("SubscriptionGetAll", null, commandType: CommandType.StoredProcedure);
			}
		}

		public IEnumerable<Subscription> GetByPk(Int32 subscriptionid, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			var parms = new DynamicParameters();
			parms.Add("Subscriptionid", subscriptionid);
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<Subscription>("SubscriptionGetByPk", parms, commandType: CommandType.StoredProcedure);
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
		//	str_cmdview=str_cmdview+" from subscription";
			
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