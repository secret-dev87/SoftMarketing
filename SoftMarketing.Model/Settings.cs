using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftMarketing.Model
{
    public class Settings
    {
        public string user_country { get; set; }

        public string user_phone { get; set; }

        public string settings { get; set; }

        public string value { get; set; }

        public int id { get; set; }

    }
}
