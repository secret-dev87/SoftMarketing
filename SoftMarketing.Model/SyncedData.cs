using SoftMarketing.Model.MarketingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftMarketing.Model
{
    public class SyncedData
    {
        public string? last_update_customer { get; set; }
        public string? last_update_message { get; set; }
        public string? last_update_setting { get; set; }
        public string? last_update_template { get; set; }
        public string? last_update_template_date { get; set; }

        public List<Customer> Customers { get; set; }
        public List<SchedulMessage> Messages { get; set; }
        public List<User_Template> Templates { get; set; }
        public List<TemplateDate> TemplateDates { get; set; }
        public UserSettings Settings { get; set; }
    }
}
