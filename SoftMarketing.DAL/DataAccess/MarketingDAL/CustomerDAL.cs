using System.Data;
using SoftMarketing.Model;
using SoftMarketing.Model.MarketingModels;
using MySql.Data.MySqlClient;
using SoftMarketing.DAL.UnitOfWork;
using Dapper;
using Dapper.Contrib.Extensions;

namespace SoftMarketing.DAL.DataAccess.MarketingDAL
{
    public class CustomerDAL : DataAccessBase
    {
        public Customer Add(Customer customer)
        {
            ValidateObject(customer);
            var customerId = Connection.Insert<Customer>(customer);
            customer.id = Convert.ToInt32(customerId);
            return customer;
        }

        public List<Customer> GetAll(int userId)
        {
            var sql = "select * from marketing_user_customer where sales_userId = @userId order by id desc";
            return Connection.Query<Customer>(sql, new { userId = userId }).ToList();
        }

        public int Update(Customer item)
        {
            ValidateObject(item);
            var sql = "UPDATE marketing_user_customer SET phone_countryId = @phone_countryId,phone = @phone,name = @name,email = @email,phone_alternate = @phone_alternate,last_visit = @last_visit,birthday = @birthday,year = @year,address = @address,details = @details,common_app_1Id = @common_app_1Id,common_app_2Id = @common_app_2Id,app_2_id = @app_2_id,send_events_msg = @send_events_msg,send_feedback_msg = @send_feedback_msg,send_advertise_msg = @send_advertise_msg,send_reminder_msg = @send_reminder_msg,reminder_duration = @reminder_duration,reminder_count = @reminder_count  WHERE id = @id and sales_userId = @sales_userId";
            var result = Connection.Execute(sql, item);
            //    MySqlCommand cmd = new MySqlCommand("CustomerUpdate", Connection);
            //cmd.Connection = Connection;
            //cmd.CommandType = CommandType.StoredProcedure;
            //FillParams(cmd, item);
            //var result = cmd.ExecuteNonQuery();
            return result;
        }

        public bool Delete(int id, int userId)
        {
            //var customer = new Customer { id = id, sales_userId = userId };
            return Connection.Delete<Customer>(new Customer { id = id, sales_userId = userId });
            //var sql = "DELETE FROM marketing_user_customer WHERE id = @id and sales_userId = @userId";
            //return Connection.Execute(sql, new { id = id, sales_userId = userId });


            //MySqlCommand cmd = new MySqlCommand("customerDelete", Connection);
            //cmd.Connection = Connection;
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@ID_VAL", id);
            //cmd.Parameters["@ID_VAL"].Direction = ParameterDirection.Input;
            //cmd.Parameters.AddWithValue("@SALES_USER_ID_VAL", userId);
            //cmd.Parameters["@SALES_USER_ID_VAL"].Direction = ParameterDirection.Input;
            //var result = cmd.ExecuteNonQuery();
            //return result;
        }

        public Customer Get(int customerId, int userId)
        {
            var sql = "select * from marketing_user_customer where id = @customerId and sales_userId = @userId";
            return Connection.Query<Customer>(sql, new { customerId = customerId , userId = userId }).First();
        }

        //public List<Customer> GetAll(int userId)
        //{
        //    var sql = "select * from marketing_user_customer where sales_userId = @userId order by id desc LIMIT 1";
        //    return Connection.Query<Customer>(sql, new { userId = userId }).ToList();
        //}
        public int GetLastRecord(int userId)
        {
            var stm = "SELECT * FROM marketing_user_customer WHERE sales_userId = '" + userId + "' ORDER BY id desc LIMIT 1;";
            MySqlCommand cmd = new MySqlCommand(stm, Connection);
            cmd.Connection = Connection;
            cmd.Parameters.AddWithValue("@userId", userId);
            using MySqlDataReader rdr = cmd.ExecuteReader();
            int id = 0;
            while (rdr.Read())
            {
                id = rdr.GetInt32(0);
            }


            return id;
        }
        public void FillParams(MySqlCommand cmd, Customer item)
        {
            //in case update
            if (item.id > 0)
            {
                cmd.Parameters.AddWithValue("@ID_VAL", item.id);
                cmd.Parameters["@ID_VAL"].Direction = ParameterDirection.Input;
            }
            cmd.Parameters.AddWithValue("@SALES_USER_ID_VAL", item.sales_userId);
            cmd.Parameters["@SALES_USER_ID_VAL"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@COMMON_COUNTRY_ID", item.phone_countryId);
            cmd.Parameters["@COMMON_COUNTRY_ID"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@PHONE", item.phone);
            cmd.Parameters["@PHONE"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@PHONE_ALTERNATE", item.phone_alternate);
            cmd.Parameters["@PHONE_ALTERNATE"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@NAME", item.name);
            cmd.Parameters["@NAME"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@LAST_VISIT", "2022-04-30");
            cmd.Parameters["@LAST_VISIT"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@ADDED", "2022-04-30 00:03:44");
            cmd.Parameters["@ADDED"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("AGE_BIRTHDAY", "2022-04-30");
            cmd.Parameters["@AGE_BIRTHDAY"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@common_app_1Id", (int)item.common_app_1Id);
            cmd.Parameters["@common_app_1Id"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@common_app_2Id", item.common_app_2Id);
            cmd.Parameters["@common_app_2Id"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@REMINDER_DURATION", item.reminder_duration);
            cmd.Parameters["@REMINDER_DURATION"].Direction = ParameterDirection.Input;

            //cmd.Parameters.AddWithValue("@REMINDER_TIMES", (int)item.reminder_times);
            //cmd.Parameters["@REMINDER_TIMES"].Direction = ParameterDirection.Input;


            cmd.Parameters.AddWithValue("@ADDRESS", item.address);
            cmd.Parameters["@ADDRESS"].Direction = ParameterDirection.Input;

            cmd.Parameters.AddWithValue("@DETAILS", item.details);
            cmd.Parameters["@DETAILS"].Direction = ParameterDirection.Input;

            //cmd.Parameters.AddWithValue("@EVENTS", item.events.ToString());
            //cmd.Parameters["@EVENTS"].Direction = ParameterDirection.Input;

            //cmd.Parameters.AddWithValue("@FEEDBACK", item.feedback.ToString());
            //cmd.Parameters["@FEEDBACK"].Direction = ParameterDirection.Input;

            //cmd.Parameters.AddWithValue("@PROMOTIONS", item.promotions.ToString());
            //cmd.Parameters["@PROMOTIONS"].Direction = ParameterDirection.Input;

        }

        private void GetEntityFromReader(MySqlDataReader reader, Customer entity)
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
            if (ColumnExists(reader, "sales_userId"))
            {
                columnIndx = reader.GetOrdinal("sales_userId");
                if (!reader.IsDBNull(columnIndx))
                {
                    entity.sales_userId = Convert.ToInt32(reader[columnIndx]);
                }
            }
            if (ColumnExists(reader, "common_countryId"))
            {
                columnIndx = reader.GetOrdinal("common_countryId");
                if (!reader.IsDBNull(columnIndx))
                {
                    entity.phone_countryId = Convert.ToInt32(reader[columnIndx]);
                }
            }
            if (ColumnExists(reader, "phone"))
            {
                columnIndx = reader.GetOrdinal("phone");
                if (!reader.IsDBNull(columnIndx))
                    entity.phone = Convert.ToString(reader[columnIndx]);
            }
            if (ColumnExists(reader, "phone_alternate"))
            {
                columnIndx = reader.GetOrdinal("phone_alternate");
                if (!reader.IsDBNull(columnIndx))
                    entity.phone_alternate = Convert.ToString(reader[columnIndx]);
            }
            if (ColumnExists(reader, "last_visit"))
            {
                columnIndx = reader.GetOrdinal("last_visit");
                if (!reader.IsDBNull(columnIndx))
                    entity.last_visit = Convert.ToDateTime(reader[columnIndx]?.ToString());
            }

            if (ColumnExists(reader, "added"))
            {
                columnIndx = reader.GetOrdinal("added");
                if (!reader.IsDBNull(columnIndx))
                    entity.added = Convert.ToDateTime(reader[columnIndx]?.ToString());
            }

            if (ColumnExists(reader, "birthday"))
            {
                columnIndx = reader.GetOrdinal("birthday");
                if (!reader.IsDBNull(columnIndx))
                    entity.birthday = Convert.ToString(reader[columnIndx]);
            }

            if (ColumnExists(reader, "year"))
            {
                columnIndx = reader.GetOrdinal("age");
                if (!reader.IsDBNull(columnIndx))
                    entity.year = Convert.ToString(reader[columnIndx]);
            }

            if (ColumnExists(reader, "address"))
            {
                columnIndx = reader.GetOrdinal("address");
                if (!reader.IsDBNull(columnIndx))
                    entity.address = Convert.ToString(reader[columnIndx]);
            }

            if (ColumnExists(reader, "common_app_1Id"))
            {
                columnIndx = reader.GetOrdinal("common_app_1Id");
                if (!reader.IsDBNull(columnIndx))
                    entity.common_app_1Id = Convert.ToInt32(reader[columnIndx]);
            }

            if (ColumnExists(reader, "common_app_2Id"))
            {
                columnIndx = reader.GetOrdinal("common_app_2Id");
                if (!reader.IsDBNull(columnIndx))
                    entity.common_app_2Id = Convert.ToInt32(reader[columnIndx]);
            }
            //if (ColumnExists(reader, "events"))
            //{
            //    columnIndx = reader.GetOrdinal("events");
            //    if (!reader.IsDBNull(columnIndx))
            //    {
            //        Enum.TryParse(Convert.ToString(reader[columnIndx]), out HasTemplate hasTemplate);
            //        entity.events = hasTemplate;
            //    }
            //}

            //if (ColumnExists(reader, "feedback"))
            //{
            //    columnIndx = reader.GetOrdinal("feedback");
            //    if (!reader.IsDBNull(columnIndx))
            //    {
            //        Enum.TryParse(Convert.ToString(reader[columnIndx]), out HasTemplate hasTemplate);
            //        entity.feedback = hasTemplate;
            //    }
            //}
            //if (ColumnExists(reader, "promotions"))
            //{
            //    columnIndx = reader.GetOrdinal("promotions");
            //    if (!reader.IsDBNull(columnIndx))
            //    {
            //        Enum.TryParse(Convert.ToString(reader[columnIndx]), out HasTemplate hasTemplate);
            //        entity.promotions = hasTemplate;
            //    }
            //}
            //if (ColumnExists(reader, "reminder_duration"))
            //{
            //    columnIndx = reader.GetOrdinal("reminder_duration");
            //    if (!reader.IsDBNull(columnIndx))
            //    {
            //        Enum.TryParse(Convert.ToString(reader[columnIndx]), out ReminderDuration reminderDuration);
            //        entity.reminder_duration = reminderDuration;
            //    }
            //}
            //if (ColumnExists(reader, "reminder_times"))
            //{
            //    columnIndx = reader.GetOrdinal("reminder_times");
            //    if (!reader.IsDBNull(columnIndx))
            //    {
            //        Enum.TryParse(Convert.ToString(reader[columnIndx]), out ReminderTimes reminderTimes);
            //        entity.reminder_times = reminderTimes;
            //    }
            //}
        }

        private void ValidateObject(Customer customer)
        {
            customer.reminder_count = customer.reminder_count == string.Empty ? "0": customer.reminder_count;
            customer.reminder_duration = customer.reminder_duration == string.Empty ? null: customer.reminder_duration;
            customer.year = customer.year == string.Empty ? null: customer.year;
        } 
    }
}
