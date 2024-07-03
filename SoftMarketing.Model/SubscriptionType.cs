using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftMarketing.Model
{
    public class SubscriptionType
    {
        public int subscriptiontypeid { get; set; }

        public string name { get; set; }

        public int? duration { get; set; }

    }
}
