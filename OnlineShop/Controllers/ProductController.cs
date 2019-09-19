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
            product.Categories = GetCategory();
           //   ViewBag.Categories = GetCategory();
            return View(product);
        }

       

        [HttpPost]
        public ActionResult AddProduct(Product product) {
            HttpPostedFileBase file = Request.Files["ImageData"];
            int addProduct = AddProducts(file, product);
            if (addProduct == 1) {
                return RedirectToAction("ViewProduct");
            }
            return View(product);
        }

        public IEnumerable<Category> GetCategory()
        {
            OnlineMartEntities onlineMartEntities = new OnlineMartEntities();
            onlineMartEntities.Configuration.ProxyCreationEnabled = false;
            return onlineMartEntities.Categories.ToList();
        }

        public int AddProducts(HttpPostedFileBase file, Product productModel) {
            OnlineMartEntities onlineMartEntities = new OnlineMartEntities();
            productModel.Image = ConvertToBytes(file);
            var Product = new Product
            {
                ProductTitle = productModel.ProductTitle,
                LaunchDate = productModel.LaunchDate,
                Quantity = productModel.Quantity,
                Mrp = productModel.Mrp,
                Discount = productModel.Discount,
                CategoryId= productModel.CategoryId,
                Description = productModel.Description,
                Image = productModel.Image,
                SellingPrice = productModel.SellingPrice
            };

            onlineMartEntities.Products.Add(Product);
            int insertedRow = onlineMartEntities.SaveChanges();
            if (insertedRow == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }

        [HttpGet]
        public ActionResult ViewProduct() {
            OnlineMartEntities onlineMartEntities = new OnlineMartEntities();
            onlineMartEntities.Configuration.ProxyCreationEnabled = false;
            List<Product> products = onlineMartEntities.Products.ToList();

            return View(products);
        }


    }

    
}