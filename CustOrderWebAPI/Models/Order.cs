
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace CustOrderWebAPI.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

      
        public int CustomerID { get; set; }

        [Required][StringLength(80)]
        public string Description { get; set; }
        [Required]
        public decimal Total { get; set; }
        [Required]
        public bool Fulfilled { get; set; }

        public virtual Customer Customer { get; set; }

        public Order Copy(Order newdata)
        {
            CustomerID = newdata.CustomerID;
            Description = newdata.Description;
            Total = newdata.Total;
            Fulfilled = newdata.Fulfilled;
            return this;
        }
    }
}