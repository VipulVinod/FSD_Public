using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POPSMvcWcfApplication.Models
{
    public class OrderVM
    {
        public string OrderNo { get; set; }
        public DateTime? OrderDate { get; set; }
        public string SupplierNumber { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }

    public class OrderDetail
    {
        public string OrderNo { get; set; }
        public string ItemCode { get; set; }
        public int Quantity { get; set; }
    }
}