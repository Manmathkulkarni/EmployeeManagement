using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class ShowEmployee

    {
        public string status { get; set; }

        public IList<Data> data { get; set; }

    }
    public class Data

    {
        public string id { get; set; }

        public string employee_name { get; set; }

        public string employee_salary { get; set; }

        public string employee_age { get; set; }

        public string profile_image { get; set; }

    }


}
