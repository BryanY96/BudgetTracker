using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MengYu.BudgetTracker.ApplicationCore.Models
{
    public class UserDetailsResponseModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public DateTime? JoinedOn { get; set; }

        public List<IncomeResponseModel> Incomes { get; set; }
        public List<ExpenditureResponseModel> Expenditures { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenditure { get; set; }
        public decimal Balance { get; set; }
    }
}
