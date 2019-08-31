using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace POPSWcfService.Entities
{
    [DataContract]
    public class Supplier
    {
        [DataMember]
        public string SUPLNO { get; set; }
        [DataMember]
        public string SUPLNAME { get; set; }
        [DataMember]
        public string SUPLADDR { get; set;}
    }
}