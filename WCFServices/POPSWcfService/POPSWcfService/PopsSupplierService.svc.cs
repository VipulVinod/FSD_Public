using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using POPSWcfService.Entities;

namespace POPSWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PopsService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select PopsService.svc or PopsService.svc.cs at the Solution Explorer and start debugging.
    public class PopsSupplierService : IPopsSupplierService
    {
        public Supplier GetSupplierById(string SNo)
        {
            using (PODbEntities podb = new PODbEntities())
            {
                Supplier supplier = new Supplier();
                var selectedItem = (from s in podb.SUPPLIERs
                                    where s.SUPLNO == SNo
                              select s).First();

                supplier.SUPLNO = selectedItem.SUPLNO;
                supplier.SUPLNAME = selectedItem.SUPLNAME;
                supplier.SUPLADDR = selectedItem.SUPLADDR;
                return supplier;
            }
        }

        public void AddSuppier(Supplier supplier)
        {
            using (PODbEntities podb = new PODbEntities())
            {
                SUPPLIER NewSupplier = new SUPPLIER();
                NewSupplier.SUPLNO = supplier.SUPLNO;
                NewSupplier.SUPLNAME = supplier.SUPLNAME;
                NewSupplier.SUPLADDR = supplier.SUPLADDR;
                podb.SUPPLIERs.Add(NewSupplier);
                podb.SaveChanges();
            }
        }

        public List<Supplier> GetAllSuppliers()
        {
            List<Supplier> lstSuppliers = new List<Supplier>();
            using (PODbEntities podb = new PODbEntities())
            {
                var suppliers = from s in podb.SUPPLIERs
                            select s;
                foreach (SUPPLIER singleSupplier in suppliers)
                {
                    Supplier supplier = new Supplier();
                    supplier.SUPLNO = singleSupplier.SUPLNO;
                    supplier.SUPLNAME = singleSupplier.SUPLNAME;
                    supplier.SUPLADDR = singleSupplier.SUPLADDR;
                    lstSuppliers.Add(supplier);
                }
            }
            return lstSuppliers;
        }

        public void SaveSupplierDetails(List<Supplier> supplierDetails)
        {
            using (PODbEntities podb = new PODbEntities())
            {
                foreach (var detail in supplierDetails)
                {
                    var Supplier = (from s in podb.SUPPLIERs
                                where s.SUPLNO == detail.SUPLNO
                                select s).First();

                    Supplier.SUPLNAME = detail.SUPLNAME;
                    Supplier.SUPLADDR = detail.SUPLADDR;
                    podb.SaveChanges();
                }
            }
        }
    }
}
