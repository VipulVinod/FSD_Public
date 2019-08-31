﻿using POPSWcfService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace POPSWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPopsService" in both code and config file together.
    [ServiceContract]
    public interface IPopsSupplierService
    {
        [OperationContract]
        Supplier GetSupplierById(string SNo);
        [OperationContract]
        void AddSuppier(Supplier supplier);
        [OperationContract]
        List<Supplier> GetAllSuppliers();
        [OperationContract]
        void SaveSupplierDetails(List<Supplier> SupplierDetails);
    }
}
