using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.SqlTypes;
using SoftMarketing.DAL.MySQL;
using Dapper;
using SoftMarketing.DAL.Dapper;

namespace SoftMarketing.DAL.MySQL.Helper
{
	/// <summary>
	/// Container for all high-level database related methods.
	/// </summary>
	public sealed class Database
	{
		/// <summary>
		/// Private constructor to prevent object instantiation of this class.
		/// </summary>
		private Database() { }


		/// <summary>
		/// Sets the connection string to be used for all data access preformed by this class.
		/// NOTE: This property can only be set one time to prevent errors in multi-threaded environments.
		/// </summary>
		public static string ConnectionString
		{
			set
			{
				// set SqlHelper's default connection string
				MySqlConnectionHelper.ConnectionString = value;
			}
		}


		/// <summary>
		/// Creates a new SqlConnection based on the default connection string.
		/// The connection object returned must be opened by the caller before use.
		/// </summary>
		/// <returns>A new transation object for user by the caller.</returns>
		public static MySqlConnection GetNewConnection()
		{
			// call our worker overload default the connection open state to closed
			return GetNewConnection(false);
		}

		/// <summary>
		/// Creates a new SqlConnection based on the default connection string and opens it if requested to do so.
		/// </summary>
		/// <param name="openConnection">Flag indicating whether the connection should be returned in an open state.</param>
		/// <returns>A new transation object for user by the caller.</returns>
		public static MySqlConnection GetNewConnection(bool openConnection)
		{
			SqlMapper.ResetTypeHandlers();
			SqlMapper.AddTypeHandler(new TimeSpanTypeHandler());

			var conn = new MySqlConnection(MySqlConnectionHelper.ConnectionString);
			if( openConnection ) conn.Open();
			return conn;
		}

		public static MySqlConnection GetNewConnection(string connectionString, bool openConnection)
		{
			SqlMapper.ResetTypeHandlers();
			var conn = new MySqlConnection(connectionString);

			if (openConnection)
			{
				conn.Open();
			}

			return conn;
		}
	}
}
