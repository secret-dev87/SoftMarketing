#region using directives

using System.Data;
using System.Data.Common;
using SoftMarketing.Model;
using MySql.Data.MySqlClient;
using Dapper;
using SoftMarketing.DAL.MySQL.Helper;

#endregion

namespace SoftMarketing.DAL
{

    public class Client_MessegingAppsDAL
	{
		
		public Client_MessegingAppsDAL(){
		}

		public Int32 Add(Client_MessegingApps client_MessegingApps, DbTransaction transaction)
		{
			if (transaction == null) throw new ArgumentNullException("trans");

			var parms = new DynamicParameters();
			parms.Add("Id", client_MessegingApps.Id, DbType.Int32, ParameterDirection.Output);
			parms.Add("clientid", client_MessegingApps.ClientId);
			parms.Add("Messegingappid", client_MessegingApps.MessegingAppId);

			var connection = transaction.Connection;
			var data = connection.Query<Client_MessegingApps>("[dbo].[Cc_messegingappsInsert]", new
			{
                parms,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);

			return data.Count();
		}


		public  Int32 Update(Client_MessegingApps client_MessegingApps, DbTransaction transaction)
		{
			var parms = new DynamicParameters();
			parms.Add("Id", client_MessegingApps.Id);
			parms.Add("clientid", client_MessegingApps.ClientId);
			parms.Add("Messegingappid", client_MessegingApps.MessegingAppId);
			var connection = transaction.Connection;
			var data = connection.Query<Client_MessegingApps>("[dbo].[Cc_messegingappsUpdate]", new
			{
               parms,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);

			return data.FirstOrDefault().Id;
		}

		public  void Delete(Int32 id, DbTransaction transaction)
		{
			var parms = new DynamicParameters();
			parms.Add("Id", id);
			var connection = transaction.Connection;
			var data = connection.Query<Client_MessegingApps>("[dbo].[Cc_messegingappsDelete]", new
			{
				parms,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);

		}

		public virtual Client_MessegingApps GetById(Int32 id, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;

			var parms = new DynamicParameters();
			parms.Add("Id", id);

			using (var connection = Database.GetNewConnection(connString, true))
			{
				return (Client_MessegingApps)connection.Query<Client_MessegingApps>("Cc_messegingappsGetByPk", parms, commandType: CommandType.StoredProcedure);
			}
		}
		public IEnumerable<Client_MessegingApps> GetByclientid(Int32 clientId, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;

			var parms = new DynamicParameters();
			parms.Add("clientid", clientId);

			using (var connection = Database.GetNewConnection(connString, true))
			{
				var result = connection.Query<Client_MessegingApps>("[dbo].[Cc_messegingappsGetBy_clientid]", new
				{
					parms,
				}, commandType: CommandType.StoredProcedure);

				return result;
			}
		}
		public IEnumerable<Client_MessegingApps> GetByMessegingappid(Int32 messegingappId, string connectionString = null)
		{
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;

			var parms = new DynamicParameters();
			parms.Add("messegingappid", messegingappId);

			using (var connection = Database.GetNewConnection(connString, true))
			{
				var result = connection.Query<Client_MessegingApps>("[dbo].[Cc_messegingappsGetByMessegingappid]", new
				{
					messegingappid = messegingappId,
				}, commandType: CommandType.StoredProcedure);

				return result;
			}
		}
	}
}