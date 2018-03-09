using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustOrderWebAPI.Models
{
    public class OrderLine
    {
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int LineNbr { get; set; }
        [Required]
        public string Product { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal LineTotal { get; set; }


        public OrderLine Copy(OrderLine newdata)
        {
            OrderId = newdata.OrderId;
            LineNbr = newdata.LineNbr;
            Product = newdata.Product;
            Quantity = newdata.Quantity;
            LineTotal = newdata.LineTotal;
            return this;
        }

        public void SetQuantity(int amount)
        {
            Quantity = amount;
            CalculateTotal();
        }
        public void SetPrice(decimal price)
        {
            Price = price;
            CalculateTotal();
        }
        public void CalculateTotal()
        {
            LineTotal = Price * Quantity;
        }
    }
}