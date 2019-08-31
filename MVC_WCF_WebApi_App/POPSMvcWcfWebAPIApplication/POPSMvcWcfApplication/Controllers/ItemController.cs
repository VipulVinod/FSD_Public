﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POPSMvcWcfApplication.Controllers
{
    public class ItemController : Controller
    {
        // GET: Item
        public ActionResult Index()
        {
            ServiceReference1.PopsServiceClient obj = new ServiceReference1.PopsServiceClient();
            return View(obj.GetAllItems());
        }

        // GET: Item/Details/5
        public ActionResult Details(string id)
        {
            ServiceReference1.PopsServiceClient obj = new ServiceReference1.PopsServiceClient();
            return View(obj.GetItemById(id));
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
            try
            {
                // TODO: Add insert logic here
                ServiceReference1.PopsServiceClient obj = new ServiceReference1.PopsServiceClient();
                ServiceReference1.Item itm = new ServiceReference1.Item();
                itm.ITCode = collection[1];
                itm.ITDesc = collection[2];
                int itemRate;
                Int32.TryParse(collection[3], out itemRate);
                itm.ITRate = itemRate;
                obj.AddItem(itm);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Item/Edit/5
        public ActionResult Edit(string id)
        {
            return View();
        }

        // POST: Item/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Item/Delete/5
        public ActionResult Delete(string id)
        {
            return View();
        }

        // POST: Item/Delete/5
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
