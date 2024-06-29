﻿namespace Mohiuddin_EcommerceWebsite.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Subtotal { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }

}