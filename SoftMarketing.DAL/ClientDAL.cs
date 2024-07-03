#region using directives

using System;
using System.Data;
using System.Data.Common;
using SoftMarketing.Model;
using MySql.Data.MySqlClient;
using Dapper;
using SoftMarketing.DAL.MySQL.Helper;

#endregion

namespace SoftMarketing.DAL
{

    public class ClientDAL
    {

        public ClientDAL()
        {
        }

        public DynamicParameters FillCustomerParams(Client client)
        {
            var parms = new DynamicParameters();
            parms.Add("clientid", client.ClientId);
            parms.Add("Customerid", client.UserId);
            parms.Add("firstname", client.FirstName);
            parms.Add("middleinitial", client.MiddleInitial);
            parms.Add("lastname", client.LastName);
            parms.Add("Contactnumber", client.ContactNumber);
            parms.Add("Alternatenumber", client.AlternateNumber);
            parms.Add("Email", client.Email);
            parms.Add("Lastvisit", client.LastName);
            parms.Add("Dateadded", client.DateAdded);
            parms.Add("Addedby", client.AddedBy);
            parms.Add("Dateupdated", client.DateUpdated);
            parms.Add("Updatedby", client.UpdatedBy);
            return parms;
        }


        public Int32 Add(Client client, DbTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException("trans");
            var parms = FillCustomerParams(client);
            var connection = transaction.Connection;
            var data = connection.Query<Client>("ClientInsert", new
            {
                parms,
            }, transaction: transaction, commandType: CommandType.StoredProcedure);

            return data.Count();
        }

        public Int32 Update(Client client, DbTransaction transaction)
        {
            var parms = FillCustomerParams(client);
            var connection = transaction.Connection;
            var data = connection.Query<Client>("ClientUpdate", new
            {
                parms,
            }, transaction: transaction, commandType: CommandType.StoredProcedure);

            return data.FirstOrDefault().ClientId;
        }

        public void Delete(Int32 clientId, DbTransaction transaction)
        {
            var parms = new DynamicParameters();
            parms.Add("clientid", clientId);
            var connection = transaction.Connection;
            var data = connection.Query<Client>("ClientDelete", new
            {
                parms,
            }, transaction: transaction, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Client> GetByClientID(Int32 clientId, string connectionString = null)
        {
            var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
            var parms = new DynamicParameters();
            parms.Add("clientid", clientId);
            using (var connection = Database.GetNewConnection(connString, true))
            {
                return connection.Query<Client>("[dbo].[ClientGet]", parms, commandType: CommandType.StoredProcedure);
            }
        }

        public virtual List<Client> GetByCustomerId(Int32 customerId, string connectionString = null)
        {
            var connString = connectionString ?? MySqlConnectionHelper.ConnectionString;
            var parms = new DynamicParameters();
            parms.Add("customerid", customerId);

            using (var connection = Database.GetNewConnection(connString, true))
            {
                var result = connection.Query<Client>("[dbo].[ClientGetByCustomerId]", new
                {
                    parms,
                }, commandType: CommandType.StoredProcedure);

                return (List<Client>)result;
            }
        }


    }
}