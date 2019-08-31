using POPSMvcWcfApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POPSMvcWcfApplication.Controllers
{
    public class PurchaseOrderController : Controller
    {
        // GET: PurchaseOrder
        public ActionResult Index()
        {
            using (ServiceReference1.PopsServiceClient obj = new ServiceReference1.PopsServiceClient())
            {
               return View(obj.GetAllOrders());
            }
        }

        // GET: PurchaseOrder/Details/5
        public ActionResult Details(string id)
        {
            using (ServiceReference1.PopsServiceClient obj = new ServiceReference1.PopsServiceClient())
            {
                return View(obj.GetOrderDetails(id));
           }
        }

        // GET: PurchaseOrder/Order
        public ActionResult Order()
        {
          
            return View();
        }

        [HttpPost]
        public JsonResult SaveOrder(OrderVM O)
        {


            bool status = false;
            if (ModelState.IsValid)
            {
                using (ServiceReference1.PopsServiceClient obj = new ServiceReference1.PopsServiceClient())
                {
                    ServiceReference1.PurchaseOrderMaster order = new ServiceReference1.PurchaseOrderMaster();
                    order.PONO = O.OrderNo;
                    order.PODate = O.OrderDate;
                    order.SUPLNO = O.SupplierNumber;
                    ServiceReference1.PurchaseOrderDetails[] details = new ServiceReference1.PurchaseOrderDetails[10];
                    int index = 0;
                    foreach (var i in O.OrderDetails)
                    {
                        ServiceReference1.PurchaseOrderDetails detail = new ServiceReference1.PurchaseOrderDetails();
                        detail.PONO = O.OrderNo;
                        detail.ITCode = i.ItemCode;
                        detail.Qty = i.Quantity;
                        details[index] = detail;
                        index++;
                    }
                    order.PODetails = details;
                    obj.AddOrder(order);
                    status = true;
                }
            }
            else
            {
                status = false;
            }
            return new JsonResult { Data = new { status = status } };
        }


        // GET: PurchaseOrder/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PurchaseOrder/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
              return View();
        }

       

        // GET: PurchaseOrder/Delete/5
        public ActionResult Delete(string id)
        {
            return View();
        }

        // POST: PurchaseOrder/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


       
    }
}
