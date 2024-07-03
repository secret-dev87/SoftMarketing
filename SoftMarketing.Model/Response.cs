using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftMarketing.Model
{
    public class Response<T> where T : class
    {
        public int NumberOfRecords { get; set; }
        public int TotalCount { get; set; }
        public List<T> Collection { get; set; }
        public List<T> Items { get; set; }
        public T Item { get; set; }
        public bool IsSuccess { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public string SyncedData { get; set; }
    }
}
