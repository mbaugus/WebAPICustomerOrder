using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustOrderWebAPI.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [StringLength(50)][Required]
        public string Name { get; set; }
        public decimal CreditLimit { get; set; }
        [Required]
        public bool Active { get; set; }

        public Customer Copy (Customer newdata)
        {
            Name = newdata.Name;
            CreditLimit = newdata.CreditLimit;
            Active = newdata.Active;
            return this;
        }
    }
}