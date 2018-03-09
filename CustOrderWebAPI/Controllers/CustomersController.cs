using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CustOrderWebAPI.Models;

namespace CustOrderWebAPI.Controllers
{
    public class CustomersController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Customers/List
        public ActionResult List()
        {
            return Js(db.Customers.ToList());
        }
    
        // GET: Customers/Get/5
        public ActionResult Get(int? id)
        {
            if(id == null)
            {
                return Failure("Id is null");
            }
            Customer customer = db.Customers.Find(id);

            if (!Save())
            {
                return BadSaveResult();
            }
            return Js(customer);
        }

        // POST: Customers/Create
        public ActionResult Create(Customer customer)
        {
            customer = db.Customers.Add(customer);
            if(customer == null)
            {
                return Failure("Unable to create customer");
            }
            if (!Save())
            {
                return BadSaveResult();
            }
            return Success("Created new Customer.  ID: " + customer.Id);
        }

        // POST: Customers/Update
        public ActionResult Update(Customer customer)
        {
            Customer oldCustomer = db.Customers.Find(customer.Id);
            if(customer == null)
            {
                return Failure("Unable to find the customer ID");
            }

            oldCustomer.Copy(customer);

            if (!Save())
            {
                return BadSaveResult();
            }
            return Success("Updated the customer succesfully");
        }

        // POST: Customers/Delete
        public ActionResult Remove(Customer customer)
        {
            Customer ExistingCustomer = db.Customers.Find(customer.Id);
            if(customer == null)
            {
                return Failure("Id does not exist.");
            }
            db.Customers.Remove(ExistingCustomer);
            if (!Save())
            {
                return BadSaveResult();
            }
            return Success("Deleted the customer succesfully.");
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