using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftMarketing.Model
{
    public class Subscription
    {
        public int subscriptionid { get; set; }

        public int? subscriptiontypeid { get; set; }

        public int? customerid { get; set; }

        public DateTime? startdate { get; set; }

        public DateTime? enddate { get; set; }

    }
}
