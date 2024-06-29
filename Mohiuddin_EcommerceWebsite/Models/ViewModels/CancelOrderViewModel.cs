using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mohiuddin_EcommerceWebsite.Models.ViewModels
{
    public class CancelOrderViewModel
    {
        [Required]
        public int OrderId { get; set; }
        [Required, Display(Name = "Cancel Date"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CancelDate { get; set; }
        [Required, Display(Name = "What Reason to Cancel?")]
        public string Reason { get; set; }

        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPayable { get; set; }

        public List<OrderDetailViewModel> OrderDetails { get; set; }

        public CancelOrderViewModel()
        {
            OrderDetails = new List<OrderDetailViewModel>();
        }

    }

}