using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class CreateEmployee
    {
        public string status { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public string name { get; set; }

        public string salary { get; set; }

        public string age { get; set; }

        public int id { get; set; }

    }
}
