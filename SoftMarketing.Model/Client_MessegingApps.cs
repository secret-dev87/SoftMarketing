using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftMarketing.Model
{
    public class Client_MessegingApps
    {
        public int Id { get; set; }

        public int? ClientId { get; set; }

        public int? MessegingAppId { get; set; }

    }
}
