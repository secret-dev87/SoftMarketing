using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftMarketing.Model.MarketingModels
{
    public class UserSettings
    {
        public bool send_feedback { get; set; }
        public bool send_advertisement { get; set; }
        public bool send_events { get; set; }
        public bool send_birthday { get; set; }
        public bool send_reminders { get; set; }
        public string? reminder_duration { get; set; }
        public string? reminder_times { get; set; }
        public bool delete_customer { get; set; }
    }
}
