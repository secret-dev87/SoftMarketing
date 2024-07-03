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
	
	public class ClientMessageHistoryDAL
	{
		
		public ClientMessageHistoryDAL(){
		}

		public virtual Int32 Add(ClientMessageHistory clientMessageHistory, DbTransaction transaction = null) 
		{
			
			if (transaction == null) throw new ArgumentNullException("trans");

			var parms = new DynamicParameters();
			//parms.Add("@Messagehistoryid", DbType.Int32, 4);
			parms.Add("messageid", clientMessageHistory.messageid);
			parms.Add("clientid", clientMessageHistory.clientid);
			parms.Add("Sentdateselected", clientMessageHistory.sentdateselected);
			parms.Add("Attemptsnos", clientMessageHistory.attemptsnos);
			parms.Add("Issuccess", clientMessageHistory.issuccess);
			parms.Add("smspackagecount", clientMessageHistory.smspackagecount);
			parms.Add("Remainingsms", clientMessageHistory.remainingsms);
			parms.Add("lastsentdate", clientMessageHistory.lastsentdate);

			var connection = transaction.Connection;
			var data = connection.Query<ClientMessageHistory>("[dbo].[ClientmessagehistoryInsert]", new
			{
				messageId = clientMessageHistory.messageid,
                clientMessageHistory.clientid,
                clientMessageHistory.sentdateselected,
                clientMessageHistory.attemptsnos,
                clientMessageHistory.issuccess,
                clientMessageHistory.smspackagecount,
                clientMessageHistory.remainingsms,
                clientMessageHistory.lastsentdate,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);

			return data.Count();
		}


		public virtual Int32 Update(ClientMessageHistory clientMessageHistory, DbTransaction transaction)
		{
			var parms = new DynamicParameters();
			parms.Add("Messagehistoryid", clientMessageHistory.messagehistoryid);
			parms.Add("messageid", clientMessageHistory.messageid);
			parms.Add("clientid", clientMessageHistory.clientid);
			parms.Add("Sentdateselected", clientMessageHistory.sentdateselected);
			parms.Add("Attemptsnos", clientMessageHistory.attemptsnos);
			parms.Add("Issuccess", clientMessageHistory.issuccess);
			parms.Add("smspackagecount", clientMessageHistory.smspackagecount);
			parms.Add("Remainingsms", clientMessageHistory.remainingsms);
			parms.Add("lastsentdate", clientMessageHistory.lastsentdate);

			var connection = transaction.Connection;
			var data = connection.Query<Client_MessegingApps>("[dbo].[ClientmessagehistoryUpdate]", new
			{
				clientMessageHistory.messagehistoryid,
				clientMessageHistory.messageid,
				clientMessageHistory.clientid,
				clientMessageHistory.sentdateselected,
				clientMessageHistory.attemptsnos,
				clientMessageHistory.issuccess,
				clientMessageHistory.smspackagecount,
				clientMessageHistory.remainingsms,
				clientMessageHistory.lastsentdate,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);

			return data.FirstOrDefault().Id;
		}
	
		public virtual void Delete(Int32 messagehistoryid, DbTransaction transaction)
		{
			var parms = new DynamicParameters();
			parms.Add("Messagehistoryid", messagehistoryid);
			var connection = transaction.Connection;
			var data = connection.Query<Client_MessegingApps>("[dbo].[ClientmessagehistoryDelete]", new
			{
				messagehistoryid,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);
		}

		public virtual ClientMessageHistory GetByID(Int32 messagehistoryid, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;

			var parms = new DynamicParameters();
			parms.Add("Messagehistoryid", messagehistoryid);

			using (var connection = Database.GetNewConnection(connString, true))
			{
				return (ClientMessageHistory)connection.Query<ClientMessageHistory>("ClientmessagehistoryGetByPk", parms, commandType: CommandType.StoredProcedure);
			}
		}

		public virtual IEnumerable<ClientMessageHistory> GetByclientid(Int32 clientid, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;

			var parms = new DynamicParameters();
			parms.Add("clientid", clientid);

			using (var connection = Database.GetNewConnection(connString, true))
			{
				var result = connection.Query<ClientMessageHistory>("[dbo].[ClientmessagehistoryGetBy_clientid]", new
				{
					clientid,
				}, commandType: CommandType.StoredProcedure);

				return result;
			}
		}
		public virtual IEnumerable<ClientMessageHistory> GetByMessageid(Int32 messageid, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;

			var parms = new DynamicParameters();
			parms.Add("messageid", messageid);

			using (var connection = Database.GetNewConnection(connString, true))
			{
				var result = connection.Query<ClientMessageHistory>("[dbo].[ClientmessagehistoryGetBy_messageid]", new
				{
					messageid,
				}, commandType: CommandType.StoredProcedure);

				return result;
			}
		}
	}
}