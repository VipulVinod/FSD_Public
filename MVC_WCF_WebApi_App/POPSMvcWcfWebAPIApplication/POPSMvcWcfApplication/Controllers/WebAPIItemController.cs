using POPSMvcWcfApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;


namespace POPSMvcWcfApplication.Controllers
{
    public class WebAPIItemController : Controller
    {
        // GET: Item
        public ActionResult Index()
        {
            IEnumerable<ItemVM> items = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59007/api/item");
                //HTTP GET
                var responseTask = client.GetAsync("item");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ItemVM>>();
                    readTask.Wait();

                    items = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    items = Enumerable.Empty<ItemVM>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(items);
        }

        // GET: Item/Details/5
        public ActionResult Details(string id)
        {
            ItemVM item = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59007/api/");
                //HTTP GET
                var responseTask = client.GetAsync("item?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ItemVM>();
                    readTask.Wait();

                    item = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    item = new ItemVM();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(item);
        }

        // GET: Item/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Item/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            ItemVM item = new ItemVM();
            item.ITCode = collection[1];
            item.ITDesc = collection[2];
            int itemRate;
            Int32.TryParse(collection[3], out itemRate);
            item.ITRate = itemRate;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59007/api/Item");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<ItemVM>("Item", item);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(item);
        }

        // GET: Item/Edit/5
        public ActionResult Edit(string id)
        {
            ItemVM item = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59007/api/");
                //HTTP GET
                var responseTask = client.GetAsync("item?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ItemVM>();
                    readTask.Wait();

                    item = readTask.Result;
                }
            }

            return View(item);
        }

        // POST: Item/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                ItemVM item = new ItemVM();
                item.ITCode = collection[1];
                item.ITDesc = collection[2];
                decimal itemRate;
                decimal.TryParse(collection[3], out itemRate);
                item.ITRate = itemRate;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:59007/api/item/" + id);

                    //HTTP POST
                    var putTask = client.PutAsJsonAsync<ItemVM>("item", item);
                    putTask.Wait();

                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Index");
                    }
                }
                return View(item);
            }
            catch
            {
                return View();
            }
        }

        // GET: Item/Delete/5
        public ActionResult Delete(string id)
        {
            ItemVM item = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:59007/api/");
                //HTTP GET
                var responseTask = client.GetAsync("item?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ItemVM>();
                    readTask.Wait();

                    item = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    item = new ItemVM();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(item);
        }

        // POST: Item/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:59007/api/");
                    //HTTP Delete
                    var deleteTask = client.DeleteAsync("item?id=" + id.ToString());
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
