using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using System.Xml.Linq;

namespace SoftMarketing.Model.SalesModels
{
    public class paymentData
    {
        public string phone { get; set; }
    }
    public class User
    {
        public int id { get; set; }
        public int referred_by_sales_userId { get; set; }
        public int sales_centerId { get; set; }
        public string name { get; set; }
        public string note { get; set; }
        public int phone_countryId { get; set; }
        public int agent_sales_userId { get; set; }
        public int message_total { get; set; }
        public int? message_last_month { get; set; }
        public string phone { get; set; }
        public int user_statusId { get; set; }
        public string added_on { get; set; }

        public int sales_planId { get; set; } 
        public string plan_start { get; set; }
        public string otp { get; set; }
       public DateTime otp_time { get; set; }
        public string last_update_customer { get; set; }
        public string last_update_message { get; set; }
        public string last_update_setting { get; set; }
        public string last_update_template { get; set; }
        public string last_update_template_date { get; set; }

        public int otp_count { get; set; }
        public string token { get; set; }
        public string payment_mode { get; set; }
        public string pt { get; set; }
        public string hash_password { get; set; }
        public byte[] salt { get; set; }
        public bool is_enabled { get; set; }
        public Hiring hiring_source { get; set; }
        public string win_con_id { get; set; }
        public string mob_con_id { get; set; }
        public string web_con_id { get; set; }
        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
