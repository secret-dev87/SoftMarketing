using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using SoftMarketing.Model;
using MySql.Data.MySqlClient;
using Dapper;
using SoftMarketing.DAL.MySQL.Helper;
using SoftMarketing.DAL.UnitOfWork;
using Newtonsoft.Json;
using SoftMarketing.Model.SalesModels;
using Dapper.Contrib.Extensions;


namespace SoftMarketing.DAL.DataAccess.MarketingDAL
{

    public class UserDAL : DataAccessBase
    {


        public string Add(User item)
        {
            MySqlCommand cmd = new MySqlCommand("SalesUserInsert", Connection);
            cmd.Connection = Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            FillParams(cmd, item);
            return cmd.ExecuteScalar().ToString();
        }
        public string Register(User item)
        {
            MySqlCommand cmd = new MySqlCommand("Register", Connection);
            cmd.Connection = Connection;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@phone_param", item.phone);
            cmd.Parameters["@phone_param"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@hash_pass_param", item.hash_password);
            cmd.Parameters["@hash_pass_param"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@salt_param", item.salt);
            cmd.Parameters["@salt_param"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@agent_sales_userId_param", item.agent_sales_userId);
            cmd.Parameters["@agent_sales_userId_param"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@name_param", item.name);
            cmd.Parameters["@name_param"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@planId_param", item.sales_planId);
            cmd.Parameters["@planId_param"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@phone_countryId_param", item.phone_countryId);
            cmd.Parameters["@phone_countryId_param"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@pt_param", item.pt);
            cmd.Parameters["@pt_param"].Direction = ParameterDirection.Input;
            return cmd.ExecuteScalar().ToString();
        }

        public int UpdateHubConnections(int userId, int appSourceId, string connectionId, RefreshToken refreshToken)
        {

            string sqlCommand = string.Empty;
            switch (appSourceId)
            {
                case 1:
                    sqlCommand = "UPDATE sales_user SET win_con_id=@connectionId where id=@userId";
                    break;
                case 2:
                    sqlCommand = "UPDATE sales_user SET mob_con_id=@connectionId where id=@userId";
                    break;
                case 3:
                    sqlCommand = "UPDATE sales_user SET web_con_id=@connectionId where id=@userId";
                    break;
                default:
                    // code block
                    break;
            }
            var result = Connection.Execute(sqlCommand, new { userId = userId, connectionId = connectionId });
            return result;
        }
        public int SetCustomerTS(int userId, string timestamp)
        {
            var sqlCommand = "UPDATE sales_user SET last_update_customer=@timestamp where id=@userId";
            var result = Connection.Execute(sqlCommand, new { userId = userId, timestamp = timestamp });
            return result;
        }
        public int SetMessageTS(int userId, string timestamp)
        {
            var sqlCommand = "UPDATE sales_user SET last_update_message=@timestamp where id=@userId";
            var result = Connection.Execute(sqlCommand, new { userId = userId, timestamp = timestamp });
            return result;
        }
        public int SetSettingTS(int userId, string timestamp)
        {
            var sqlCommand = "UPDATE sales_user SET last_update_setting=@timestamp where id=@userId";
            var result = Connection.Execute(sqlCommand, new { userId = userId, timestamp = timestamp });
            return result;
        }
        public int SetTemplateTS(int userId, string timestamp)
        {
            var sqlCommand = "UPDATE sales_user SET last_update_template=@timestamp where id=@userId";
            var result = Connection.Execute(sqlCommand, new { userId = userId, timestamp = timestamp });
            return result;
        }
        public int UpdateRefreshToken(RefreshToken rToken)
        {
            MySqlCommand cmd = new MySqlCommand("refresh_token_update", Connection);
            cmd.Connection = Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            FillParams(cmd, rToken);
            return cmd.ExecuteNonQuery();
        }
        public int UpdateOTP(int userId, string OTP)
        {
            var sqlCommand = "UPDATE sales_user SET otp = @OTP where id = @id";
            return Connection.Execute(sqlCommand, new { OTP, id = userId });
        }

        public string AddRefreshToken(RefreshToken rToken)
        {
            MySqlCommand cmd = new MySqlCommand("refresh_token_insert", Connection);
            cmd.Connection = Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            FillParams(cmd, rToken);
            return cmd.ExecuteScalar().ToString();
        }
        public int RemoveRefreshTokenByTokenId(int tokenId)
        {
            MySqlCommand cmd = new MySqlCommand("refresh_token_delete", Connection);
            cmd.Connection = Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id_pram", tokenId);
            cmd.Parameters["@id_pram"].Direction = ParameterDirection.Input;
            var result = cmd.ExecuteNonQuery();
            return result;
        }
        public int RemoveRefreshTokenByUserId(int userId)
        {
            var sqlCommand = "DELETE FROM common_refresh_token where userId = @value";
            return Connection.Execute(sqlCommand, new { value = userId });
        }
        public User Get(string phone, int countryId)
        {

            MySqlCommand cmd = new MySqlCommand("sales_user_by_phone_countryId_otp", Connection);
            cmd.Connection = Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@countryId_pram", countryId);
            cmd.Parameters["@countryId_pram"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@phone_pram", phone);
            cmd.Parameters["@phone_pram"].Direction = ParameterDirection.Input;
            //cmd.Parameters.AddWithValue("@otp_pram", otp);
            //cmd.Parameters["@otp_pram"].Direction = ParameterDirection.Input;
            var reader = cmd.ExecuteReader();
            var user = new User();
            while (reader.Read())
            {
                GetEntityFromReader(reader, user);
            }
            return user;
        }
        public User GetSalesUserAndRefreshTokensByToken(string token)
        {
            MySqlCommand cmd = new MySqlCommand("sales_user_by_refresh_token", Connection);
            cmd.Connection = Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@token_pram", token);
            cmd.Parameters["@token_pram"].Direction = ParameterDirection.Input;
            var reader = cmd.ExecuteReader();
            var user = new User();
            while (reader.Read())
            {
                GetEntityFromReader(reader, user);
            }
            return user;
        }
        public User GetSalesUserAndRefreshTokensByUserId(int userId)
        {
            MySqlCommand cmd = new MySqlCommand("sales_user_and_his_tokens_by_userId", Connection);
            cmd.Connection = Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@userId_pram", userId);
            cmd.Parameters["@userId_pram"].Direction = ParameterDirection.Input;
            var reader = cmd.ExecuteReader();
            var user = new User();
            while (reader.Read())
            {
                GetEntityFromReader(reader, user);
            }
            return user;
        }
        public User GetById(int id)
        {
            MySqlCommand cmd = new MySqlCommand("sales_user_by_id", Connection);
            cmd.Connection = Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id_pram", id);
            cmd.Parameters["@id_pram"].Direction = ParameterDirection.Input;
            var reader = cmd.ExecuteReader();
            var user = new User();
            while (reader.Read())
            {
                GetEntityFromReader(reader, user);
            }
            return user;
        }
        public User GetByRefreshToken(string rToken)
        {
            MySqlCommand cmd = new MySqlCommand("sales_user_by_refresh_token", Connection);
            cmd.Connection = Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@token_pram", rToken);
            cmd.Parameters["@token_pram"].Direction = ParameterDirection.Input;
            var reader = cmd.ExecuteReader();
            var user = new User();
            while (reader.Read())
            {
                GetEntityFromReader(reader, user);
            }
            return user;
        }
        public RefreshToken GetRTokenByToken(string token)
        {
            MySqlCommand cmd = new MySqlCommand("refresh_token_by_token", Connection);
            cmd.Connection = Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@token_pram", token);
            cmd.Parameters["@token_pram"].Direction = ParameterDirection.Input;
            var reader = cmd.ExecuteReader();
            var RefreshToken = new RefreshToken();
            while (reader.Read())
            {
                GetEntityFromReader(reader, RefreshToken);
            }
            return RefreshToken;
        }
        public List<RefreshToken> GetRefreshTokensByUserId(int userId)
        {
            MySqlCommand cmd = new MySqlCommand("refresh_token_by_userId", Connection);
            cmd.Connection = Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@userId_pram", userId);
            cmd.Parameters["@userId_pram"].Direction = ParameterDirection.Input;
            var reader = cmd.ExecuteReader();
            List<RefreshToken> refreshTokens = new List<RefreshToken>();
            while (reader.Read())
            {
                var rToken = new RefreshToken();
                GetEntityFromReader(reader, rToken);
                refreshTokens.Add(rToken);

            }
            return refreshTokens;
        }
        public List<RefreshToken> GetRefreshTokensByUserIdAndConnectionId(int userId, string connId)
        {
            MySqlCommand cmd = new MySqlCommand("refresh_token_by_userId_and_connId", Connection);
            cmd.Connection = Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@userId_pram", userId);
            cmd.Parameters["@userId_pram"].Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@connId_pram", connId);
            cmd.Parameters["@connId_pram"].Direction = ParameterDirection.Input;
            var reader = cmd.ExecuteReader();
            List<RefreshToken> refreshTokens = new List<RefreshToken>();
            while (reader.Read())
            {
                var rToken = new RefreshToken();
                GetEntityFromReader(reader, rToken);
                refreshTokens.Add(rToken);

            }
            return refreshTokens;
        }
        public int UpdateLastUpdateCustomer(int userId)
        {
            MySqlCommand cmd = new MySqlCommand("sp_last_update_customer", Connection);
            cmd.Connection = Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SALES_USER_ID_VAL", userId);
            cmd.Parameters["@SALES_USER_ID_VAL"].Direction = ParameterDirection.Input;
            var result = cmd.ExecuteNonQuery();
            return result;
        }
        public int UpdateLastUpdateMessage(int userId)
        {
            MySqlCommand cmd = new MySqlCommand("sp_last_update_message", Connection);
            cmd.Connection = Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SALES_USER_ID_VAL", userId);
            cmd.Parameters["@SALES_USER_ID_VAL"].Direction = ParameterDirection.Input;
            var result = cmd.ExecuteNonQuery();
            return result;
        }
        public int UpdateLastUpdateSetting(int userId)
        {
            MySqlCommand cmd = new MySqlCommand("sp_last_update_setting", Connection);
            cmd.Connection = Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SALES_USER_ID_VAL", userId);
            cmd.Parameters["@SALES_USER_ID_VAL"].Direction = ParameterDirection.Input;
            var result = cmd.ExecuteNonQuery();
            return result;
        }
        public int UpdateLastUpdateTemplate(int userId)
        {
            MySqlCommand cmd = new MySqlCommand("sp_last_update_template", Connection);
            cmd.Connection = Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SALES_USER_ID_VAL", userId);
            cmd.Parameters["@SALES_USER_ID_VAL"].Direction = ParameterDirection.Input;
            var result = cmd.ExecuteNonQuery();
            return result;
        }
        public void FillParams(MySqlCommand cmd, RefreshToken item)
        {
            //in case update
            if (item.Id > 0)
            {
                cmd.Parameters.AddWithValue("@id_pram", item.Id);
                cmd.Parameters["@id_pram"].Direction = ParameterDirection.Input;
            }
            cmd.Parameters.AddWithValue("@userId_pram", item.UserId);
            cmd.Parameters["@userId_pram"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@token_pram", item.Token);
            cmd.Parameters["@token_pram"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@expires_pram", item.Expires);
            cmd.Parameters["@expires_pram"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@created_pram", item.Created);
            cmd.Parameters["@created_pram"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@createdByIp_pram", item.CreatedByIp);
            cmd.Parameters["@createdByIp_pram"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@revoked_pram", item.Revoked);
            cmd.Parameters["@revoked_pram"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@revokedByIp_pram", item.RevokedByIp);
            cmd.Parameters["@revokedByIp_pram"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@replacedByToken_pram", item.ReplacedByToken);
            cmd.Parameters["@replacedByToken_pram"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@resonRevoked_pram", item.ReasonRevoked);
            cmd.Parameters["@resonRevoked_pram"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@connection_id_pram", item.ConnectionId);
            cmd.Parameters["@connection_id_pram"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@app_source_id_pram", item.AppSourceId);
            cmd.Parameters["@app_source_id_pram"].Direction = ParameterDirection.Input;

        }
        private void GetEntityFromReader(MySqlDataReader reader, RefreshToken entity)
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
            if (ColumnExists(reader, "token"))
            {
                columnIndx = reader.GetOrdinal("token");
                if (!reader.IsDBNull(columnIndx))
                {
                    entity.Token = Convert.ToString(reader[columnIndx]);
                }
            }
            if (ColumnExists(reader, "expires"))
            {
                columnIndx = reader.GetOrdinal("expires");
                if (!reader.IsDBNull(columnIndx))
                {
                    entity.Expires = Convert.ToDateTime(reader[columnIndx]);
                }
            }
            if (ColumnExists(reader, "created"))
            {
                columnIndx = reader.GetOrdinal("created");
                if (!reader.IsDBNull(columnIndx))
                {
                    entity.Created = Convert.ToDateTime(reader[columnIndx]);
                }
            }
            if (ColumnExists(reader, "createdByIp"))
            {
                columnIndx = reader.GetOrdinal("createdByIp");
                if (!reader.IsDBNull(columnIndx))
                {
                    entity.CreatedByIp = Convert.ToString(reader[columnIndx]);
                }
            }
            if (ColumnExists(reader, "revoked"))
            {
                columnIndx = reader.GetOrdinal("revoked");
                if (!reader.IsDBNull(columnIndx))
                {
                    entity.Revoked = Convert.ToDateTime(reader[columnIndx]);
                }
            }
            if (ColumnExists(reader, "revokedByIp"))
            {
                columnIndx = reader.GetOrdinal("revokedByIp");
                if (!reader.IsDBNull(columnIndx))
                {
                    entity.RevokedByIp = Convert.ToString(reader[columnIndx]);
                }
            }

            if (ColumnExists(reader, "replacedByToken"))
            {
                columnIndx = reader.GetOrdinal("replacedByToken");
                if (!reader.IsDBNull(columnIndx))
                    entity.ReplacedByToken = Convert.ToString(reader[columnIndx]);
            }
            if (ColumnExists(reader, "resonRevoked"))
            {
                columnIndx = reader.GetOrdinal("resonRevoked");
                if (!reader.IsDBNull(columnIndx))
                    entity.ReasonRevoked = Convert.ToString(reader[columnIndx]);
            }
            if (ColumnExists(reader, "appSourceId"))
            {
                columnIndx = reader.GetOrdinal("appSourceId");
                if (!reader.IsDBNull(columnIndx))
                    entity.AppSourceId = Convert.ToInt16(reader[columnIndx]);
            }
            if (ColumnExists(reader, "connectionId"))
            {
                columnIndx = reader.GetOrdinal("connectionId");
                if (!reader.IsDBNull(columnIndx))
                    entity.ConnectionId = Convert.ToString(reader[columnIndx]);
            }
        }
        private void GetEntityFromReader(MySqlDataReader reader, User entity)
        {
            int columnIndx = 0;
            if (ColumnExists(reader, "id"))
            {
                columnIndx = reader.GetOrdinal("id");
                if (!reader.IsDBNull(columnIndx))
                {
                    entity.id = Convert.ToInt32(reader[columnIndx]);
                }
            }

            if (ColumnExists(reader, "message_last_month"))
            {
                columnIndx = reader.GetOrdinal("message_last_month");
                if (!reader.IsDBNull(columnIndx))
                {
                    entity.message_last_month = Convert.ToInt32(reader[columnIndx]);
                }
            }

            if (ColumnExists(reader, "referred_by_sales_userId"))
            {
                columnIndx = reader.GetOrdinal("referred_by_sales_userId");
                if (!reader.IsDBNull(columnIndx))
                {
                    entity.referred_by_sales_userId = Convert.ToInt32(reader[columnIndx]);
                }
            }
            if (ColumnExists(reader, "agent_sales_userId"))
            {
                columnIndx = reader.GetOrdinal("agent_sales_userId");
                if (!reader.IsDBNull(columnIndx))
                {
                    entity.agent_sales_userId = Convert.ToInt32(reader[columnIndx]);
                }
            }
            if (ColumnExists(reader, "message_total"))
            {
                columnIndx = reader.GetOrdinal("message_total");
                if (!reader.IsDBNull(columnIndx))
                {
                    entity.message_total = Convert.ToInt32(reader[columnIndx]);
                }
            }
            if (ColumnExists(reader, "phone"))
            {
                columnIndx = reader.GetOrdinal("phone");
                if (!reader.IsDBNull(columnIndx))
                    entity.phone = Convert.ToString(reader[columnIndx]);
            }
            if (ColumnExists(reader, "phone_countryId"))
            {
                columnIndx = reader.GetOrdinal("phone_countryId");
                if (!reader.IsDBNull(columnIndx))
                {
                    entity.phone_countryId = Convert.ToInt32(reader[columnIndx]);
                }
            }
            if (ColumnExists(reader, "web_con_id"))
            {
                columnIndx = reader.GetOrdinal("web_con_id");
                if (!reader.IsDBNull(columnIndx))
                    entity.name = Convert.ToString(reader[columnIndx]);
            }
            if (ColumnExists(reader, "mob_con_id"))
            {
                columnIndx = reader.GetOrdinal("mob_con_id");
                if (!reader.IsDBNull(columnIndx))
                    entity.name = Convert.ToString(reader[columnIndx]);
            }
            if (ColumnExists(reader, "win_con_id"))
            {
                columnIndx = reader.GetOrdinal("win_con_id");
                if (!reader.IsDBNull(columnIndx))
                    entity.win_con_id = Convert.ToString(reader[columnIndx]);
            }

            if (ColumnExists(reader, "name"))
            {
                columnIndx = reader.GetOrdinal("name");
                if (!reader.IsDBNull(columnIndx))
                    entity.name = Convert.ToString(reader[columnIndx]);
            }
            if (ColumnExists(reader, "user_statusId"))
            {
                columnIndx = reader.GetOrdinal("user_statusId");
                if (!reader.IsDBNull(columnIndx))
                    entity.user_statusId = Convert.ToInt16(reader[columnIndx]);
            }
            if (ColumnExists(reader, "note"))
            {
                columnIndx = reader.GetOrdinal("note");
                if (!reader.IsDBNull(columnIndx))
                    entity.note = Convert.ToString(reader[columnIndx]);
            }
            if (ColumnExists(reader, "sales_centerId"))
            {
                columnIndx = reader.GetOrdinal("sales_centerId");
                if (!reader.IsDBNull(columnIndx))
                {
                    entity.sales_centerId = Convert.ToInt32(reader[columnIndx]);
                }
            }
            if (ColumnExists(reader, "sales_planId"))
            {
                columnIndx = reader.GetOrdinal("sales_planId");
                if (!reader.IsDBNull(columnIndx))
                {
                    entity.sales_planId = Convert.ToInt32(reader[columnIndx]);
                }
            }
            if (ColumnExists(reader, "added_on"))
            {
                columnIndx = reader.GetOrdinal("added_on");
                if (!reader.IsDBNull(columnIndx))
                    entity.added_on = Convert.ToString(reader[columnIndx]);
            }
            if (ColumnExists(reader, "message_last_month"))
            {
                columnIndx = reader.GetOrdinal("message_last_month");
                if (!reader.IsDBNull(columnIndx))
                    entity.message_last_month = Convert.ToInt32(reader[columnIndx]);
            }
            if (ColumnExists(reader, "plan_start"))
            {
                columnIndx = reader.GetOrdinal("plan_start");
                if (!reader.IsDBNull(columnIndx))
                    entity.plan_start = Convert.ToString(reader[columnIndx]);
            }
            if (ColumnExists(reader, "otp"))
            {
                columnIndx = reader.GetOrdinal("otp");
                if (!reader.IsDBNull(columnIndx))
                    entity.otp = Convert.ToString(reader[columnIndx]);
            }
            if (ColumnExists(reader, "token"))
            {
                columnIndx = reader.GetOrdinal("token");
                if (!reader.IsDBNull(columnIndx))
                    entity.token = Convert.ToString(reader[columnIndx]);
            }
            if (ColumnExists(reader, "otp_time"))
            {
                columnIndx = reader.GetOrdinal("otp_time");
                if (!reader.IsDBNull(columnIndx))
                    entity.otp_time = Convert.ToDateTime(reader[columnIndx]);
            }
            if (ColumnExists(reader, "payment_mode"))
            {
                columnIndx = reader.GetOrdinal("payment_mode");
                if (!reader.IsDBNull(columnIndx))
                    entity.payment_mode = Convert.ToString(reader[columnIndx]);
            }
            if (ColumnExists(reader, "otp_count"))
            {
                columnIndx = reader.GetOrdinal("otp_count");
                if (!reader.IsDBNull(columnIndx))
                {
                    entity.otp_count = Convert.ToInt32(reader[columnIndx]);
                }
            }
            if (ColumnExists(reader, "last_update_template"))
            {
                columnIndx = reader.GetOrdinal("last_update_template");
                if (!reader.IsDBNull(columnIndx))
                {
                    entity.last_update_template = Convert.ToString(reader[columnIndx]);
                }
            }
            if (ColumnExists(reader, "last_update_customer"))
            {
                columnIndx = reader.GetOrdinal("last_update_customer");
                if (!reader.IsDBNull(columnIndx))
                {
                    entity.last_update_customer = Convert.ToString(reader[columnIndx]);
                }
            }
            if (ColumnExists(reader, "last_update_message"))
            {
                columnIndx = reader.GetOrdinal("last_update_message");
                if (!reader.IsDBNull(columnIndx))
                {
                    entity.last_update_message = Convert.ToString(reader[columnIndx]);
                }
            }
            if (ColumnExists(reader, "last_update_setting"))
            {
                columnIndx = reader.GetOrdinal("last_update_setting");
                if (!reader.IsDBNull(columnIndx))
                {
                    entity.last_update_setting = Convert.ToString(reader[columnIndx]);
                }
            }
            if (ColumnExists(reader, "last_update_template_date"))
            {
                columnIndx = reader.GetOrdinal("last_update_template_date");
                if (!reader.IsDBNull(columnIndx))
                {
                    entity.last_update_template_date = Convert.ToString(reader[columnIndx]);
                }
            }
            if (ColumnExists(reader, "hiring_source"))
            {
                columnIndx = reader.GetOrdinal("hiring_source");
                if (!reader.IsDBNull(columnIndx))
                {
                    Enum.TryParse(Convert.ToString(reader[columnIndx]), out Hiring hiring);
                    entity.hiring_source = hiring;
                }
            }
            if (ColumnExists(reader, "rTokens"))
            {
                columnIndx = reader.GetOrdinal("rTokens");
                if (!reader.IsDBNull(columnIndx))
                {
                    var rTokensStr = Convert.ToString(reader[columnIndx]);
                    entity.RefreshTokens = new List<RefreshToken>();
                    entity.RefreshTokens = JsonConvert.DeserializeObject<List<RefreshToken>>(rTokensStr);
                }
            }
            if (ColumnExists(reader, "hash_password"))
            {
                columnIndx = reader.GetOrdinal("hash_password");
                if (!reader.IsDBNull(columnIndx))
                {
                    entity.hash_password = Convert.ToString(reader[columnIndx]);
                }
            }
            if (ColumnExists(reader, "salt"))
            {
                columnIndx = reader.GetOrdinal("salt");
                if (!reader.IsDBNull(columnIndx))
                {
                    entity.salt = (byte[])(reader[columnIndx]);
                }
            }
        }

        public void FillParams(MySqlCommand cmd, User item)
        {
            //in case update
            //if (item.id > 0)
            //{
            //    cmd.Parameters.AddWithValue("@ID_VAL", item.id);
            //    cmd.Parameters["@ID_VAL"].Direction = ParameterDirection.Input;
            //}
            //cmd.Parameters.AddWithValue("@PHONE", item.phone);
            //cmd.Parameters["@PHONE"].Direction = ParameterDirection.Input;

            //cmd.Parameters.AddWithValue("@NAME", item.name);
            //cmd.Parameters["@NAME"].Direction = ParameterDirection.Input;

            //cmd.Parameters.AddWithValue("@COUNTRYID", item.countryId);
            //cmd.Parameters["@COUNTRYID"].Direction = ParameterDirection.Input;

            //cmd.Parameters.AddWithValue("@NEXT_CALL", DateTime.Now.ToString("yyyyMMddHHmmss")/*item.next_call*/);
            //cmd.Parameters["@NEXT_CALL"].Direction = ParameterDirection.Input;

            //cmd.Parameters.AddWithValue("@SALES_USER_REFERRED_BY_ID", item.referred_by_sales_userId);
            //cmd.Parameters["@SALES_USER_REFERRED_BY_ID"].Direction = ParameterDirection.Input;

            //cmd.Parameters.AddWithValue("@LAST_CALL", DateTime.Now.ToString("yyyyMMddHHmmss")/*item.last_call*/);
            //cmd.Parameters["@LAST_CALL"].Direction = ParameterDirection.Input;

            //cmd.Parameters.AddWithValue("@VISITED", "2022-04-30");
            //cmd.Parameters["@VISITED"].Direction = ParameterDirection.Input;

            //cmd.Parameters.AddWithValue("@VISIT_PURPOSE", item.visit_purpose);
            //cmd.Parameters["@VISIT_PURPOSE"].Direction = ParameterDirection.Input;

            //cmd.Parameters.AddWithValue("@DETAILS", "2022-04-30 00:03:44");
            //cmd.Parameters["@DETAILS"].Direction = ParameterDirection.Input;

            //cmd.Parameters.AddWithValue("SALES_USER_AGENT_ID", item.sales_user_agentId);
            //cmd.Parameters["@SALES_USER_AGENT_ID"].Direction = ParameterDirection.Input;

            //cmd.Parameters.AddWithValue("@ADDED_ON", DateTime.Now.ToString("yyyyMMddHHmmss") /*item.added_on*/);
            //cmd.Parameters["@ADDED_ON"].Direction = ParameterDirection.Input;

            //cmd.Parameters.AddWithValue("@SALES_PLAN_ID", item.sales_planId);
            //cmd.Parameters["@SALES_PLAN_ID"].Direction = ParameterDirection.Input;

            //cmd.Parameters.AddWithValue("@PLAN_START_DATE", DateTime.Now.ToString("yyyyMMddHHmmss")/*item.plan_start_date*/);
            //cmd.Parameters["@PLAN_START_DATE"].Direction = ParameterDirection.Input;

            //cmd.Parameters.AddWithValue("@BLOCKED", item.blocked);
            //cmd.Parameters["@BLOCKED"].Direction = ParameterDirection.Input;

            //cmd.Parameters.AddWithValue("@OTP", item.otp);
            //cmd.Parameters["@OTP"].Direction = ParameterDirection.Input;

            //cmd.Parameters.AddWithValue("@OTP_TIME", DateTime.Now.ToString("yyyyMMddHHmmss")/*item.otp_time*/);
            //cmd.Parameters["@OTP_TIME"].Direction = ParameterDirection.Input;

            //cmd.Parameters.AddWithValue("@OTP_COUNT", item.otp_count);
            //cmd.Parameters["@OTP_COUNT"].Direction = ParameterDirection.Input;

            //cmd.Parameters.AddWithValue("@TOKEN", item.token);
            //cmd.Parameters["@TOKEN"].Direction = ParameterDirection.Input;

            //cmd.Parameters.AddWithValue("@PAYMENT_MODE", item.payment_mode);
            //cmd.Parameters["@PAYMENT_MODE"].Direction = ParameterDirection.Input;

            //cmd.Parameters.AddWithValue("@SALES_CENTER_NAME_ID", item.sales_centerId);
            //cmd.Parameters["@SALES_CENTER_NAME_ID"].Direction = ParameterDirection.Input;

            //cmd.Parameters.AddWithValue("@HIRING", item.hiring_source);
            //cmd.Parameters["@HIRING"].Direction = ParameterDirection.Input;

            //cmd.Parameters.AddWithValue("@last_update_customer", item.last_update_customer);
            //cmd.Parameters["@last_update_customer"].Direction = ParameterDirection.Input;

            //cmd.Parameters.AddWithValue("@last_update_message", item.last_update_message);
            //cmd.Parameters["@last_update_message"].Direction = ParameterDirection.Input;

            //cmd.Parameters.AddWithValue("@last_update_setting", item.last_update_setting);
            //cmd.Parameters["@last_update_setting"].Direction = ParameterDirection.Input;

            //cmd.Parameters.AddWithValue("@last_update_template", item.last_update_template);
            //cmd.Parameters["@last_update_template"].Direction = ParameterDirection.Input;
        }
    }
}