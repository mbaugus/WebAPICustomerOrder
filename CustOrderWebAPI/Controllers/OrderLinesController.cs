using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CustOrderWebAPI.Models;

namespace CustOrderWebAPI.Controllers
{
    public class OrderLinesController : Controller
    {
        AppDbContext db = new AppDbContext();

        // GET: OrderLines/List
        public ActionResult List()
        {
            return Js(db.OrderLines.ToList());
        }

        // GET: OrderLines/Get/5
        public ActionResult Get(int? id)
        {
            if (id == null)
            {
                return Failure("Provided ID is null");
            }

            OrderLine orderline = db.OrderLines.Find(id);

            if (orderline == null)
            {
                return Failure("Could not find orderline.");
            }
            return Js(orderline);
        }

        // POST: Orders/Create/
        public ActionResult Create(OrderLine orderline)
        {
            //orderline.CalculateTotal();
            orderline.LineTotal = orderline.Quantity * orderline.Price;

            OrderLine neworderline = db.OrderLines.Add(orderline);
            if (neworderline == null)
            {
                return Failure("Unable to create.");
            }
            // update the main order total
           
            Order order = db.Orders.Find(neworderline.OrderId);
            order.Total += neworderline.LineTotal;

            if (!Save())
            {
                return BadSaveResult();
            }

            return Success("Created new orderline succesfully. ID: " + neworderline.Id);
        }

        // POST: Orders/Update/
        public ActionResult Update(OrderLine orderline)
        {
            OrderLine oldOrderLine = db.OrderLines.Find(orderline.Id);
            decimal OldTotal = oldOrderLine.LineTotal;

            if (oldOrderLine == null)
            {
                return Failure("Unable to find the OrderLine ID.");
            }

            oldOrderLine.Copy(orderline);
            oldOrderLine.CalculateTotal();

            Order order = db.Orders.Find(oldOrderLine.OrderId);
            order.Total = order.Total - OldTotal + oldOrderLine.LineTotal;

            if (!Save())
            {
                return BadSaveResult();
            }

            return Success("Updated orderline succesfully.");
        }

        // POST: Orders/Delete/
        public ActionResult Remove(OrderLine orderline)
        {
            OrderLine oldOrderLine = db.OrderLines.Find(orderline.Id);
            if (oldOrderLine == null)
            {
                return Failure("Unable to find the Order ID.");
            }

            decimal OrderLineTotal = oldOrderLine.LineTotal;
            Order order = db.Orders.Find(oldOrderLine.OrderId);
            order.Total = order.Total - OrderLineTotal;
            db.OrderLines.Remove(oldOrderLine);

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
            return Failure(SaveException.Message + "Inner Exception: " + SaveException.InnerException);
        }

        public bool Save()
        {
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
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