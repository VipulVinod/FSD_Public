using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POPSMvcWcfApplication.Models
{
    public class ItemVM
    {
        public string ITCode { get; set; }
        public string ITDesc { get; set; }
        public decimal? ITRate { get; set; }
    }
}