#region using directives

using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using SoftMarketing.Model;
using MySql.Data.MySqlClient;
using Dapper;
using SoftMarketing.DAL.MySQL.Helper;
using SoftMarketing.DAL.UnitOfWork;
#endregion

namespace SoftMarketing.DAL
{

    public class EventDAL : DataAccessBase
    {

        public EventDAL()
        {
        }

        public DynamicParameters FillParams(Events Event)
        {
            var parms = new DynamicParameters();
            parms.Add("Eventid", Event.eventid);
            parms.Add("Trial_eventname_2", Event.eventname);
            parms.Add("Eventdetail", Event.eventdetail);
            return parms;
        }

        //public Int32 Add(Events Event)
        //{
        //    var parms = FillParams(Event);
        //    var data = Connection.Query<Events>("EventInsert", new
        //    {
        //        parms,
        //    }, transaction: DbTransaction, commandType: CommandType.StoredProcedure);

        //    return data.Count();
        //}

        //public Int32 Update(Events Event)
        //{
        //    var parms = FillParams(Event);
        //    var data = Connection.Query<Events>("EventUpdate", new
        //    {
        //        parms,
        //    }, transaction: DbTransaction, commandType: CommandType.StoredProcedure);

        //    return data.Count();
        //}

        public Int32 Add()
        {
            var dataTimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var lastVist = DateTime.Now.ToString("yyyy-MM-dd");
            string query = $"INSERT INTO soft_marketing_dev.customers (customerID, userID, phone, name,added, last_visit) values(15,'1','07779','InsertQuery','{dataTimeNow}','{lastVist}')";
            //string query = "INSERT INTO soft_marketing_dev.customers (customerID, userID, phone, name, added,last_visit) values(" + 14 + ",'" + 1 + "','" + "0779111" + "','" + "InsertQuery" + "','" + dataTimeNow + "')";

            return Connection.Query<User_Message>(query, null, commandType: CommandType.Text).Count();
            //var parms = FillParams(Event);
        }

        public Int32 Update(Events Event)
        {
            var parms = FillParams(Event);
            var data = Connection.Query<Events>("EventUpdate", new
            {
                parms,
            }, transaction: DbTransaction, commandType: CommandType.StoredProcedure);

            return data.Count();
        }


        public void Delete(Int32 eventid, DbTransaction transaction)
        {

            var parms = new DynamicParameters();
            parms.Add("Eventid", eventid);
            var connection = transaction.Connection;
            var data = connection.Query<Events>("EventDelete", new
            {
                parms,
            }, transaction: transaction, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Events> GetPagedData(Int32 pageFirstRow, Int32 pageRowCount, String toPageOn, String toSortOn, string connectionString = null)
        {
            var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
            var parms = new DynamicParameters();
            parms.Add("pageFirstRow", pageFirstRow);
            parms.Add("pageRowCount", pageRowCount);
            parms.Add("toPageOn", toPageOn);
            parms.Add("toSortOn", toSortOn);
            using (var connection = Database.GetNewConnection(connString, true))
            {
                return connection.Query<Events>("EventGetPagingData", parms, commandType: CommandType.StoredProcedure);
            }
        }

        //public virtual void DeleteAll(DbTransaction transaction){
        //		Database database =DatabaseFactory.CreateDatabase();
        //		DbCommand command = database.GetStoredProcCommand("EventDeleteAll");
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


        public Events Get(Int32 eventid, string connectionString = null)
        {
            var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
            var parms = new DynamicParameters();
            parms.Add("Eventid", eventid);
            using (var connection = Database.GetNewConnection(connString, true))
            {
                return (Events)connection.Query<Events>("EventGet", parms, commandType: CommandType.StoredProcedure);
            }
        }

        public Int32 CountAll()
        {
            return Connection.Query<User_Message>("select * FROM soft_sales.users", null, commandType: CommandType.Text).Count();
        }

        public IEnumerable<Events> GetAll(string connectionString = null)
        {
            var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
            using (var connection = Database.GetNewConnection(connString, true))
            {
                return connection.Query<Events>("EventGetAll", null, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Events> GetByPk(Int32 eventid, string connectionString = null)
        {
            var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
            var parms = new DynamicParameters();
            parms.Add("Eventid", eventid);
            using (var connection = Database.GetNewConnection(connString, true))
            {
                return connection.Query<Events>("EventGetByPk", parms, commandType: CommandType.StoredProcedure);
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
        //	str_cmdview=str_cmdview+" from event";

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