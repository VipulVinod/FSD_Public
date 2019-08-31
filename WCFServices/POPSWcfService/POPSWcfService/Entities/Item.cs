using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace POPSWcfService.Entities
{
    [DataContract]
    public class Item
    {
        [DataMember]
        public string ITCode { get; set; }
        [DataMember]
        public string ITDesc { get; set; }
        [DataMember]
        public decimal ? ITRate { get; set;
        }
    }
}