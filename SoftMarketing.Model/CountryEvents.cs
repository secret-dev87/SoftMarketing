using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftMarketing.Model
{
    public class CountryEvents
    {
        public int countryeventid { get; set; }

        public int? countryid { get; set; }

        public int? eventid { get; set; }

        public DateTime? customdate { get; set; }

    }
}
