using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftMarketing.Model
{
    public class User_Message
    {
        public int UMId { get; set; }

        public int UserID { get; set; }

        public int MessageId { get; set; }

        public string CustomMessage { get; set; }

        public string ImageURL { get; set; }

    }
}
