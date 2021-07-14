using System;
using System.Collections.Generic;
using System.Text;

namespace DurableFunctionsApp
{
    public class Student
    {
        public int id { get; set; }
        public string name { get; set; }
        public double percentage { get; set; }

        public string grade
        {
            get
            {
                if (percentage >= 80)
                    return "A";

                else if (percentage > 35)
                    return "B";
                else
                    return "C";
            }
        }
    }

}
