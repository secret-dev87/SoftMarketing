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
	
	public class User_MessageDAL
	{
		
		public User_MessageDAL(){
		}
		public DynamicParameters FillParams(User_Message user_Message)
		{
			var parms = new DynamicParameters();
			parms.Add("messageid", user_Message.MessageId);
			parms.Add("customer_messagecol", user_Message.ImageURL);
			parms.Add("custommessage", user_Message.CustomMessage);
			parms.Add("cmid", user_Message.UMId);
			parms.Add("customerid", user_Message.UserID);
			return parms;
		}

		public Int32 Add(User_Message Customer_Message, DbTransaction transaction)
		{
			var parms = FillParams(Customer_Message);
			var connection = transaction.Connection;
			var data = connection.Query<Client>("User_messageInsert", new
			{
				parms,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);

			return data.Count();
		}
		public Int32 Update(User_Message user_Message, DbTransaction transaction)
		{
			var parms = FillParams(user_Message);
			var connection = transaction.Connection;
			var data = connection.Query<User_Message>("User_messageUpdate", new
			{
				parms,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);

			return data.FirstOrDefault().UMId;
		}
	
		public void Delete(Int32 cmid, DbTransaction transaction){

			var parms = new DynamicParameters();
			parms.Add("Cmid", cmid);
			var connection = transaction.Connection;
			var data = connection.Query<User_Message>("User_messageDelete", new
			{
				parms,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);
		}
			
		public IEnumerable<User_Message> GetPagedData(Int32 pageFirstRow, Int32 pageRowCount, String toPageOn, String toSortOn, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			var parms = new DynamicParameters();
			parms.Add("pageFirstRow", pageFirstRow);
			parms.Add("pageRowCount", pageRowCount);
			parms.Add("toPageOn", toPageOn);
			parms.Add("toSortOn", toSortOn);
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<User_Message>("User_messageGetPagingData", parms, commandType: CommandType.StoredProcedure);
			}
		}
		public User_Message Get(Int32 umId, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			var parms = new DynamicParameters();
			parms.Add("Cmid", umId);
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return (User_Message)connection.Query<User_Message>("User_messageGet", parms, commandType: CommandType.StoredProcedure);
			}
		}
		
		public IEnumerable<User_Message> GetAll(string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<User_Message>("User_messageGetAll", null, commandType: CommandType.StoredProcedure);
			}
		}

        public Int32 CountAll(string connectionString = null)
        {
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<User_Message>("User_messageCountAll", null, commandType: CommandType.StoredProcedure).Count();
			}
        }

        public IEnumerable<User_Message> GetByPk(Int32 umId, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			var parms = new DynamicParameters();
			parms.Add("Cmid", umId);
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<User_Message>("User_messageGetByPk", parms, commandType: CommandType.StoredProcedure);
			}
		}
		public IEnumerable<User_Message> GetUserID(Int32 userID, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			var parms = new DynamicParameters();
			parms.Add("UserID", userID);
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<User_Message>("User_messageGetByUserID", parms, commandType: CommandType.StoredProcedure);
			}
		}
		public IEnumerable<User_Message> GetByMessageId(Int32 messageId, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
			var parms = new DynamicParameters();
			parms.Add("MessageId", messageId);
			using (var connection = Database.GetNewConnection(connString, true))
			{
				return connection.Query<User_Message>("User_messageGetByMessageid", parms, commandType: CommandType.StoredProcedure);
			}
		}
	}
}