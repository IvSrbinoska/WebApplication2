using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Case
    {
        public int ID { get; set; }

        public String caseNumber { get; set; }

        public String kind { get; set; }

        public String customerNumber { get; set; }
    }
}