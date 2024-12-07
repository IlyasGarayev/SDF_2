using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDF_2.data.models
{
    public class ExpenseType
    {
        public int Id { get; set; } // Xərc növünün unikal identifikatoru
        public string Name { get; set; } // Xərc növünün adı

        public ExpenseType() { }

        public ExpenseType(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

}
