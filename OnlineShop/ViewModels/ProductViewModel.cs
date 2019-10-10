using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.DataModel;

namespace OnlineShop.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        [Display(Name = "Product Title")]
        [Required(ErrorMessage = "Enter Product Title")]
        public string ProductTitle { get; set; }

        [Display(Name = "Launch Date")]
        [Required(ErrorMessage = "Enter Launch Date")]
        [DataType(DataType.Date)]
        public System.DateTime LaunchDate { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Enter Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "MRP")]
        [Required(ErrorMessage = "Enter MRP")]
        public double Mrp { get; set; }

        [Display(Name = "Discount")]
        [Required(ErrorMessage = "Enter Discount")]
        public int Discount { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Enter Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Enter Description")]
        public string Description { get; set; }

        [AllowHtml]
        [Display(Name = "Image")]
        [Required(ErrorMessage = "Please Select Image")]
        public String Image { get; set; }

        public double SellingPrice { get; set; }

        public virtual Category Category { get; set; }

       
    }
}