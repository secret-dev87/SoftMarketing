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
	
	public class MessageTypesDAL
	{
		
		public MessageTypesDAL(){
		}
		public DynamicParameters FillParams(MessageTypes MessageTypes)
		{
			var parms = new DynamicParameters();
			parms.Add("Trial_messagetypeid_1", MessageTypes.messagetypeid);
			parms.Add("Trial_messagename_2", MessageTypes.messagename);
			parms.Add("Trial_messagetext_3", MessageTypes.messagetext);
			return parms;
		}

		public Int32 Add(MessageTypes MessageTypes, DbTransaction transaction)
		{
			var parms = FillParams(MessageTypes);
			var connection = transaction.Connection;
			var data = connection.Query<MessageTypes>("MessagetypesInsert", new
			{
				parms,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);

			return data.Count();
		}

		public string Update(MessageTypes MessageTypes, DbTransaction transaction)
		{
			var parms = FillParams(MessageTypes);
			var connection = transaction.Connection;
			var data = connection.Query<MessageTypes>("MessagetypesUpdate", new
			{
				parms,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);

			return data.FirstOrDefault().messagename;
		}

		public void Delete(Int32 trial_messagetypeid_1, DbTransaction transaction)
		{

			var parms = new DynamicParameters();
			parms.Add("Trial_messagetypeid_1", trial_messagetypeid_1);
			var connection = transaction.Connection;
			var data = connection.Query<Events>("MessagetypesDelete", new
			{
				parms,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);
		}

		public IEnumerable<MessageTypes> GetPagedData(Int32 pageFirstRow, Int32 pageRowCount, String toPageOn, String toSortOn, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			var parms = new DynamicParameters();
			parms.Add("pageFirstRow", pageFirstRow);
			parms.Add("pageRowCount", pageRowCount);
			parms.Add("toPageOn", toPageOn);
			parms.Add("toSortOn", toSortOn);
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<MessageTypes>("MessagetypesGetPagingData", parms, commandType: CommandType.StoredProcedure);
			}
		}


		//public virtual void DeleteAll(DbTransaction transaction){
		//		Database database =DatabaseFactory.CreateDatabase();
		//		DbCommand command = database.GetStoredProcCommand("MessagetypesDeleteAll");
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

		public IEnumerable<MessageTypes> Get(Int32 trial_messagetypeid_1, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			var parms = new DynamicParameters();
			parms.Add("Trial_messagetypeid_1", trial_messagetypeid_1);
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<MessageTypes>("MessagetypesGet", parms, commandType: CommandType.StoredProcedure);
			}
		}

		public Int32 CountAll(string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<MessageTypes>("MessagetypesCountAll", null, commandType: CommandType.StoredProcedure).Count();
			}
		}

		public IEnumerable<MessageTypes> GetAll(string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<MessageTypes>("MessagetypesGetAll", null, commandType: CommandType.StoredProcedure);
			}
		}

		public IEnumerable<MessageTypes> GetByPk(Int32 trial_messagetypeid_1, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			var parms = new DynamicParameters();
			parms.Add("Trial_messagetypeid_1", trial_messagetypeid_1);
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<MessageTypes>("MessagetypesGetByPk", parms, commandType: CommandType.StoredProcedure);
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
		//	str_cmdview=str_cmdview+" from messagetypes";
			
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