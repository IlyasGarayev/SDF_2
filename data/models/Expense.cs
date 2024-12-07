using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDF_2.data.models
{

    public class Expense
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ExpenseType { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
