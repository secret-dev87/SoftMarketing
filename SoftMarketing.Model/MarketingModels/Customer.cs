using Contrib = Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations;

namespace SoftMarketing.Model.MarketingModels
{
    [Contrib.Table("marketing_user_customer")]
    public class Customer
    {
        public Customer()
        {
            phone_countryId = 50;
        }
        [Contrib.Key]
        public int id { get; set; }
        [Required]
        public int sales_userId { get; set; }
        [Required]
        public int phone_countryId { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string phone { get; set; }
        public string? phone_alternate { get; set; }
        public string? email { get; set; }
        public DateTime? last_visit { get; set; }
        public DateTime? added { get; set; }
        public string? birthday { get; set; }
        public string? year { get; set; }
        public string? address { get; set; }
        public string? details { get; set; }
        public string? app_2_id { get; set; }
       
        public int? common_app_1Id { get; set; }
        public int? common_app_2Id { get; set; }
        public bool send_advertise_msg
        {
            get;
            set;
        }
        public bool send_feedback_msg {
            get;
            set;
        }
        public bool send_events_msg
        {
            get;
            set;
        }


        public bool send_reminder_msg
        {
            get;
            set;
        }
        public string? reminder_duration { get; set; }
        public string reminder_count { get; set; }
        public DateTime? last_reminder { get; set; }
    }
}
