using POPSMvcWcfApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;


namespace POPSMvcWcfApplication.Controllers
{
    public class WebAPISupplierController : Controller
    {
        // GET: Supplier
        public ActionResult Index()
        {
            IEnumerable<SupplierVM> suppliers = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59007/api/supplier");
                //HTTP GET
                var responseTask = client.GetAsync("supplier");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<SupplierVM>>();
                    readTask.Wait();

                    suppliers = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    suppliers = Enumerable.Empty<SupplierVM>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(suppliers);
        }

        // GET: Supplier/Details/5
        public ActionResult Details(string id)
        {
            SupplierVM supplier = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59007/api/");
                //HTTP GET
                var responseTask = client.GetAsync("supplier?id=" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<SupplierVM>();
                    readTask.Wait();

                    supplier = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    supplier = new SupplierVM();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(supplier);
        }

        // GET: Supplier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Supplier/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            SupplierVM supplier = new SupplierVM();
            supplier.SUPLNO = collection[1];
            supplier.SUPLNAME = collection[2];
            supplier.SUPLADDR = collection[3];
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59007/api/Supplier");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<SupplierVM>("Supplier", supplier);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(supplier);
        }

        // GET: Supplier/Edit/5
        public ActionResult Edit(string id)
        {
            SupplierVM supplier = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59007/api/");
                //HTTP GET
                var responseTask = client.GetAsync("supplier?id=" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<SupplierVM>();
                    readTask.Wait();

                    supplier = readTask.Result;
                }
            }

            return View(supplier);
        }

        // POST: Supplier/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                SupplierVM supplier = new SupplierVM();
                supplier.SUPLNO = collection[1];
                supplier.SUPLNAME = collection[2];
                supplier.SUPLADDR = collection[3];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:59007/api/supplier/" + id);

                    //HTTP POST
                    var putTask = client.PutAsJsonAsync<SupplierVM>("supplier", supplier);
                    putTask.Wait();

                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
                return View(supplier);
            }
            catch
            {
                return View();
            }
        }

        // GET: Supplier/Delete/5
        public ActionResult Delete(string id)
        {
            SupplierVM supplier = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59007/api/");
                //HTTP GET
                var responseTask = client.GetAsync("supplier?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<SupplierVM>();
                    readTask.Wait();

                    supplier = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    supplier = new SupplierVM();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(supplier);
        }

        // POST: Supplier/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:59007/api/");
                    //HTTP Delete
                    var deleteTask = client.DeleteAsync("supplier?id=" + id.ToString());
                    deleteTask.Wait();

                    var result = deleteTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Index");
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
