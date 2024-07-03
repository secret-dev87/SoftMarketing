using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftMarketing.Model
{
    public class Client
    {
        public int ClientId { get; set; }

        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string MiddleInitial { get; set; }

        public string LastName { get; set; }

        public string ContactNumber { get; set; }

        public string AlternateNumber { get; set; }

        public string Email { get; set; }

        public DateTime LastVisit { get; set; }

        public DateTime DateAdded { get; set; }

        public int? AddedBy { get; set; }

        public DateTime DateUpdated { get; set; }

        public int UpdatedBy { get; set; }

    }
}
