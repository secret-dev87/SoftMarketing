//#region using directives
//using System.Data;
//using SoftMarketing.Model;
//using SoftMarketing.Model.MarketingModels;
//using MySql.Data.MySqlClient;
//using SoftMarketing.DAL.UnitOfWork;
//#endregion

//namespace SoftMarketing.DAL.DataAccess.MarketingDAL
//{
//    public class WhatsappDAL : DataAccessBase
//    {
//        public string Add(Whatsapp item)
//        {
//            MySqlCommand cmd = new MySqlCommand("WhatsappInsert", Connection);
//            cmd.Connection = Connection;
//            cmd.CommandType = CommandType.StoredProcedure;
//            FillParams(cmd, item);
//            return cmd.ExecuteScalar().ToString();
//        }
//        public int Update(Whatsapp item)
//        {
//            MySqlCommand cmd = new MySqlCommand("WhatsappUpdate", Connection);
//            cmd.Connection = Connection;
//            cmd.CommandType = CommandType.StoredProcedure;
//            FillParams(cmd, item);
//            var result = cmd.ExecuteNonQuery();
//            return result;
//        }
//        public int Delete(int id, int userId)
//        {
//            MySqlCommand cmd = new MySqlCommand("WhatsappDelete", Connection);
//            cmd.Connection = Connection;
//            cmd.CommandType = CommandType.StoredProcedure;
//            cmd.Parameters.AddWithValue("@ID_VAL", id);
//            cmd.Parameters["@ID_VAL"].Direction = ParameterDirection.Input;
//            cmd.Parameters.AddWithValue("@SALES_USER_ID_VAL", userId);
//            cmd.Parameters["@SALES_USER_ID_VAL"].Direction = ParameterDirection.Input;
//            var result = cmd.ExecuteNonQuery();
//            return result;
//        }
//        public Whatsapp Get(int userId)
//        {

//            MySqlCommand cmd = new MySqlCommand("WhatsappGetByUserId", Connection);
//            cmd.Connection = Connection;
//            cmd.CommandType = CommandType.StoredProcedure;
//            cmd.Parameters.AddWithValue("@ID_VAL", userId);
//            cmd.Parameters["@ID_VAL"].Direction = ParameterDirection.Input;
//            var reader = cmd.ExecuteReader();
//            var whatsapp = new Whatsapp();
//            while (reader.Read())
//            {
//                GetEntityFromReader(reader, whatsapp);
//            }
//            return whatsapp;
//        }
//        public List<Whatsapp> GetAll()
//        {

//            MySqlCommand cmd = new MySqlCommand("WhatsappGetAll", Connection);
//            cmd.Connection = Connection;
//            cmd.CommandType = CommandType.StoredProcedure;
//            var reader = cmd.ExecuteReader();
//            List<Whatsapp> whatsapps = new List<Whatsapp>();
//            while (reader.Read())
//            {
//                Whatsapp whatsapp = new Whatsapp();
//                GetEntityFromReader(reader, whatsapp);
//                whatsapps.Add(whatsapp);
//            }
//            return whatsapps;
//        }
//        public void FillParams(MySqlCommand cmd, Whatsapp item)
//        {
//            //in case update
//            if (item.Id > 0)
//            {
//                cmd.Parameters.AddWithValue("@ID_VAL", item.Id);
//                cmd.Parameters["@ID_VAL"].Direction = ParameterDirection.Input;
//            }
//            cmd.Parameters.AddWithValue("@USER_ID", item.UserId);
//            cmd.Parameters["@USER_ID"].Direction = ParameterDirection.Input;

//            cmd.Parameters.AddWithValue("@API_KEY", item.ApiKey);
//            cmd.Parameters["@API_KEY"].Direction = ParameterDirection.Input;

//            cmd.Parameters.AddWithValue("@SESSION_PATH", item.SessionPath);
//            cmd.Parameters["@SESSION_PATH"].Direction = ParameterDirection.Input;

//            cmd.Parameters.AddWithValue("@STATUS", item.Status);
//            cmd.Parameters["@STATUS"].Direction = ParameterDirection.Input;

//            cmd.Parameters.AddWithValue("@PROCESS_ID", item.ProcessId);
//            cmd.Parameters["@PROCESS_ID"].Direction = ParameterDirection.Input;

//            cmd.Parameters.AddWithValue("@ACTIVE", item.Active);
//            cmd.Parameters["@ACTIVE"].Direction = ParameterDirection.Input;
            

//        }
//        private void GetEntityFromReader(MySqlDataReader reader, Whatsapp entity)
//        {

//            int columnIndx = 0;
//            if (ColumnExists(reader, "id"))
//            {
//                columnIndx = reader.GetOrdinal("id");
//                if (!reader.IsDBNull(columnIndx))
//                {
//                    entity.Id = Convert.ToInt32(reader[columnIndx]);
//                }
//            }
//            if (ColumnExists(reader, "user_id"))
//            {
//                columnIndx = reader.GetOrdinal("user_id");
//                if (!reader.IsDBNull(columnIndx))
//                {
//                    entity.UserId = Convert.ToInt32(reader[columnIndx]);
//                }
//            }
//            if (ColumnExists(reader, "api_key"))
//            {
//                columnIndx = reader.GetOrdinal("api_key");
//                if (!reader.IsDBNull(columnIndx))
//                {
//                    entity.ApiKey = Convert.ToString(reader[columnIndx]);
//                }
//            }
//            if (ColumnExists(reader, "session_path"))
//            {
//                columnIndx = reader.GetOrdinal("session_path");
//                if (!reader.IsDBNull(columnIndx))
//                    entity.SessionPath = Convert.ToString(reader[columnIndx]);
//            }
//            if (ColumnExists(reader, "status"))
//            {
//                columnIndx = reader.GetOrdinal("status");
//                if (!reader.IsDBNull(columnIndx))
//                    entity.Status = Convert.ToString(reader[columnIndx]);
//            }

//            if (ColumnExists(reader, "process_id"))
//            {
//                columnIndx = reader.GetOrdinal("process_id");
//                if (!reader.IsDBNull(columnIndx))
//                    entity.ProcessId = Convert.ToInt32(reader[columnIndx]);
//            }
//            if (ColumnExists(reader, "name"))
//            {
//                columnIndx = reader.GetOrdinal("name");
//                if (!reader.IsDBNull(columnIndx))
//                    entity.Name = reader[columnIndx].ToString();
//            }
//            if (ColumnExists(reader, "phone"))
//            {
//                columnIndx = reader.GetOrdinal("phone");
//                if (!reader.IsDBNull(columnIndx))
//                    entity.Phone = Convert.ToString(reader[columnIndx]);
//            }

//            if (ColumnExists(reader, "active"))
//            {
//                columnIndx = reader.GetOrdinal("active");
//                if (!reader.IsDBNull(columnIndx))
//                    entity.Active = Convert.ToInt32(reader[columnIndx]);
//            }
//        }
//    }
//}
