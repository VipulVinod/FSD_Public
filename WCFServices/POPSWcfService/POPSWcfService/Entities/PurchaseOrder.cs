using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace POPSWcfService.Entities
{
    [DataContract]
    public class PurchaseOrderMaster
    {
        [DataMember]
        public string PONO { get; set; }
        [DataMember]
        public DateTime? PODate { get; set; }
        [DataMember]
        public string SUPLNO { get; set; }
        [DataMember]
        public List<PurchaseOrderDetails> PODetails { get; set; }
    }

    [DataContract]
    public class PurchaseOrderDetails
    {
        [DataMember]
        public string PONO { get; set; }
        [DataMember]
        public string ITCode { get; set; }
        [DataMember]
        public int? Qty { get; set; }

    }


}