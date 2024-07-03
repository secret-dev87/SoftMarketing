using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftMarketing.Model
{
    public class Message
    {
        public int usermessage_id { get; set; }
        public string salesuserid { get; set; }
        public string customerid { get; set; }
        public string marketingusertemplateid { get; set; }
        public string scheduledtime { get; set; }
    }
    public class SchedulMessage
    {
        public long id { get; set; }
        [Required]
        public int? usertemplate_id { get; set; }
        public int sales_userId { get; set; }
        public string? name { get; set; }
        public string? templatetype { get; set; }
        [Required]
        public string? template { get; set; }
        public string? customer_country_name { get; set; }
        public string? country_name { get; set; }
        [Required]
        public string? customer_name { get; set; }
        [Required]
        public string? phone { get; set; }
        public string? phone_alternate { get; set; }
        public int? templatetypeid { get; set; }
        public int marketing_user_customerId { get; set; }
        public bool? sent { get; set; }
        public int customer_countryid { get; set; }
        [Required]
        public DateTime scheduled_time { get; set; }
    }
    public class TodayScheduledMessages
    {
        //usertemplate_id customer_name		phone template add_customer	add_owner	add_business	send_text_with_image
        public long id { get; set; }
        [Required]
        public int? usertemplate_id { get; set; }
        //public int sales_userId { get; set; }
        //public string? name { get; set; }
        //public string? templatetype { get; set; }
        //[Required]
        //public string? template { get; set; }
        //public string? customer_country_name { get; set; }
        //public string? country_name { get; set; }
        [Required]
        public string? customer_name { get; set; }
        [Required]
        public string? phone { get; set; }
        public string? phone_alternate { get; set; }
        public int? templatetypeid { get; set; }
        public int marketing_user_customerId { get; set; }
        public bool? sent { get; set; }
        public bool? add_customer { get; set; }
        public bool? add_owner { get; set; }
        public bool? add_business { get; set; }
        public bool? send_text_with_image { get; set; }
        //public int customer_countryid { get; set; }
        [Required]
        public DateTime scheduled_time { get; set; }
    }

}
