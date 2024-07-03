using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftMarketing.Model
{
    public class ClientMessageHistory
    {
        public int messagehistoryid { get; set; }

        public int? messageid { get; set; }

        public int clientid { get; set; }

        public DateTime? sentdateselected { get; set; }

        public int? attemptsnos { get; set; }

        public bool? issuccess { get; set; }

        public int? smspackagecount { get; set; }

        public int? remainingsms { get; set; }

        public DateTime? lastsentdate { get; set; }

    }
}
