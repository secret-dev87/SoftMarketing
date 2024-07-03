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
	
	public class CountryEventsDAL
	{
		
		public CountryEventsDAL(){
		}

		public Int32 Add(CountryEvents countryEvents, DbTransaction transaction)
		{
			if (transaction == null) throw new ArgumentNullException("trans");

			var parms = new DynamicParameters();
			parms.Add("countryid", countryEvents.countryid);
			parms.Add("Eventid", countryEvents.eventid);
			parms.Add("Customdate", countryEvents.customdate);

			var connection = transaction.Connection;
			var data = connection.Query<CountryEvents>("[dbo].[CountryeventsInsert]", new
			{
				countryEvents.countryid,
				countryEvents.eventid,
				countryEvents.customdate,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);

			return data.Count();
		}


		public Int32 Update(CountryEvents countryEvents, DbTransaction transaction)
		{
			var parms = new DynamicParameters();
			parms.Add("CountryEventID", countryEvents.countryeventid);
			parms.Add("countryid", countryEvents.countryid);
			parms.Add("Eventid", countryEvents.eventid);
			parms.Add("Customdate", countryEvents.customdate);
			var connection = transaction.Connection;
			var data = connection.Query<CountryEvents>("[dbo].[CountryeventsUpdate]", new
			{
				countryEvents.countryeventid,
				countryEvents.countryid,
				countryEvents.eventid,
				countryEvents.customdate,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);

			return data.FirstOrDefault().countryeventid;

		}
	
		public void Delete(Int32 countryeventid, DbTransaction transaction){
		
			var parms = new DynamicParameters();
			parms.Add("countryeventid", countryeventid);
			var connection = transaction.Connection;
			var data = connection.Query<CountryEvents>("[dbo].[CountryeventsDelete]", new
			{
                countryeventid,
			}, transaction: transaction, commandType: CommandType.StoredProcedure);
		}

		public CountryEvents GetByID(Int32 countryeventid ,string connectionString = null){
			var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;

			var parms = new DynamicParameters();
			parms.Add("countryeventid", countryeventid);

			using (var connection = Database.GetNewConnection(connString, true))
			{
				return (CountryEvents)connection.Query<CountryEvents>("CountryEventGetByPk", parms, commandType: CommandType.StoredProcedure);
			}
		}
		
	}
}