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
    public class PopsService : IPopsService
    {
        #region 'Item'
        public Item GetItemById(string itemId)
        {
            using (PODbEntities podb = new PODbEntities())
            {
                Item itm = new Item();
                var selectedItem = (from p in podb.ITEMs
                                    where p.ITCODE == itemId
                                    select p).First();

                itm.ITCode = selectedItem.ITCODE;
                itm.ITDesc = selectedItem.ITDESC;
                itm.ITRate = selectedItem.ITRATE;
                return itm;
            }
        }

        public void AddItem(Item item)
        {
            using (PODbEntities podb = new PODbEntities())
            {
                ITEM newITEM = new ITEM();
                newITEM.ITCODE = item.ITCode;
                newITEM.ITDESC = item.ITDesc;
                newITEM.ITRATE = item.ITRate;
                podb.ITEMs.Add(newITEM);
                podb.SaveChanges();
            }
        }

        public List<Item> GetAllItems()
        {
            List<Item> lstItems = new List<Item>();
            using (PODbEntities podb = new PODbEntities())
            {
                var Items = from p in podb.ITEMs
                            select p;
                foreach (ITEM singleItem in Items)
                {
                    Item itm = new Item();
                    itm.ITCode = singleItem.ITCODE;
                    itm.ITDesc = singleItem.ITDESC;
                    itm.ITRate = singleItem.ITRATE;
                    lstItems.Add(itm);
                }
            }
            return lstItems;
        }

        public void SaveItemDetails(Item Item)
        {
            using (PODbEntities podb = new PODbEntities())
            {
                Item itm = new Item();
                var selectedItem = (from p in podb.ITEMs
                                    where p.ITCODE == Item.ITCode
                                    select p).First();

                selectedItem.ITDESC = Item.ITDesc;
                selectedItem.ITRATE = Item.ITRate;
                podb.SaveChanges(); 
            }
        }
        #endregion 'Item'

        #region 'Supplier'
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

        #endregion 'Supplier'

        #region 'Order'
        public List<PurchaseOrderMaster> GetAllOrders()
        {
            using (PODbEntities podb = new PODbEntities())
            {
                List<PurchaseOrderMaster> orders = new List<PurchaseOrderMaster>();
                foreach (var order in podb.POMASTERs)
                {
                    PurchaseOrderMaster master = new PurchaseOrderMaster();
                    master.PONO = order.PONO;
                    master.PODate = order.PODATE;
                    master.SUPLNO = order.SUPLNO;
                    orders.Add(master);
                }
                return orders;
            }
        }

        public List<PurchaseOrderDetails> GetOrderDetails (string orderId)
        {
            using (PODbEntities podb = new PODbEntities())
            {
                var selectedItem = (from p in podb.POMASTERs
                                    where p.PONO == orderId
                                    select p).First();

                List<PurchaseOrderDetails> PODetails = new List<PurchaseOrderDetails>();
                foreach (var det in selectedItem.PODETAILs)
                {
                    PurchaseOrderDetails detail = new PurchaseOrderDetails();
                    detail.PONO = det.PONO;
                    detail.ITCode = det.ITCODE;
                    detail.Qty = det.QTY;
                    PODetails.Add(detail);
                }
                return PODetails;
            }
        }

        public void AddOrder(PurchaseOrderMaster purchaseOrder)
        {
            try
            {
                using (PODbEntities podb = new PODbEntities())
                {
                    POMASTER master = new POMASTER();
                    master.PONO = purchaseOrder.PONO;
                    master.PODATE = purchaseOrder.PODate;
                    master.SUPLNO = purchaseOrder.SUPLNO;


                    foreach (var podetail in purchaseOrder.PODetails)
                    {
                        if (podetail != null)
                        {
                            PODETAIL detail = new PODETAIL();
                            detail.PONO = purchaseOrder.PONO;
                            detail.ITCODE = podetail.ITCode;
                            detail.QTY = podetail.Qty;
                            master.PODETAILs.Add(detail);
                        }
                    }
                    podb.POMASTERs.Add(master);

                    podb.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }

        }
        #endregion 'Order'
    }
}
