using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftMarketing.Model.SalesModels
{
    public class CenterUser
    {
        public int Id { get; set; }
        public int CenterId { get; set; }
        public int CountryId { get; set; }
        public int DesignationId { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }

    }
}
