using OnlineShop.DataModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        [HttpGet]
        public ActionResult AddProduct()
        {
            Product product = new Product();
            product.Categories = GetProduct();
            
            return View();
        }

       

        [HttpPost]
        public ActionResult AddProduct(Product product) {
           

            return View();
        }

        public IEnumerable<Category> GetProduct()
        {
            OnlineMartEntities onlineMartEntities = new OnlineMartEntities();
            return onlineMartEntities.Categories.ToList();
        }
    }
}