using POPSWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POPSWebApi.Controllers
{
    public class ItemController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<ItemVM> Get()
        {
            List<ItemVM> lstItems = new List<ItemVM>();
            using (PODbEntities podb = new PODbEntities())
            {
                var Items = from p in podb.ITEMs
                            select p;
                foreach (ITEM singleItem in Items)
                {
                    ItemVM itm = new ItemVM();
                    itm.ITCode = singleItem.ITCODE;
                    itm.ITDesc = singleItem.ITDESC;
                    itm.ITRate = singleItem.ITRATE;
                    lstItems.Add(itm);
                }
            }
            return lstItems;
        }

        // GET api/Item/5
        public IHttpActionResult Get(string id)
        {
            ItemVM itm = new ItemVM();
            using (PODbEntities podb = new PODbEntities())
            {
                var selectedItem = (from p in podb.ITEMs
                                    where p.ITCODE == id
                                    select p).First();
                itm.ITCode = selectedItem.ITCODE;
                itm.ITDesc = selectedItem.ITDESC;
                itm.ITRate = selectedItem.ITRATE;
            }
            return Ok(itm);
        }

        // POST api/<controller>
        public void Post(ItemVM item)
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

        // PUT api/<controller>/5
        public void Put(string id, ItemVM item)
        {
            using (PODbEntities podb = new PODbEntities())
            {
                var selectedItem = (from p in podb.ITEMs
                                    where p.ITCODE == item.ITCode
                                    select p).First();

                selectedItem.ITDESC = item.ITDesc;
                selectedItem.ITRATE = item.ITRate;
                podb.SaveChanges();
            }
        }

        // DELETE api/<controller>/5
        public void Delete(string id)
        {
            using (PODbEntities podb = new PODbEntities())
            {
                var selectedItem = (from p in podb.ITEMs
                                    where p.ITCODE == id
                                    select p).First();

                podb.Entry(selectedItem).State = System.Data.Entity.EntityState.Deleted;
                podb.SaveChanges();
            }
        }
    }
}