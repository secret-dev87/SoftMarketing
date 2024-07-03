using MySql.Data.MySqlClient;
using SoftMarketing.DAL.UnitOfWork;
using SoftMarketing.Model.SalesModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftMarketing.DAL.DataAccess.SalesDAL
{
    public class CenterUserDAL : DataAccessBase
    {
        /// <summary>
        ///    CenterUserDAL
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public CenterUser GetCenterUser(string phone, string pass)
        {
            //var parms = FillParams(customer);

            MySqlCommand cmd = new MySqlCommand("GET_CENTER_USER_PRC", Connection);
            cmd.Connection = Connection;
            //cmd.CommandText = "CustomerInsert";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PHONE", phone);
            cmd.Parameters["@PHONE"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@PASS", pass);
            cmd.Parameters["@PASS"].Direction = ParameterDirection.Input;

            var reader = cmd.ExecuteReader();

            var centerUser = new CenterUser();
            while (reader.Read())
            {
                GetEntityFromReader(reader, centerUser);
            }

            return centerUser;
        }

        private void GetEntityFromReader(MySqlDataReader reader, CenterUser entity)
        {
            int columnIndx = 0;
            if (ColumnExists(reader, "id"))
            {
                columnIndx = reader.GetOrdinal("id");
                if (!reader.IsDBNull(columnIndx))
                {
                    entity.Id = Convert.ToInt32(reader[columnIndx]);
                }
            }
            if (ColumnExists(reader, "centerId"))
            {
                columnIndx = reader.GetOrdinal("centerId");
                if (!reader.IsDBNull(columnIndx))
                {
                    entity.CenterId = Convert.ToInt32(reader[columnIndx]);
                }
            }
            if (ColumnExists(reader, "designationId"))
            {
                columnIndx = reader.GetOrdinal("designationId");
                if (!reader.IsDBNull(columnIndx))
                {
                    entity.DesignationId = Convert.ToInt32(reader[columnIndx]);
                }
            }
            if (ColumnExists(reader, "phone"))
            {
                columnIndx = reader.GetOrdinal("phone");
                if (!reader.IsDBNull(columnIndx))
                    entity.Phone = Convert.ToString(reader[columnIndx]);
            }
            if (ColumnExists(reader, "password"))
            {
                columnIndx = reader.GetOrdinal("password");
                if (!reader.IsDBNull(columnIndx))
                    entity.Password = Convert.ToString(reader[columnIndx]);
            }
        }
    }

}
