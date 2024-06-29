using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mohiuddin_EcommerceWebsite.Models
{
    public class OrderCancel
    {
        [Key]
        public int OrderCancelId { get; set; }
        [Required]
        [Display(Name ="Order ID")]
        public int OrderId { get; set; }
        [Required, Display(Name = "Cancel Date"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CancelDate { get; set; }

        [Required, Display(Name = "What Reason to Cancel?"), DataType(DataType.Date)]
        public string Reason { get; set; }

        public virtual Order Order { get; set; }
    }


}