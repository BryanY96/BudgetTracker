using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Income
    {
        public int Id { get; set; }
        
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime? IncomeDate { get; set; }
        public string Remarks { get; set; }

        // Foriegn Key
        public int UserId { get; set; }

        // navigation property
        public User User { get; set; }
    }
}
