using OnlineShop.DataModel;
using OnlineShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using PagedList;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        public double SellingPrice { get; private set; }

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
        public Image byteArrayToImage(byte[] bytesArr)
        {
            using (MemoryStream memstr = new MemoryStream(bytesArr))
            {
                Image img = Image.FromStream(memstr);
                return img;
            }
        }

        [HttpGet]
        public ActionResult ViewProduct(int? page)
        {
            OnlineMartEntities onlineMartEntities = new OnlineMartEntities();
            onlineMartEntities.Configuration.ProxyCreationEnabled = false;
            var Products = onlineMartEntities.Products.ToList();
            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfProducts = Products.ToPagedList(pageNumber, 25); // will only contain 25 products max because of the pageSize

            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View();
        }
        //Retrieve and convert image to display it rn the uri is too long...
        public JsonResult getProduct() {
            OnlineMartEntities onlineMartEntities = new OnlineMartEntities();
            onlineMartEntities.Configuration.ProxyCreationEnabled = false;
            var Products = onlineMartEntities.Products.ToList();

            List<ProductViewModel> productList = new List<ProductViewModel>();

            foreach (var product in Products)
            {
                string img = String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(product.Image));

                var ProductsViewModel = new ProductViewModel
                {
                    ProductId = product.ProductId,
                    ProductTitle = product.ProductTitle,
                    LaunchDate = product.LaunchDate,
                    Quantity = product.Quantity,
                    Mrp = product.Mrp,
                    Discount = product.Discount,
                    CategoryId = product.CategoryId,
                    Description = product.Description,
                    Image = img,
                    SellingPrice = product.SellingPrice
                };
                productList.Add(ProductsViewModel);
            }

            return Json(productList, JsonRequestBehavior.AllowGet);
            

        }
    }

    
}