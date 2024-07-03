using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Xml;

namespace SoftMarketing.DAL.MySQL.Helper
{
    /// <summary>
    /// This class is use for generaizing connection methods
    /// </summary>
    public static class MySqlConnectionHelper
    {
        private static string s_connectionString = "server=localhost;user id = root; password=root;persistsecurityinfo=True;database=soft_marketing;allowuservariables=True;Convert Zero Datetime=True";

        /// <summary>
        /// Gets or sets the default connection string to use with methods in this class
        /// that do not explicitly specify one.
        /// NOTE: This property can only be set one time to prevent problems in
        /// multi-threaded environments.
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                return MySqlConnectionHelper.s_connectionString;
            }
            set
            {
                if (MySqlConnectionHelper.s_connectionString != null)
                    throw new InvalidOperationException("The default connection string can only be set once.");
                MySqlConnectionHelper.s_connectionString = value;
            }
        }

        public static string MultiTenantScheduledTaskConnectionString

        {
            get => MySqlConnectionHelper.s_connectionString;
            set => s_connectionString = value;
        }

        /// <summary>
        /// Executes a stored procedure (that returns no result set and takes
        /// no parameters) in the database specified by the default connection string.
        /// </summary>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <returns>An int representing the number of rows affected by the command.</returns>
        public static int ExecuteNonQuery(MySqlConnection conn, string spName, CommandType type)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteNonQuery(MySqlConnectionHelper.s_connectionString, CommandType.StoredProcedure, spName, (MySqlParameter[])null);
        }

        public static int ExecuteNonQuery(MySqlConnection conn, string spName, CommandType type, MySqlParameter[]? parameters)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteNonQuery(MySqlConnectionHelper.s_connectionString, CommandType.StoredProcedure, spName, parameters);
        }

        public static int ExecuteNonQuery(MySqlConnection conn, string spName, CommandType type, int timeout, MySqlParameter[] parameters)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteNonQuery(MySqlConnectionHelper.s_connectionString, CommandType.StoredProcedure, spName, timeout, parameters);
        }

        /// <summary>
        /// Executes a stored procedure (that returns no result set and takes
        /// no parameters) in the database specified by the default connection string.
        /// </summary>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <returns>An int representing the number of rows affected by the command.</returns>
        public static int ExecuteNonQuery(string spName, int timeout)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteNonQuery(MySqlConnectionHelper.s_connectionString, CommandType.StoredProcedure, spName, timeout, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure (that returns no result set) in the database specified
        /// by the default connection string, using the provided parameters.
        /// </summary>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>An int representing the number of rows affected by the command.</returns>
        public static int ExecuteNonQuery(string spName, params MySqlParameter[] parameters)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteNonQuery(MySqlConnectionHelper.s_connectionString, CommandType.StoredProcedure, spName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure (that returns no result set) in the database specified
        /// by the default connection string, using the provided parameters.
        /// </summary>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>An int representing the number of rows affected by the command.</returns>
        public static int ExecuteNonQuery(string spName, int timeout, params MySqlParameter[] parameters)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteNonQuery(MySqlConnectionHelper.s_connectionString, CommandType.StoredProcedure, spName, timeout, parameters);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns no result set and takes no parameters) against the database specified by
        /// the default connection string.
        /// </summary>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <returns>An int representing the number of rows affected by the command.</returns>
        public static int ExecuteNonQuery(CommandType type, string text)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteNonQuery(MySqlConnectionHelper.s_connectionString, type, text, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns no result set) against the database specified by the default connection string
        /// using the provided parameters.
        /// </summary>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>An int representing the number of rows affected by the command.</returns>
        public static int ExecuteNonQuery(
          CommandType type,
          string text,
          params MySqlParameter[] parameters)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteNonQuery(MySqlConnectionHelper.s_connectionString, type, text, parameters);
        }

        /// <summary>
        /// Executes a stored procedure (that returns no result set and takes
        /// no parameters) in the database specified by the connection string.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <returns>An int representing the number of rows affected by the command.</returns>
        public static int ExecuteNonQuery(string connectionString, string spName)
        {
            return MySqlConnectionHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure (that returns no result set and takes
        /// no parameters) in the database specified by the connection string.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <returns>An int representing the number of rows affected by the command.</returns>
        public static int ExecuteNonQuery(string connectionString, string spName, int timeout)
        {
            return MySqlConnectionHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, timeout, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure (that returns no result set) in the database specified
        /// by the connection string, using the provided parameters.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>An int representing the number of rows affected by the command.</returns>
        public static int ExecuteNonQuery(
          string connectionString,
          string spName,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure (that returns no result set) in the database specified
        /// by the connection string, using the provided parameters.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>An int representing the number of rows affected by the command.</returns>
        public static int ExecuteNonQuery(
          string connectionString,
          string spName,
          int timeout,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, timeout, parameters);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns no result set and takes no parameters) against the database specified by
        /// the connection string.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <returns>An int representing the number of rows affected by the command.</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType type, string text, MySqlParameter[]? mySqlParameterCollections)
        {
            return MySqlConnectionHelper.ExecuteNonQuery(connectionString, type, text, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns no result set) against the database specified by the connection string
        /// using the provided parameters.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>An int representing the number of rows affected by the command.</returns>
        public static int ExecuteNonQuery(
          string connectionString,
          CommandType type,
          string text,
          MySqlParameter[]? mysqlps,
          params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                return MySqlConnectionHelper.ExecuteNonQuery(conn, text, type, parameters);
            }
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns no result set) against the database specified by the connection string
        /// using the provided parameters.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>An int representing the number of rows affected by the command.</returns>
        public static int ExecuteNonQuery(
          string connectionString,
          CommandType type,
          string text,
          int timeout,
          params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                return MySqlConnectionHelper.ExecuteNonQuery(conn, text, type, timeout, parameters);
            }
        }

        /// <summary>
        /// Executes a stored procedure (that returns no result set) against the specified MySqlConnection.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(MySqlConnection conn, string spName)
        {
            return MySqlConnectionHelper.ExecuteNonQuery(conn,spName, CommandType.StoredProcedure, null);
        }

        /// <summary>
        /// Executes a stored procedure (that returns no result set) against the specified MySqlConnection.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(MySqlConnection conn, string spName, int timeout)
        {
            return MySqlConnectionHelper.ExecuteNonQuery(conn, spName, CommandType.StoredProcedure, timeout, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure (that returns no result set) against the specified MySqlConnection
        /// using the provided parameters.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(
          MySqlConnection conn,
          string spName,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteNonQuery(conn, spName, CommandType.StoredProcedure, parameters);
        }

        /// <summary>
        /// Executes a stored procedure (that returns no result set) against the specified MySqlConnection
        /// using the provided parameters.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(
          MySqlConnection conn,
          string spName,
          int timeout,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteNonQuery(conn, spName, CommandType.StoredProcedure, timeout, parameters);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns no result set and takes no parameters) against the provided MySqlConnection.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <returns>An int representing the number of rows affected by the command.</returns>
        public static int ExecuteNonQuery(MySqlConnection conn1, MySqlConnection conn, CommandType type, string text)
        {
            return MySqlConnectionHelper.ExecuteNonQuery(conn, text, type, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns no result set) against the specified MySqlConnection
        /// using the provided parameters.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>An int representing the number of rows affected by the command.</returns>
        public static int ExecuteNonQuery(
          MySqlConnection conn1,
          MySqlConnection conn,
          CommandType type,
          string text,
          params MySqlParameter[] parameters)
        {
            MySqlCommand command = new MySqlCommand();
            MySqlConnectionHelper.PrepareCommand(command, conn, (MySqlTransaction)null, type, text, parameters);
            try
            {
                return command.ExecuteNonQuery();
            }
            finally
            {
                command.Parameters.Clear();
            }
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns no result set) against the specified MySqlConnection
        /// using the provided parameters.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>An int representing the number of rows affected by the command.</returns>
        public static int ExecuteNonQuery(
          MySqlConnection conn,
          CommandType type,
          string text,
          int timeout,
          params MySqlParameter[] parameters)
        {
            MySqlCommand command = new MySqlCommand();
            MySqlConnectionHelper.PrepareCommand(command, conn, (MySqlTransaction)null, type, text, parameters, timeout);
            try
            {
                return command.ExecuteNonQuery();
            }
            finally
            {
                command.Parameters.Clear();
            }
        }

        /// <summary>
        /// Executes a stored procedure (that returns no result set) against the specified MySqlCommand.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(MySqlTransaction trans, string spName)
        {
            return MySqlConnectionHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, spName, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure (that returns no result set) against the specified MySqlTransaction.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(MySqlTransaction trans, string spName, int timeout)
        {
            return MySqlConnectionHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, spName, timeout, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure (that returns no result set) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(
          MySqlTransaction trans,
          string spName,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, spName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure (that returns no result set) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>An int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(
          MySqlTransaction trans,
          string spName,
          int timeout,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, spName, timeout, parameters);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns no result set and takes no parameters) against the provided MySqlTransaction.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <returns>An int representing the number of rows affected by the command.</returns>
        public static int ExecuteNonQuery(MySqlTransaction trans, CommandType type, string text)
        {
            return MySqlConnectionHelper.ExecuteNonQuery(trans, type, text, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns no result set) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="parameters">An array of SqlParameters used to execute the command.</param>
        /// <returns>An int representing the number of rows affected by the command.</returns>
        public static int ExecuteNonQuery(
          MySqlTransaction trans,
          CommandType type,
          string text,
          params MySqlParameter[] parameters)
        {
            MySqlCommand command = new MySqlCommand();
            MySqlConnectionHelper.PrepareCommand(command, trans.Connection, trans, type, text, parameters);
            try
            {
                return command.ExecuteNonQuery();
            }
            finally
            {
                command.Parameters.Clear();
            }
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns no result set) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">An array of SqlParameters used to execute the command.</param>
        /// <returns>An int representing the number of rows affected by the command.</returns>
        public static int ExecuteNonQuery(
          MySqlTransaction trans,
          CommandType type,
          string text,
          int timeout,
          params MySqlParameter[] parameters)
        {
            MySqlCommand command = new MySqlCommand();
            MySqlConnectionHelper.PrepareCommand(command, trans.Connection, trans, type, text, parameters, timeout);
            try
            {
                return command.ExecuteNonQuery();
            }
            finally
            {
                command.Parameters.Clear();
            }
        }

        /// <summary>
        /// Executes a stored procedure (that returns a result set and takes no parameters) against the against the database
        /// specified in the default connection string.
        /// </summary>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <returns>A DataSet containing the results generated by the stored procedure.</returns>
        public static DataSet ExecuteDataset(string spName)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteDataset(MySqlConnectionHelper.s_connectionString, CommandType.StoredProcedure, spName, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure (that returns a result set and takes no parameters) against the against the database
        /// specified in the default connection string.
        /// </summary>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <returns>A DataSet containing the results generated by the stored procedure.</returns>
        public static DataSet ExecuteDataset(string spName, int timeout)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteDataset(MySqlConnectionHelper.s_connectionString, CommandType.StoredProcedure, spName, timeout, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure (that returns a result set) against the against the database
        /// specified in the default connection string using the provided parameters.
        /// </summary>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A DataSet containing the results generated by the stored procedure.</returns>
        public static DataSet ExecuteDataset(string spName, params MySqlParameter[] parameters)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteDataset(MySqlConnectionHelper.s_connectionString, CommandType.StoredProcedure, spName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure (that returns a result set) against the against the database
        /// specified in the default connection string using the provided parameters.
        /// </summary>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A DataSet containing the results generated by the stored procedure.</returns>
        public static DataSet ExecuteDataset(
          string spName,
          int timeout,
          params MySqlParameter[] parameters)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteDataset(MySqlConnectionHelper.s_connectionString, CommandType.StoredProcedure, spName, timeout, parameters);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set and takes no parameters) against the database specified in
        /// the default connection string.
        /// </summary>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <returns>A DataSet containing the results generated by the command.</returns>
        public static DataSet ExecuteDataset(CommandType type, string text)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteDataset(MySqlConnectionHelper.s_connectionString, type, text, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set and takes no parameters) against the database specified in
        /// the default connection string using the provided parameters.
        /// </summary>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A DataSet containing the results generated by the command.</returns>
        public static DataSet ExecuteDataset(
          CommandType type,
          string text,
          params MySqlParameter[] parameters)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteDataset(MySqlConnectionHelper.s_connectionString, type, text, parameters);
        }

        /// <summary>
        /// Executes a stored procedure (that returns a result set and takes no parameters) against the against the database
        /// specified in the connection string.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <returns>A DataSet containing the results generated by the stored procedure.</returns>
        public static DataSet ExecuteDataset(string connectionString, string spName)
        {
            return MySqlConnectionHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure (that returns a result set and takes no parameters) against the against the database
        /// specified in the connection string.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <returns>A DataSet containing the results generated by the stored procedure.</returns>
        public static DataSet ExecuteDataset(
          string connectionString,
          string spName,
          int timeout)
        {
            return MySqlConnectionHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, timeout, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure (that returns a result set) against the against the database
        /// specified in the connection string using the provided parameters.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A DataSet containing the results generated by the stored procedure.</returns>
        public static DataSet ExecuteDataset(
          string connectionString,
          string spName,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure (that returns a result set) against the against the database
        /// specified in the connection string using the provided parameters.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A DataSet containing the results generated by the stored procedure.</returns>
        public static DataSet ExecuteDataset(
          string connectionString,
          string spName,
          int timeout,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, timeout, parameters);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set and takes no parameters) against the database specified in
        /// the connection string.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <returns>A DataSet containing the results generated by the command.</returns>
        public static DataSet ExecuteDataset(
          string connectionString,
          CommandType type,
          string text)
        {
            return MySqlConnectionHelper.ExecuteDataset(connectionString, type, text, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set) against the database specified in the connection string
        /// using the provided parameters.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A DataSet containing the results generated by the command.</returns>
        public static DataSet ExecuteDataset(
          string connectionString,
          CommandType type,
          string text,
          params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                return MySqlConnectionHelper.ExecuteDataset(conn, type, text, parameters);
            }
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set) against the database specified in the connection string
        /// using the provided parameters.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A DataSet containing the results generated by the command.</returns>
        public static DataSet ExecuteDataset(
          string connectionString,
          CommandType type,
          string text,
          int timeout,
          params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                return MySqlConnectionHelper.ExecuteDataset(conn, type, text, timeout, parameters);
            }
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set) against the specified MySqlConnection.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <returns>A DataSet containing the results generated by the stored procedure.</returns>
        public static DataSet ExecuteDataset(MySqlConnection conn, string spName)
        {
            return MySqlConnectionHelper.ExecuteDataset(conn, CommandType.StoredProcedure, spName, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set) against the specified MySqlConnection.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <returns>A DataSet containing the results generated by the stored procedure.</returns>
        public static DataSet ExecuteDataset(MySqlConnection conn, string spName, int timeout)
        {
            return MySqlConnectionHelper.ExecuteDataset(conn, CommandType.StoredProcedure, spName, timeout, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set) against the specified MySqlConnection
        /// using the provided parameters.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A DataSet containing the results generated by the stored procedure.</returns>
        public static DataSet ExecuteDataset(
          MySqlConnection conn,
          string spName,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteDataset(conn, CommandType.StoredProcedure, spName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set) against the specified MySqlConnection
        /// using the provided parameters.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A DataSet containing the results generated by the stored procedure.</returns>
        public static DataSet ExecuteDataset(
          MySqlConnection conn,
          string spName,
          int timeout,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteDataset(conn, CommandType.StoredProcedure, spName, timeout, parameters);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set and takes no parameters) against the provided MySqlConnection.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <returns>A DataSet containing the results generated by the command.</returns>
        public static DataSet ExecuteDataset(MySqlConnection conn, CommandType type, string text)
        {
            return MySqlConnectionHelper.ExecuteDataset(conn, type, text, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set) against the specified MySqlConnection
        /// using the provided parameters.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A DataSet containing the results generated by the command.</returns>
        public static DataSet ExecuteDataset(
          MySqlConnection conn,
          CommandType type,
          string text,
          params MySqlParameter[] parameters)
        {
            MySqlCommand MySqlCommand = new MySqlCommand();
            MySqlConnectionHelper.PrepareCommand(MySqlCommand, conn, (MySqlTransaction)null, type, text, parameters);
            MySqlDataAdapter MySqlDataAdapter = new MySqlDataAdapter(MySqlCommand);
            DataSet dataSet = new DataSet();
            try
            {
                MySqlDataAdapter.Fill(dataSet);
            }
            finally
            {
                MySqlCommand.Parameters.Clear();
            }
            return dataSet;
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set) against the specified MySqlConnection
        /// using the provided parameters.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A DataSet containing the results generated by the command.</returns>
        public static DataSet ExecuteDataset(
          MySqlConnection conn,
          CommandType type,
          string text,
          int timeout,
          params MySqlParameter[] parameters)
        {
            MySqlCommand MySqlCommand = new MySqlCommand();
            MySqlConnectionHelper.PrepareCommand(MySqlCommand, conn, (MySqlTransaction)null, type, text, parameters, timeout);
            MySqlDataAdapter MySqlDataAdapter = new MySqlDataAdapter(MySqlCommand);
            DataSet dataSet = new DataSet();
            try
            {
                MySqlDataAdapter.Fill(dataSet);
            }
            finally
            {
                MySqlCommand.Parameters.Clear();
            }
            return dataSet;
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set) against the specified MySqlTransaction.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <returns>A DataSet containing the results generated by the stored procedure.</returns>
        public static DataSet ExecuteDataset(MySqlTransaction trans, string spName)
        {
            return MySqlConnectionHelper.ExecuteDataset(trans, CommandType.StoredProcedure, spName, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set) against the specified MySqlTransaction.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <returns>A DataSet containing the results generated by the stored procedure.</returns>
        public static DataSet ExecuteDataset(MySqlTransaction trans, string spName, int timeout)
        {
            return MySqlConnectionHelper.ExecuteDataset(trans, CommandType.StoredProcedure, spName, timeout, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A DataSet containing the results generated by the stored procedure.</returns>
        public static DataSet ExecuteDataset(
          MySqlTransaction trans,
          string spName,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteDataset(trans, CommandType.StoredProcedure, spName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A DataSet containing the results generated by the stored procedure.</returns>
        public static DataSet ExecuteDataset(
          MySqlTransaction trans,
          string spName,
          int timeout,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteDataset(trans, CommandType.StoredProcedure, spName, timeout, parameters);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set and takes no parameters) against the provided MySqlTransaction.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <returns>A DataSet containing the results generated by the command.</returns>
        public static DataSet ExecuteDataset(
          MySqlTransaction trans,
          CommandType type,
          string text)
        {
            return MySqlConnectionHelper.ExecuteDataset(trans, type, text, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A DataSet containing the results generated by the command.</returns>
        public static DataSet ExecuteDataset(
          MySqlTransaction trans,
          CommandType type,
          string text,
          params MySqlParameter[] parameters)
        {
            MySqlCommand MySqlCommand = new MySqlCommand();
            MySqlConnectionHelper.PrepareCommand(MySqlCommand, trans.Connection, trans, type, text, parameters);
            MySqlDataAdapter MySqlDataAdapter = new MySqlDataAdapter(MySqlCommand);
            DataSet dataSet = new DataSet();
            try
            {
                MySqlDataAdapter.Fill(dataSet);
            }
            finally
            {
                MySqlCommand.Parameters.Clear();
            }
            return dataSet;
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A DataSet containing the results generated by the command.</returns>
        public static DataSet ExecuteDataset(
          MySqlTransaction trans,
          CommandType type,
          string text,
          int timeout,
          params MySqlParameter[] parameters)
        {
            MySqlCommand MySqlCommand = new MySqlCommand();
            MySqlConnectionHelper.PrepareCommand(MySqlCommand, trans.Connection, trans, type, text, parameters, timeout);
            MySqlDataAdapter MySqlDataAdapter = new MySqlDataAdapter(MySqlCommand);
            DataSet dataSet = new DataSet();
            try
            {
                MySqlDataAdapter.Fill(dataSet);
            }
            finally
            {
                MySqlCommand.Parameters.Clear();
            }
            return dataSet;
        }

        /// <summary>
        /// Executes a stored procedure (that returns a result set and takes no parameters) against the against the database
        /// specified in the default connection string.
        /// </summary>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <returns>A DataTable containing the results generated by the stored procedure.</returns>
        public static DataTable ExecuteDataTable(string spName)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteDataTable(MySqlConnectionHelper.s_connectionString, CommandType.StoredProcedure, spName, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure (that returns a result set and takes no parameters) against the against the database
        /// specified in the default connection string.
        /// </summary>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <returns>A DataTable containing the results generated by the stored procedure.</returns>
        public static DataTable ExecuteDataTable(string spName, int timeout)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteDataTable(MySqlConnectionHelper.s_connectionString, CommandType.StoredProcedure, spName, timeout, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure (that returns a result set) against the against the database
        /// specified in the default connection string using the provided parameters.
        /// </summary>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A DataTable containing the results generated by the stored procedure.</returns>
        public static DataTable ExecuteDataTable(
          string spName,
          params MySqlParameter[] parameters)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteDataTable(MySqlConnectionHelper.s_connectionString, CommandType.StoredProcedure, spName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure (that returns a result set) against the against the database
        /// specified in the default connection string using the provided parameters.
        /// </summary>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A DataTable containing the results generated by the stored procedure.</returns>
        public static DataTable ExecuteDataTable(
          string spName,
          int timeout,
          params MySqlParameter[] parameters)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteDataTable(MySqlConnectionHelper.s_connectionString, CommandType.StoredProcedure, spName, timeout, parameters);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set and takes no parameters) against the database specified in
        /// the default connection string.
        /// </summary>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the specified command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <returns>A DataTable containing the results generated by the command.</returns>
        public static DataTable ExecuteDataTable(CommandType type, string text)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteDataTable(MySqlConnectionHelper.s_connectionString, type, text, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set and takes no parameters) against the database specified in
        /// the default connection string using the provided parameters.
        /// </summary>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the specified command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A DataTable containing the results generated by the command.</returns>
        public static DataTable ExecuteDataTable(
          CommandType type,
          string text,
          params MySqlParameter[] parameters)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteDataTable(MySqlConnectionHelper.s_connectionString, type, text, parameters);
        }

        /// <summary>
        /// Executes a stored procedure (that returns a result set and takes no parameters) against the against the database
        /// specified in the connection string.
        /// </summary>
        /// <param name="connectionString">Valid connection string for a MySqlConnection.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <returns>A DataTable containing the results generated by the stored procedure.</returns>
        public static DataTable ExecuteDataTable(string connectionString, string spName)
        {
            return MySqlConnectionHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, spName, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure (that returns a result set and takes no parameters) against the against the database
        /// specified in the connection string.
        /// </summary>
        /// <param name="connectionString">Valid connection string for a MySqlConnection.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <returns>A DataTable containing the results generated by the stored procedure.</returns>
        public static DataTable ExecuteDataTable(
          string connectionString,
          string spName,
          int timeout)
        {
            return MySqlConnectionHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, spName, timeout, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure (that returns a result set) against the against the database
        /// specified in the connection string using the provided parameters.
        /// </summary>
        /// <param name="connectionString">Valid connection string for a MySqlConnection.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A DataTable containing the results generated by the stored procedure.</returns>
        public static DataTable ExecuteDataTable(
          string connectionString,
          string spName,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, spName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure (that returns a result set) against the against the database
        /// specified in the connection string using the provided parameters.
        /// </summary>
        /// <param name="connectionString">Valid connection string for a MySqlConnection.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A DataTable containing the results generated by the stored procedure.</returns>
        public static DataTable ExecuteDataTable(
          string connectionString,
          string spName,
          int timeout,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteDataTable(connectionString, CommandType.StoredProcedure, spName, timeout, parameters);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set and takes no parameters) against the database specified in
        /// the connection string.
        /// </summary>
        /// <param name="connectionString">Valid connection string for a MySqlConnection.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <returns>A DataTable containing the results generated by the command.</returns>
        public static DataTable ExecuteDataTable(
          string connectionString,
          CommandType type,
          string text)
        {
            return MySqlConnectionHelper.ExecuteDataTable(connectionString, type, text, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set) against the database specified in the connection string
        /// using the provided parameters.
        /// </summary>
        /// <param name="connectionString">Valid connection string for a MySqlConnection.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A DataTable containing the results generated by the command.</returns>
        public static DataTable ExecuteDataTable(
          string connectionString,
          CommandType type,
          string text,
          params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                return MySqlConnectionHelper.ExecuteDataTable(conn, type, text, parameters);
            }
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set) against the database specified in the connection string
        /// using the provided parameters.
        /// </summary>
        /// <param name="connectionString">Valid connection string for a MySqlConnection.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A DataTable containing the results generated by the command.</returns>
        public static DataTable ExecuteDataTable(
          string connectionString,
          CommandType type,
          string text,
          int timeout,
          params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                return MySqlConnectionHelper.ExecuteDataTable(conn, type, text, timeout, parameters);
            }
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set) against the specified MySqlConnection.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <returns>A DataTable containing the results generated by the stored procedure.</returns>
        public static DataTable ExecuteDataTable(MySqlConnection conn, string spName)
        {
            return MySqlConnectionHelper.ExecuteDataTable(conn, CommandType.StoredProcedure, spName, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set) against the specified MySqlConnection.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <returns>A DataTable containing the results generated by the stored procedure.</returns>
        public static DataTable ExecuteDataTable(
          MySqlConnection conn,
          string spName,
          int timeout)
        {
            return MySqlConnectionHelper.ExecuteDataTable(conn, CommandType.StoredProcedure, spName, timeout, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set) against the specified MySqlConnection
        /// using the provided parameters.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Array of SqlParameters used to be passed to the stored procedure.</param>
        /// <returns>A DataTable containing the results generated by the stored procedure.</returns>
        public static DataTable ExecuteDataTable(
          MySqlConnection conn,
          string spName,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteDataTable(conn, CommandType.StoredProcedure, spName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set) against the specified MySqlConnection
        /// using the provided parameters.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to be passed to the stored procedure.</param>
        /// <returns>A DataTable containing the results generated by the stored procedure.</returns>
        public static DataTable ExecuteDataTable(
          MySqlConnection conn,
          string spName,
          int timeout,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteDataTable(conn, CommandType.StoredProcedure, spName, timeout, parameters);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set and takes no parameters) against the provided MySqlConnection.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <returns>A DataTable containing the results generated by the command.</returns>
        public static DataTable ExecuteDataTable(
          MySqlConnection conn,
          CommandType type,
          string text)
        {
            return MySqlConnectionHelper.ExecuteDataTable(conn, type, text, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set) against the specified MySqlConnection
        /// using the provided parameters.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A DataTable containing the results generated by the command.</returns>
        public static DataTable ExecuteDataTable(
          MySqlConnection conn,
          CommandType type,
          string text,
          params MySqlParameter[] parameters)
        {
            MySqlCommand MySqlCommand = new MySqlCommand();
            MySqlConnectionHelper.PrepareCommand(MySqlCommand, conn, (MySqlTransaction)null, type, text, parameters);
            MySqlDataAdapter MySqlDataAdapter = new MySqlDataAdapter(MySqlCommand);
            DataTable dataTable = new DataTable();
            try
            {
                MySqlDataAdapter.Fill(dataTable);
            }
            finally
            {
                MySqlCommand.Parameters.Clear();
            }
            return dataTable;
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set) against the specified MySqlConnection
        /// using the provided parameters.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A DataTable containing the results generated by the command.</returns>
        public static DataTable ExecuteDataTable(
          MySqlConnection conn,
          CommandType type,
          string text,
          int timeout,
          params MySqlParameter[] parameters)
        {
            MySqlCommand MySqlCommand = new MySqlCommand();
            MySqlConnectionHelper.PrepareCommand(MySqlCommand, conn, (MySqlTransaction)null, type, text, parameters, timeout);
            MySqlDataAdapter MySqlDataAdapter = new MySqlDataAdapter(MySqlCommand);
            DataTable dataTable = new DataTable();
            try
            {
                MySqlDataAdapter.Fill(dataTable);
            }
            finally
            {
                MySqlCommand.Parameters.Clear();
            }
            return dataTable;
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set) against the specified MySqlTransaction.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <returns>A DataTable containing the result set generated by the stored procedure.</returns>
        public static DataTable ExecuteDataTable(MySqlTransaction trans, string spName)
        {
            return MySqlConnectionHelper.ExecuteDataTable(trans, CommandType.StoredProcedure, spName, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set) against the specified MySqlTransaction.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <returns>A DataTable containing the result set generated by the stored procedure.</returns>
        public static DataTable ExecuteDataTable(
          MySqlTransaction trans,
          string spName,
          int timeout)
        {
            return MySqlConnectionHelper.ExecuteDataTable(trans, CommandType.StoredProcedure, spName, timeout, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="parameters">An array of SqlParameters used to be passed to the stored procedure.</param>
        /// <returns>A DataTable containing the result set generated by the stored procedure.</returns>
        public static DataTable ExecuteDataTable(
          MySqlTransaction trans,
          string spName,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteDataTable(trans, CommandType.StoredProcedure, spName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">An array of SqlParameters used to be passed to the stored procedure.</param>
        /// <returns>A DataTable containing the result set generated by the stored procedure.</returns>
        public static DataTable ExecuteDataTable(
          MySqlTransaction trans,
          string spName,
          int timeout,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteDataTable(trans, CommandType.StoredProcedure, spName, timeout, parameters);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set and takes no parameters) against the provided MySqlTransaction.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <returns>A DataTable containing the results generated by the command.</returns>
        public static DataTable ExecuteDataTable(
          MySqlTransaction trans,
          CommandType type,
          string text)
        {
            return MySqlConnectionHelper.ExecuteDataTable(trans, type, text, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A DataTable containing the results generated by the command.</returns>
        public static DataTable ExecuteDataTable(
          MySqlTransaction trans,
          CommandType type,
          string text,
          params MySqlParameter[] parameters)
        {
            MySqlCommand MySqlCommand = new MySqlCommand();
            MySqlConnectionHelper.PrepareCommand(MySqlCommand, trans.Connection, trans, type, text, parameters);
            MySqlDataAdapter MySqlDataAdapter = new MySqlDataAdapter(MySqlCommand);
            DataTable dataTable = new DataTable();
            try
            {
                MySqlDataAdapter.Fill(dataTable);
            }
            finally
            {
                MySqlCommand.Parameters.Clear();
            }
            return dataTable;
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A DataTable containing the results generated by the command.</returns>
        public static DataTable ExecuteDataTable(
          MySqlTransaction trans,
          CommandType type,
          string text,
          int timeout,
          params MySqlParameter[] parameters)
        {
            MySqlCommand MySqlCommand = new MySqlCommand();
            MySqlConnectionHelper.PrepareCommand(MySqlCommand, trans.Connection, trans, type, text, parameters, timeout);
            MySqlDataAdapter MySqlDataAdapter = new MySqlDataAdapter(MySqlCommand);
            DataTable dataTable = new DataTable();
            try
            {
                MySqlDataAdapter.Fill(dataTable);
            }
            finally
            {
                MySqlCommand.Parameters.Clear();
            }
            return dataTable;
        }

        /// <summary>
        /// Executes a stored procedure (that returns a result set and takes no parameters) against the against the database
        /// specified in the default connection string.
        /// </summary>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <returns>The first row in the result set generated by the command, or null if no row exists.</returns>
        public static DataRow ExecuteDataRow(string spName)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteDataRow(MySqlConnectionHelper.s_connectionString, CommandType.StoredProcedure, spName, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure (that returns a result set and takes no parameters) against the against the database
        /// specified in the default connection string.
        /// </summary>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <returns>The first row in the result set generated by the command, or null if no row exists.</returns>
        public static DataRow ExecuteDataRow(string spName, int timeout)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteDataRow(MySqlConnectionHelper.s_connectionString, CommandType.StoredProcedure, spName, timeout, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure (that returns a result set) against the against the database
        /// specified in the default connection string using the provided parameters.
        /// </summary>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>The first row in the result set generated by the command, or null if no row exists.</returns>
        public static DataRow ExecuteDataRow(string spName, params MySqlParameter[] parameters)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteDataRow(MySqlConnectionHelper.s_connectionString, CommandType.StoredProcedure, spName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure (that returns a result set) against the against the database
        /// specified in the default connection string using the provided parameters.
        /// </summary>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>The first row in the result set generated by the command, or null if no row exists.</returns>
        public static DataRow ExecuteDataRow(
          string spName,
          int timeout,
          params MySqlParameter[] parameters)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteDataRow(MySqlConnectionHelper.s_connectionString, CommandType.StoredProcedure, spName, timeout, parameters);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set and takes no parameters) against the database specified in
        /// the default connection string.
        /// </summary>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <returns>The first row in the result set generated by the command, or null if no row exists.</returns>
        public static DataRow ExecuteDataRow(CommandType type, string text)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteDataRow(MySqlConnectionHelper.s_connectionString, type, text, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set and takes no parameters) against the database specified in
        /// the default connection string.
        /// </summary>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>The first row in the result set generated by the command, or null if no row exists.</returns>
        public static DataRow ExecuteDataRow(
          CommandType type,
          string text,
          params MySqlParameter[] parameters)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteDataRow(MySqlConnectionHelper.s_connectionString, type, text, parameters);
        }

        /// <summary>
        /// Executes a stored procedure (that returns a result set and takes no parameters) against the against the database
        /// specified in the connection string.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <returns>The first row in the result set generated by the command, or null if no row exists.</returns>
        public static DataRow ExecuteDataRow(string connectionString, string spName)
        {
            return MySqlConnectionHelper.ExecuteDataRow(connectionString, CommandType.StoredProcedure, spName, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure (that returns a result set and takes no parameters) against the against the database
        /// specified in the connection string.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <returns>The first row in the result set generated by the command, or null if no row exists.</returns>
        public static DataRow ExecuteDataRow(
          string connectionString,
          string spName,
          int timeout)
        {
            return MySqlConnectionHelper.ExecuteDataRow(connectionString, CommandType.StoredProcedure, spName, timeout, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure (that returns a result set) against the against the database
        /// specified in the connection string using the provided parameters.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>The first row in the result set generated by the command, or null if no row exists.</returns>
        public static DataRow ExecuteDataRow(
          string connectionString,
          string spName,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteDataRow(connectionString, CommandType.StoredProcedure, spName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure (that returns a result set) against the against the database
        /// specified in the connection string using the provided parameters.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>The first row in the result set generated by the command, or null if no row exists.</returns>
        public static DataRow ExecuteDataRow(
          string connectionString,
          string spName,
          int timeout,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteDataRow(connectionString, CommandType.StoredProcedure, spName, timeout, parameters);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set and takes no parameters) against the database specified in
        /// the connection string.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <returns>The first row in the result set generated by the command, or null if no row exists.</returns>
        public static DataRow ExecuteDataRow(
          string connectionString,
          CommandType type,
          string text)
        {
            return MySqlConnectionHelper.ExecuteDataRow(connectionString, type, text, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set) against the database specified in the connection string
        /// using the provided parameters.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>The first row in the result set generated by the command, or null if no row exists.</returns>
        public static DataRow ExecuteDataRow(
          string connectionString,
          CommandType type,
          string text,
          params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                return MySqlConnectionHelper.ExecuteDataRow(conn, type, text, parameters);
            }
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set) against the database specified in the connection string
        /// using the provided parameters.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>The first row in the result set generated by the command, or null if no row exists.</returns>
        public static DataRow ExecuteDataRow(
          string connectionString,
          CommandType type,
          string text,
          int timeout,
          params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                return MySqlConnectionHelper.ExecuteDataRow(conn, type, text, timeout, parameters);
            }
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set) against the specified MySqlConnection.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <returns>The first row in the result set generated by the command, or null if no row exists.</returns>
        public static DataRow ExecuteDataRow(MySqlConnection conn, string spName)
        {
            return MySqlConnectionHelper.ExecuteDataRow(conn, CommandType.StoredProcedure, spName, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set) against the specified MySqlConnection.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <returns>The first row in the result set generated by the command, or null if no row exists.</returns>
        public static DataRow ExecuteDataRow(MySqlConnection conn, string spName, int timeout)
        {
            return MySqlConnectionHelper.ExecuteDataRow(conn, CommandType.StoredProcedure, spName, timeout, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set) against the specified MySqlConnection
        /// using the provided parameters.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>The first row in the result set generated by the command, or null if no row exists.</returns>
        public static DataRow ExecuteDataRow(
          MySqlConnection conn,
          string spName,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteDataRow(conn, CommandType.StoredProcedure, spName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set) against the specified MySqlConnection
        /// using the provided parameters.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>The first row in the result set generated by the command, or null if no row exists.</returns>
        public static DataRow ExecuteDataRow(
          MySqlConnection conn,
          string spName,
          int timeout,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteDataRow(conn, CommandType.StoredProcedure, spName, timeout, parameters);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set and takes no parameters) against the provided MySqlConnection.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <returns>The first row in the result set generated by the command, or null if no row exists.</returns>
        public static DataRow ExecuteDataRow(MySqlConnection conn, CommandType type, string text)
        {
            return MySqlConnectionHelper.ExecuteDataRow(conn, type, text, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set) against the specified MySqlConnection
        /// using the provided parameters.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>The first row in the result set generated by the command, or null if no row exists.</returns>
        public static DataRow ExecuteDataRow(
          MySqlConnection conn,
          CommandType type,
          string text,
          params MySqlParameter[] parameters)
        {
            MySqlCommand MySqlCommand = new MySqlCommand();
            MySqlConnectionHelper.PrepareCommand(MySqlCommand, conn, (MySqlTransaction)null, type, text, parameters);
            MySqlDataAdapter MySqlDataAdapter = new MySqlDataAdapter(MySqlCommand);
            DataSet dataSet = new DataSet();
            try
            {
                MySqlDataAdapter.Fill(dataSet, 0, 1, "Table");
            }
            finally
            {
                MySqlCommand.Parameters.Clear();
            }
            DataRowCollection rows = dataSet.Tables[0].Rows;
            return rows.Count <= 0 ? (DataRow)null : rows[0];
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set) against the specified MySqlConnection
        /// using the provided parameters.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>The first row in the result set generated by the command, or null if no row exists.</returns>
        public static DataRow ExecuteDataRow(
          MySqlConnection conn,
          CommandType type,
          string text,
          int timeout,
          params MySqlParameter[] parameters)
        {
            MySqlCommand MySqlCommand = new MySqlCommand();
            MySqlConnectionHelper.PrepareCommand(MySqlCommand, conn, (MySqlTransaction)null, type, text, parameters, timeout);
            MySqlDataAdapter MySqlDataAdapter = new MySqlDataAdapter(MySqlCommand);
            DataSet dataSet = new DataSet();
            try
            {
                MySqlDataAdapter.Fill(dataSet, 0, 1, "Table");
            }
            finally
            {
                MySqlCommand.Parameters.Clear();
            }
            DataRowCollection rows = dataSet.Tables[0].Rows;
            return rows.Count <= 0 ? (DataRow)null : rows[0];
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set) against the specified MySqlTransaction.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <returns>The first row in the result set generated by the command, or null if no row exists.</returns>
        public static DataRow ExecuteDataRow(MySqlTransaction trans, string spName)
        {
            return MySqlConnectionHelper.ExecuteDataRow(trans, CommandType.StoredProcedure, spName, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set) against the specified MySqlTransaction.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <returns>The first row in the result set generated by the command, or null if no row exists.</returns>
        public static DataRow ExecuteDataRow(MySqlTransaction trans, string spName, int timeout)
        {
            return MySqlConnectionHelper.ExecuteDataRow(trans, CommandType.StoredProcedure, spName, timeout, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>The first row in the result set generated by the command, or null if no row exists.</returns>
        public static DataRow ExecuteDataRow(
          MySqlTransaction trans,
          string spName,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteDataRow(trans, CommandType.StoredProcedure, spName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>The first row in the result set generated by the command, or null if no row exists.</returns>
        public static DataRow ExecuteDataRow(
          MySqlTransaction trans,
          string spName,
          int timeout,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteDataRow(trans, CommandType.StoredProcedure, spName, timeout, parameters);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set and takes no parameters) against the provided MySqlTransaction.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <returns>The first row in the result set generated by the command, or null if no row exists.</returns>
        public static DataRow ExecuteDataRow(
          MySqlTransaction trans,
          CommandType type,
          string text)
        {
            return MySqlConnectionHelper.ExecuteDataRow(trans, type, text, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>The first row in the result set generated by the command, or null if no row exists.</returns>
        public static DataRow ExecuteDataRow(
          MySqlTransaction trans,
          CommandType type,
          string text,
          params MySqlParameter[] parameters)
        {
            MySqlCommand MySqlCommand = new MySqlCommand();
            MySqlConnectionHelper.PrepareCommand(MySqlCommand, trans.Connection, trans, type, text, parameters);
            MySqlDataAdapter MySqlDataAdapter = new MySqlDataAdapter(MySqlCommand);
            DataSet dataSet = new DataSet();
            try
            {
                MySqlDataAdapter.Fill(dataSet, 0, 1, "Table");
            }
            finally
            {
                MySqlCommand.Parameters.Clear();
            }
            DataRowCollection rows = dataSet.Tables[0].Rows;
            return rows.Count <= 0 ? (DataRow)null : rows[0];
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>The first row in the result set generated by the command, or null if no row exists.</returns>
        public static DataRow ExecuteDataRow(
          MySqlTransaction trans,
          CommandType type,
          string text,
          int timeout,
          params MySqlParameter[] parameters)
        {
            MySqlCommand MySqlCommand = new MySqlCommand();
            MySqlConnectionHelper.PrepareCommand(MySqlCommand, trans.Connection, trans, type, text, parameters, timeout);
            MySqlDataAdapter MySqlDataAdapter = new MySqlDataAdapter(MySqlCommand);
            DataSet dataSet = new DataSet();
            try
            {
                MySqlDataAdapter.Fill(dataSet, 0, 1, "Table");
            }
            finally
            {
                MySqlCommand.Parameters.Clear();
            }
            DataRowCollection rows = dataSet.Tables[0].Rows;
            return rows.Count <= 0 ? (DataRow)null : rows[0];
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set and takes no parameters) against the database specified in
        /// the default connection string.
        /// </summary>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <returns>A MySqlDataReader containing the result set generated by the command.</returns>
        public static MySqlDataReader ExecuteReader(string spName)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteReader(MySqlConnectionHelper.s_connectionString, CommandType.StoredProcedure, spName, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set and takes no parameters) against the database specified in
        /// the default connection string.
        /// </summary>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <returns>A MySqlDataReader containing the result set generated by the command.</returns>
        public static MySqlDataReader ExecuteReader(string spName, int timeout)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteReader(MySqlConnectionHelper.s_connectionString, CommandType.StoredProcedure, spName, timeout, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set) against the database specified in the default connection string
        /// using the provided parameters.
        /// </summary>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A MySqlDataReader containing the result set generated by the command.</returns>
        public static MySqlDataReader ExecuteReader(
          string spName,
          params MySqlParameter[] parameters)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteReader(MySqlConnectionHelper.s_connectionString, CommandType.StoredProcedure, spName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set) against the database specified in the default connection string
        /// using the provided parameters.
        /// </summary>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A MySqlDataReader containing the result set generated by the command.</returns>
        public static MySqlDataReader ExecuteReader(
          string spName,
          int timeout,
          params MySqlParameter[] parameters)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteReader(MySqlConnectionHelper.s_connectionString, CommandType.StoredProcedure, spName, timeout, parameters);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set and takes no parameters) against the database specified in
        /// the default connection string.
        /// </summary>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <returns>A MySqlDataReader containing the result set generated by the command.</returns>
        public static MySqlDataReader ExecuteReader(CommandType type, string text)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteReader(MySqlConnectionHelper.s_connectionString, type, text, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set and takes no parameters) against the database specified in
        /// the default connection string using the provided parameters.
        /// </summary>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A MySqlDataReader containing the result set generated by the command.</returns>
        public static MySqlDataReader ExecuteReader(
          CommandType type,
          string text,
          params MySqlParameter[] parameters)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteReader(MySqlConnectionHelper.s_connectionString, type, text, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set and takes no parameters) against the database
        /// specified in the connection string.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <returns>A MySqlDataReader containing the result set generated by the command.</returns>
        public static MySqlDataReader ExecuteReader(string connectionString, string spName)
        {
            return MySqlConnectionHelper.ExecuteReader(connectionString, CommandType.StoredProcedure, spName, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set and takes no parameters) against the database
        /// specified in the connection string.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <returns>A MySqlDataReader containing the result set generated by the command.</returns>
        public static MySqlDataReader ExecuteReader(
          string connectionString,
          string spName,
          int timeout)
        {
            return MySqlConnectionHelper.ExecuteReader(connectionString, CommandType.StoredProcedure, spName, timeout, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set) against the database
        /// specified in the connection string using the provided parameters.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A MySqlDataReader containing the result set generated by the command.</returns>
        public static MySqlDataReader ExecuteReader(
          string connectionString,
          string spName,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteReader(connectionString, CommandType.StoredProcedure, spName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set) against the database
        /// specified in the connection string using the provided parameters.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A MySqlDataReader containing the result set generated by the command.</returns>
        public static MySqlDataReader ExecuteReader(
          string connectionString,
          string spName,
          int timeout,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteReader(connectionString, CommandType.StoredProcedure, spName, timeout, parameters);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set and takes no parameters) against the database specified in
        /// the connection string.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <returns>a MySqlDataReader containing the result set generated by the command</returns>
        public static MySqlDataReader ExecuteReader(
          string connectionString,
          CommandType type,
          string text)
        {
            return MySqlConnectionHelper.ExecuteReader(connectionString, type, text, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set) against the database specified in the connection string
        /// using the provided parameters.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A MySqlDataReader containing the result set generated by the command.</returns>
        public static MySqlDataReader ExecuteReader(
          string connectionString,
          CommandType type,
          string text,
          params MySqlParameter[] parameters)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            try
            {
                return MySqlConnectionHelper.ExecuteReader(conn, (MySqlTransaction)null, type, text, parameters, MySqlConnectionHelper.SqlConnectionOwnership.Internal);
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set) against the database specified in the connection string
        /// using the provided parameters.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A MySqlDataReader containing the result set generated by the command.</returns>
        public static MySqlDataReader ExecuteReader(
          string connectionString,
          CommandType type,
          string text,
          int timeout,
          params MySqlParameter[] parameters)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            try
            {
                return MySqlConnectionHelper.ExecuteReader(conn, (MySqlTransaction)null, type, text, parameters, MySqlConnectionHelper.SqlConnectionOwnership.Internal, timeout);
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// Executes a stored procedure via s MySqlCommand (that returns a result set) against the specified MySqlConnection.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <returns>A MySqlDataReader containing the result set generated by the command.</returns>
        public static MySqlDataReader ExecuteReader(MySqlConnection conn, string spName)
        {
            return MySqlConnectionHelper.ExecuteReader(conn, CommandType.StoredProcedure, spName, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure via s MySqlCommand (that returns a result set) against the specified MySqlConnection.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <returns>A MySqlDataReader containing the result set generated by the command.</returns>
        public static MySqlDataReader ExecuteReader(
          MySqlConnection conn,
          string spName,
          int timeout)
        {
            return MySqlConnectionHelper.ExecuteReader(conn, CommandType.StoredProcedure, spName, timeout, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure via s MySqlCommand (that returns a result set) against the specified MySqlConnection
        /// using the provided parameters.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A MySqlDataReader containing the result set generated by the command.</returns>
        public static MySqlDataReader ExecuteReader(
          MySqlConnection conn,
          string spName,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteReader(conn, CommandType.StoredProcedure, spName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via s MySqlCommand (that returns a result set) against the specified MySqlConnection
        /// using the provided parameters.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A MySqlDataReader containing the result set generated by the command.</returns>
        public static MySqlDataReader ExecuteReader(
          MySqlConnection conn,
          string spName,
          int timeout,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteReader(conn, CommandType.StoredProcedure, spName, timeout, parameters);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set and takes no parameters) against the provided MySqlConnection.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <returns>a MySqlDataReader containing the result set generated by the command</returns>
        public static MySqlDataReader ExecuteReader(
          MySqlConnection conn,
          CommandType type,
          string text)
        {
            return MySqlConnectionHelper.ExecuteReader(conn, type, text, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set) against the specified MySqlConnection
        /// using the provided parameters.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A MySqlDataReader containing the result set generated by the command.</returns>
        public static MySqlDataReader ExecuteReader(
          MySqlConnection conn,
          CommandType type,
          string text,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteReader(conn, (MySqlTransaction)null, type, text, parameters, MySqlConnectionHelper.SqlConnectionOwnership.External);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set) against the specified MySqlConnection
        /// using the provided parameters.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A MySqlDataReader containing the result set generated by the command.</returns>
        public static MySqlDataReader ExecuteReader(
          MySqlConnection conn,
          CommandType type,
          string text,
          int timeout,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteReader(conn, (MySqlTransaction)null, type, text, parameters, MySqlConnectionHelper.SqlConnectionOwnership.External, timeout);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set) against the specified MySqlTransaction.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <returns>A MySqlDataReader containing the result set generated by the command.</returns>
        public static MySqlDataReader ExecuteReader(MySqlTransaction trans, string spName)
        {
            return MySqlConnectionHelper.ExecuteReader(trans, CommandType.StoredProcedure, spName, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set) against the specified MySqlTransaction.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <returns>A MySqlDataReader containing the result set generated by the command.</returns>
        public static MySqlDataReader ExecuteReader(
          MySqlTransaction trans,
          string spName,
          int timeout)
        {
            return MySqlConnectionHelper.ExecuteReader(trans, CommandType.StoredProcedure, spName, timeout, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A MySqlDataReader containing the result set generated by the command.</returns>
        public static MySqlDataReader ExecuteReader(
          MySqlTransaction trans,
          string spName,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteReader(trans, CommandType.StoredProcedure, spName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a result set) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A MySqlDataReader containing the result set generated by the command.</returns>
        public static MySqlDataReader ExecuteReader(
          MySqlTransaction trans,
          string spName,
          int timeout,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteReader(trans, CommandType.StoredProcedure, spName, timeout, parameters);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set and takes no parameters) against the provided MySqlTransaction.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <returns>a MySqlDataReader containing the result set generated by the command</returns>
        public static MySqlDataReader ExecuteReader(
          MySqlTransaction trans,
          CommandType type,
          string text)
        {
            return MySqlConnectionHelper.ExecuteReader(trans, type, text, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A MySqlDataReader containing the result set generated by the command.</returns>
        public static MySqlDataReader ExecuteReader(
          MySqlTransaction trans,
          CommandType type,
          string text,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteReader(trans.Connection, trans, type, text, parameters, MySqlConnectionHelper.SqlConnectionOwnership.External);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>A MySqlDataReader containing the result set generated by the command.</returns>
        public static MySqlDataReader ExecuteReader(
          MySqlTransaction trans,
          CommandType type,
          string text,
          int timeout,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteReader(trans.Connection, trans, type, text, parameters, MySqlConnectionHelper.SqlConnectionOwnership.External, timeout);
        }

        private static MySqlDataReader ExecuteReader(
          MySqlConnection conn,
          MySqlTransaction trans,
          CommandType type,
          string text,
          MySqlParameter[] parameters,
          MySqlConnectionHelper.SqlConnectionOwnership connectionOwnership)
        {
            MySqlCommand command = new MySqlCommand();
            MySqlConnectionHelper.PrepareCommand(command, conn, trans, type, text, parameters);
            try
            {
                return connectionOwnership == MySqlConnectionHelper.SqlConnectionOwnership.External ? command.ExecuteReader() : command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            finally
            {
                command.Parameters.Clear();
            }
        }

        private static MySqlDataReader ExecuteReader(
          MySqlConnection conn,
          MySqlTransaction trans,
          CommandType type,
          string text,
          MySqlParameter[] parameters,
          MySqlConnectionHelper.SqlConnectionOwnership connectionOwnership,
          int timeout)
        {
            MySqlCommand command = new MySqlCommand();
            MySqlConnectionHelper.PrepareCommand(command, conn, trans, type, text, parameters, timeout);
            try
            {
                return connectionOwnership == MySqlConnectionHelper.SqlConnectionOwnership.External ? command.ExecuteReader() : command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            finally
            {
                command.Parameters.Clear();
            }
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a 1x1 result set and takes no parameters) against
        /// the database specified in the default connection string.
        /// </summary>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <returns>An object containing the value in the 1x1 result set generated by the command.</returns>
        public static object ExecuteScalar(string spName)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteScalar(MySqlConnectionHelper.s_connectionString, CommandType.StoredProcedure, spName, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a 1x1 result set and takes no parameters) against
        /// the database specified in the default connection string.
        /// </summary>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <returns>An object containing the value in the 1x1 result set generated by the command.</returns>
        public static object ExecuteScalar(string spName, int timeout)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteScalar(MySqlConnectionHelper.s_connectionString, CommandType.StoredProcedure, spName, timeout, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a 1x1 result set) against the database specified
        /// in the default connection string using the provided parameters.
        /// </summary>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>An object containing the value in the 1x1 result set generated by the command.</returns>
        public static object ExecuteScalar(string spName, params MySqlParameter[] parameters)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteScalar(MySqlConnectionHelper.s_connectionString, CommandType.StoredProcedure, spName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a 1x1 result set) against the database specified
        /// in the default connection string using the provided parameters.
        /// </summary>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>An object containing the value in the 1x1 result set generated by the command.</returns>
        public static object ExecuteScalar(
          string spName,
          int timeout,
          params MySqlParameter[] parameters)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteScalar(MySqlConnectionHelper.s_connectionString, CommandType.StoredProcedure, spName, timeout, parameters);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a 1x1 result set and takes no parameters) against the database specified in
        /// the default connection string.
        /// </summary>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <returns>An object containing the value in the 1x1 result set generated by the command.</returns>
        public static object ExecuteScalar(CommandType type, string text)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteScalar(MySqlConnectionHelper.s_connectionString, type, text, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a 1x1 result set and takes no parameters) against the database specified in
        /// the default connection string using the provided parameters.
        /// </summary>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>An object containing the value in the 1x1 result set generated by the command.</returns>
        public static object ExecuteScalar(
          CommandType type,
          string text,
          params MySqlParameter[] parameters)
        {
            if (MySqlConnectionHelper.s_connectionString == null)
                throw new InvalidOperationException("A default connection string must be specified before calling this method.");
            return MySqlConnectionHelper.ExecuteScalar(MySqlConnectionHelper.s_connectionString, type, text, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a 1x1 result set and takes no parameters) against
        /// the database specified in the connection string.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <returns>An object containing the value in the 1x1 result set generated by the command.</returns>
        public static object ExecuteScalar(string connectionString, string spName)
        {
            return MySqlConnectionHelper.ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a 1x1 result set and takes no parameters) against
        /// the database specified in the connection string.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <returns>An object containing the value in the 1x1 result set generated by the command.</returns>
        public static object ExecuteScalar(string connectionString, string spName, int timeout)
        {
            return MySqlConnectionHelper.ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, timeout, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a 1x1 result set) against the database specified
        /// in the connection string using the provided parameters.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>An object containing the value in the 1x1 result set generated by the command.</returns>
        public static object ExecuteScalar(
          string connectionString,
          string spName,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a 1x1 result set) against the database specified
        /// in the connection string using the provided parameters.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>An object containing the value in the 1x1 result set generated by the command.</returns>
        public static object ExecuteScalar(
          string connectionString,
          string spName,
          int timeout,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, timeout, parameters);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a 1x1 result set and takes no parameters) against the database specified in
        /// the connection string.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <returns>An object containing the value in the 1x1 result set generated by the command.</returns>
        public static object ExecuteScalar(string connectionString, CommandType type, string text)
        {
            return MySqlConnectionHelper.ExecuteScalar(connectionString, type, text, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a 1x1 result set) against the database specified in the connection string
        /// using the provided parameters.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>An object containing the value in the 1x1 result set generated by the command.</returns>
        public static object ExecuteScalar(
          string connectionString,
          CommandType type,
          string text,
          params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                return MySqlConnectionHelper.ExecuteScalar(conn, type, text, parameters);
            }
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a 1x1 result set) against the database specified in the connection string
        /// using the provided parameters.
        /// </summary>
        /// <param name="connectionString">Valid database connection string.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>An object containing the value in the 1x1 result set generated by the command.</returns>
        public static object ExecuteScalar(
          string connectionString,
          CommandType type,
          string text,
          int timeout,
          params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                return MySqlConnectionHelper.ExecuteScalar(conn, type, text, timeout, parameters);
            }
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a 1x1 result set) against the specified MySqlConnection.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <returns>An object containing the value in the 1x1 result set generated by the command.</returns>
        public static object ExecuteScalar(MySqlConnection conn, string spName)
        {
            return MySqlConnectionHelper.ExecuteScalar(conn, CommandType.StoredProcedure, spName, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a 1x1 result set) against the specified MySqlConnection.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <returns>An object containing the value in the 1x1 result set generated by the command.</returns>
        public static object ExecuteScalar(MySqlConnection conn, string spName, int timeout)
        {
            return MySqlConnectionHelper.ExecuteScalar(conn, CommandType.StoredProcedure, spName, timeout, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a 1x1 result set) against the specified MySqlConnection
        /// using the provided parameters.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>An object containing the value in the 1x1 result set generated by the command.</returns>
        public static object ExecuteScalar(
          MySqlConnection conn,
          string spName,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteScalar(conn, CommandType.StoredProcedure, spName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a 1x1 result set) against the specified MySqlConnection
        /// using the provided parameters.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>An object containing the value in the 1x1 result set generated by the command.</returns>
        public static object ExecuteScalar(
          MySqlConnection conn,
          string spName,
          int timeout,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteScalar(conn, CommandType.StoredProcedure, spName, timeout, parameters);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a 1x1 result set and takes no parameters) against the provided MySqlConnection.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <returns>An object containing the value in the 1x1 result set generated by the command.</returns>
        public static object ExecuteScalar(MySqlConnection conn, CommandType type, string text)
        {
            return MySqlConnectionHelper.ExecuteScalar(conn, type, text, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a 1x1 result set) against the specified MySqlConnection
        /// using the provided parameters.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>An object containing the value in the 1x1 result set generated by the command.</returns>
        public static object ExecuteScalar(
          MySqlConnection conn,
          CommandType type,
          string text,
          params MySqlParameter[] parameters)
        {
            MySqlCommand command = new MySqlCommand();
            MySqlConnectionHelper.PrepareCommand(command, conn, (MySqlTransaction)null, type, text, parameters);
            try
            {
                return command.ExecuteScalar();
            }
            finally
            {
                command.Parameters.Clear();
            }
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a 1x1 result set) against the specified MySqlConnection
        /// using the provided parameters.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>An object containing the value in the 1x1 result set generated by the command.</returns>
        public static object ExecuteScalar(
          MySqlConnection conn,
          CommandType type,
          string text,
          int timeout,
          params MySqlParameter[] parameters)
        {
            MySqlCommand command = new MySqlCommand();
            MySqlConnectionHelper.PrepareCommand(command, conn, (MySqlTransaction)null, type, text, parameters, timeout);
            try
            {
                return command.ExecuteScalar();
            }
            finally
            {
                command.Parameters.Clear();
            }
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a 1x1 result set) against the specified MySqlTransaction.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <returns>An object containing the value in the 1x1 result set generated by the command.</returns>
        public static object ExecuteScalar(MySqlTransaction trans, string spName)
        {
            return MySqlConnectionHelper.ExecuteScalar(trans, CommandType.StoredProcedure, spName, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a 1x1 result set) against the specified MySqlTransaction.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <returns>An object containing the value in the 1x1 result set generated by the command.</returns>
        public static object ExecuteScalar(MySqlTransaction trans, string spName, int timeout)
        {
            return MySqlConnectionHelper.ExecuteScalar(trans, CommandType.StoredProcedure, spName, timeout, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a 1x1 result set) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>An object containing the value in the 1x1 result set generated by the command.</returns>
        public static object ExecuteScalar(
          MySqlTransaction trans,
          string spName,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteScalar(trans, CommandType.StoredProcedure, spName, parameters);
        }

        /// <summary>
        /// Executes a stored procedure via a MySqlCommand (that returns a 1x1 result set) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="spName">Name of the stored procedure to execute.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>An object containing the value in the 1x1 result set generated by the command.</returns>
        public static object ExecuteScalar(
          MySqlTransaction trans,
          string spName,
          int timeout,
          params MySqlParameter[] parameters)
        {
            return MySqlConnectionHelper.ExecuteScalar(trans, CommandType.StoredProcedure, spName, timeout, parameters);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a 1x1 result set and takes no parameters) against the provided MySqlTransaction.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <returns>An object containing the value in the 1x1 result set generated by the command.</returns>
        public static object ExecuteScalar(MySqlTransaction trans, CommandType type, string text)
        {
            return MySqlConnectionHelper.ExecuteScalar(trans, type, text, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a 1x1 result set) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>An object containing the value in the 1x1 result set generated by the command.</returns>
        public static object ExecuteScalar(
          MySqlTransaction trans,
          CommandType type,
          string text,
          params MySqlParameter[] parameters)
        {
            MySqlCommand command = new MySqlCommand();
            MySqlConnectionHelper.PrepareCommand(command, trans.Connection, trans, type, text, parameters);
            try
            {
                return command.ExecuteScalar();
            }
            finally
            {
                command.Parameters.Clear();
            }
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a 1x1 result set) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>An object containing the value in the 1x1 result set generated by the command.</returns>
        public static object ExecuteScalar(
          MySqlTransaction trans,
          CommandType type,
          string text,
          int timeout,
          params MySqlParameter[] parameters)
        {
            MySqlCommand command = new MySqlCommand();
            MySqlConnectionHelper.PrepareCommand(command, trans.Connection, trans, type, text, parameters, timeout);
            try
            {
                return command.ExecuteScalar();
            }
            finally
            {
                command.Parameters.Clear();
            }
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set and takes no parameters) against the provided MySqlConnection.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command (using "FOR XML AUTO").</param>
        /// <returns>An XmlReader containing the result set generated by the command.</returns>
        public static XmlReader ExecuteXmlReader(
          MySqlConnection conn,
          CommandType type,
          string text,
          MySqlParameter[]? mySqlParameters)
        {
            return MySqlConnectionHelper.ExecuteXmlReader(conn, type, text, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set and takes no parameters) against the provided MySqlConnection.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command (using "FOR XML AUTO").</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <returns>An XmlReader containing the result set generated by the command.</returns>
        public static XmlReader ExecuteXmlReader(
          MySqlConnection conn,
          CommandType type,
          string text,
          int timeout,
          MySqlParameter[]? mySqlParameters)
        {
            return MySqlConnectionHelper.ExecuteXmlReader(conn, type, text, timeout, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set) against the specified MySqlConnection
        /// using the provided parameters.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command (using "FOR XML AUTO").</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>An XmlReader containing the result set generated by the command.</returns>
        //public static XmlReader ExecuteXmlReader(
        //  MySqlConnection conn,
        //  CommandType type,
        //  string text,
        //  params MySqlParameter[] parameters)
        //{
        //    MySqlCommand command = new MySqlCommand();
        //    MySqlConnectionHelper.PrepareCommand(command, conn, (MySqlTransaction)null, type, text, parameters);
        //    try
        //    {
        //        return command.ExecuteXmlReader();
        //    }
        //    finally
        //    {
        //        command.Parameters.Clear();
        //    }
        //}

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set) against the specified MySqlConnection
        /// using the provided parameters.
        /// </summary>
        /// <param name="conn">Initialized MySqlConnection object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command (using "FOR XML AUTO").</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>An XmlReader containing the result set generated by the command.</returns>
        //public static XmlReader ExecuteXmlReader(
        //  MySqlConnection conn,
        //  CommandType type,
        //  string text,
        //  int timeout,
        //  params MySqlParameter[] parameters)
        //{
        //    MySqlCommand command = new MySqlCommand();
        //    MySqlConnectionHelper.PrepareCommand(command, conn, (MySqlTransaction)null, type, text, parameters, timeout);
        //    try
        //    {
        //        return command.ExecuteXmlReader();
        //    }
        //    finally
        //    {
        //        command.Parameters.Clear();
        //    }
        //}

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set and takes no parameters) against the provided MySqlTransaction.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command (using "FOR XML AUTO").</param>
        /// <returns>An XmlReader containing the result set generated by the command.</returns>
        public static XmlReader ExecuteXmlReader(
          MySqlTransaction trans,
          CommandType type,
          string text,
          MySqlParameter[]? mySqlParameters)
        {
            return MySqlConnectionHelper.ExecuteXmlReader(trans, type, text, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set and takes no parameters) against the provided MySqlTransaction.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command (using "FOR XML AUTO").</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <returns>An XmlReader containing the result set generated by the command.</returns>
        public static XmlReader ExecuteXmlReader(
          MySqlTransaction trans,
          CommandType type,
          string text,
          int timeout,
          MySqlParameter[]? mySqlParameters)
        {
            return MySqlConnectionHelper.ExecuteXmlReader(trans, type, text, timeout, (MySqlParameter[])null);
        }

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command (using "FOR XML AUTO").</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>An XmlReader containing the result set generated by the command.</returns>
        //public static XmlReader ExecuteXmlReader(
        //  MySqlTransaction trans,
        //  CommandType type,
        //  string text,
        //  params MySqlParameter[] parameters)
        //{
        //    MySqlCommand command = new MySqlCommand();
        //    MySqlConnectionHelper.PrepareCommand(command, trans.Connection, trans, type, text, parameters);
        //    try
        //    {
        //        return command.ExecuteXmlReader();
        //    }
        //    finally
        //    {
        //        command.Parameters.Clear();
        //    }
        //}

        /// <summary>
        /// Executes a MySqlCommand (that returns a result set) against the specified MySqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <param name="trans">Initialized MySqlTransaction object.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command (using "FOR XML AUTO").</param>
        /// <param name="timeout">Number of seconds to wait before execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        /// <returns>An XmlReader containing the result set generated by the command.</returns>
        //public static XmlReader ExecuteXmlReader(
        //  MySqlTransaction trans,
        //  CommandType type,
        //  string text,
        //  int timeout,
        //  params MySqlParameter[] parameters)
        //{
        //    MySqlCommand command = new MySqlCommand();
        //    MySqlConnectionHelper.PrepareCommand(command, trans.Connection, trans, type, text, parameters, timeout);
        //    try
        //    {
        //        return command.ExecuteXmlReader();
        //    }
        //    finally
        //    {
        //        command.Parameters.Clear();
        //    }
        //}

        /// <summary>
        /// Prepares the provided MySqlCommand for execution with the default timeout period.
        /// The connection is opened if necessary.
        /// </summary>
        /// <param name="command">The MySqlCommand to be prepared.</param>
        /// <param name="conn">A valid MySqlConnection on which to execute this command.</param>
        /// <param name="trans">A valid MySqlTransaction object, or 'null'.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="parameters">Array of SqlParameters to be associated with the command or 'null' if no parameters are required.</param>
        private static void PrepareCommand(
          MySqlCommand command,
          MySqlConnection conn,
          MySqlTransaction trans,
          CommandType type,
          string text,
          MySqlParameter[] parameters)
        {
            command.Connection = conn;
            command.CommandType = type;
            command.CommandText = text;
            if (trans != null)
                command.Transaction = trans;
            if (parameters != null)
                MySqlConnectionHelper.AttachParameters(command, parameters);
            if (conn.State == ConnectionState.Open)
                return;
            conn.Open();
        }

        /// <summary>
        /// Prepares the provided MySqlCommand for execution with a specific timeout period.
        /// The connection is opened if necessary.
        /// </summary>
        /// <param name="command">The MySqlCommand to be prepared.</param>
        /// <param name="conn">A valid MySqlConnection on which to execute this command.</param>
        /// <param name="trans">A valid MySqlTransaction object, or 'null'.</param>
        /// <param name="type">The CommandType (stored procedure, text, etc.) of the command text.</param>
        /// <param name="text">Stored procedure name or T-SQL command.</param>
        /// <param name="parameters">Array of SqlParameters to be associated with the command or 'null' if no parameters are required.</param>
        /// <param name="timeout">Number of seconds to wait before command execution is terminated.  A value of zero (infinite timeout) is not allowed.</param>
        private static void PrepareCommand(
          MySqlCommand command,
          MySqlConnection conn,
          MySqlTransaction trans,
          CommandType type,
          string text,
          MySqlParameter[] parameters,
          int timeout)
        {
            if (timeout <= 0)
                throw new ArgumentOutOfRangeException(nameof(timeout), (object)timeout, "Timeout period must be a positive number greater than zero.");
            MySqlConnectionHelper.PrepareCommand(command, conn, trans, type, text, parameters);
            command.CommandTimeout = timeout;
        }

        /// <summary>
        /// This method is used to attach array of SqlParameters to a MySqlCommand.
        /// 
        /// A value of DBNull will be assigned to any parameter with a direction of
        /// InputOutput and a value of null.
        /// 
        /// This behavior will prevent default values from being used, but
        /// this will be the less common case than an intended pure output parameter (derived as InputOutput)
        /// where the user provided no input value.
        /// </summary>
        /// <param name="command">Command to which the parameters will be added.</param>
        /// <param name="parameters">Array of SqlParameters used to execute the command.</param>
        private static void AttachParameters(MySqlCommand command, MySqlParameter[] parameters)
        {
            foreach (MySqlParameter parameter in parameters)
            {
                if (parameter.Value == null && parameter.Direction == ParameterDirection.InputOutput)
                    parameter.Value = (object)DBNull.Value;
                command.Parameters.Add(parameter);
            }
        }

        /// <summary>
        /// Indicates the owner of the connection so that we can set the appropriate
        /// CommandBehavior when calling ExecuteReader().
        /// </summary>
        private enum SqlConnectionOwnership
        {
            /// <summary>Indicates the connection is owned and managed by SqlHelper.</summary>
            Internal,
            /// <summary>Indicates the connection is owned and managed by the caller.</summary>
            External,
        }
    }
}
