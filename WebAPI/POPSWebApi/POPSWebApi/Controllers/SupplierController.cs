using POPSWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POPSWebApi.Controllers
{
    public class SupplierController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<SupplierVM> Get()
        {
            List<SupplierVM> lstSuppliers = new List<SupplierVM>();
            using (PODbEntities podb = new PODbEntities())
            {
                var Suppliers = from p in podb.SUPPLIERs
                            select p;
                foreach (SUPPLIER singleSupplier in Suppliers)
                {
                    SupplierVM sup = new SupplierVM();
                    sup.SUPLNO = singleSupplier.SUPLNO;
                    sup.SUPLNAME = singleSupplier.SUPLNAME;
                    sup.SUPLADDR = singleSupplier.SUPLADDR;
                    lstSuppliers.Add(sup);
                }
            }
            return lstSuppliers;
        }

        // GET api/Supplier/5
        public IHttpActionResult Get(string id)
        {
            SupplierVM sup = new SupplierVM();
            using (PODbEntities podb = new PODbEntities())
            {
                var selectedSupplier = (from p in podb.SUPPLIERs
                                    where p.SUPLNO == id
                                    select p).First();
                sup.SUPLNO = selectedSupplier.SUPLNO;
                sup.SUPLNAME = selectedSupplier.SUPLNAME;
                sup.SUPLADDR = selectedSupplier.SUPLADDR;
            }
            return Ok(sup);
        }

        // POST api/<controller>
        public void Post(SupplierVM supplier)
        {
            using (PODbEntities podb = new PODbEntities())
            {
                SUPPLIER NewSUPPLIER = new SUPPLIER();
                NewSUPPLIER.SUPLNO = supplier.SUPLNO;
                NewSUPPLIER.SUPLNAME = supplier.SUPLNAME;
                NewSUPPLIER.SUPLADDR = supplier.SUPLADDR;
                podb.SUPPLIERs.Add(NewSUPPLIER);
                podb.SaveChanges();
            }

        }

        // PUT api/<controller>/5
        public void Put(string id, SupplierVM supplier)
        {
            using (PODbEntities podb = new PODbEntities())
            {
                var selectedSupplier = (from p in podb.SUPPLIERs
                                    where p.SUPLNO == supplier.SUPLNO
                                        select p).First();

                selectedSupplier.SUPLNAME = supplier.SUPLNAME;
                selectedSupplier.SUPLADDR = supplier.SUPLADDR;
                podb.SaveChanges();
            }
        }

        // DELETE api/<controller>/5
        public void Delete(string id)
        {
            using (PODbEntities podb = new PODbEntities())
            {
                var selectedSupplier = (from p in podb.SUPPLIERs
                                    where p.SUPLNO == id
                                    select p).First();

                podb.Entry(selectedSupplier).State = System.Data.Entity.EntityState.Deleted;
                podb.SaveChanges();
            }
        }
    }
}