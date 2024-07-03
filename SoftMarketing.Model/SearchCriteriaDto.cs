using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftMarketing.Model
{
    [System.Serializable]
    public class SearchCriteriaDto
    {
        public int CurrentPage { set; get; }
        public int PageSize { set; get; }
        public string PhoneNumber { set; get; }
        public string CountryCode { set; get; }
    }
}
