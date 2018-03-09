using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CustOrderWebAPI.Models;

namespace CustOrderWebAPI.Controllers
{
    public class OrdersController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Orders/List
        public ActionResult List()
        {
            return Js(db.Orders.ToList());
        }

        // GET: Orders/Get/5
        public ActionResult Get(int? id)
        {
            if(id == null)
            {
                return Failure("Provided ID is null");
            }

            Order order = db.Orders.Find(id);

            if(order == null)
            {
                return Failure("Could not find order.");
            }
            return Js(order);
        }

        // POST: Orders/Create/
        public ActionResult Create(Order order)
        {
            Order neworder = db.Orders.Add(order);
            if(neworder == null){
                return Failure("Unable to create.");
            }
            if (!Save())
            {
                return BadSaveResult();
            }
            return Success("Created new order succesfully. ID: " + neworder.Id);
        }

        // POST: Orders/Update/
        public ActionResult Update(Order order)
        {
            Order oldOrder = db.Orders.Find(order.Id);

            if (order == null)
            {
                return Failure("Unable to find the Order ID.");
            }

            oldOrder.Copy(order);

            if (!Save())
            {
                return BadSaveResult();
            }

            return Success("Updated succesfully");
        }
        // POST: Orders/Delete/
        public ActionResult Remove(Order order)
        {
            order = db.Orders.Find(order.Id);
            if(order == null)
            {
                return Failure("Unable to find the Order ID.");
            }
            db.Orders.Remove(order);
            if (!Save())
            {
                return BadSaveResult();
            }
            return Success("Removed order");
        }


        /// <summary>
        ///  addon helper functions
        /// </summary>
        private Exception SaveException = null;
        private ActionResult BadSaveResult()
        {
            return Js(new { Status = "Failure", Message = SaveException.Message });
        }

        public bool Save()
        {
            try
            {
                db.SaveChanges();
            }
            catch(Exception e)
            {
                SaveException = e;
                return false;
            }
            return true;
        }

        private ActionResult Success(string Message)
        {
            return Js(new { Status = "Success", Message });
        }
        private ActionResult Failure(string Message)
        {
            return Js(new { Status = "Failure", Message });
        }
        private ActionResult Js(object data)
        {
            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}