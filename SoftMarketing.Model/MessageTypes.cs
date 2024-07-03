using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftMarketing.Model
{
    public class MessageTypes
    {
        public int messagetypeid { get; set; }

        public string messagename { get; set; }

        public string messagetext { get; set; }

    }
}
