using OnlineShop.DataModel;
using OnlineShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        OnlineMartEntities onlineMartEntities = new OnlineMartEntities();
        public ActionResult AddToCart(ProductViewModel product)
        {
            
            if (Session["cart"] == null)
            {
                //OnlineMartEntities onlineMartEntities = new OnlineMartEntities();
                //onlineMartEntities.Configuration.ProxyCreationEnabled = false;
                //var productList = onlineMartEntities.Products.ToList();
                List<ProductViewModel> productList = new List<ProductViewModel>();
                productList.Add(product);
                Session["cart"] = productList;
                ViewBag.cart = productList.Count();
                Session["count"] = 1;

            }

            else
            {
                List<ProductViewModel> productList = (List<ProductViewModel>)Session["cart"];
                productList.Add(product);
                Session["cart"] = productList;
                ViewBag.cart = productList.Count();
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;

            }
            return RedirectToAction("MyOrder", "Cart");
        }

        public ActionResult Myorder()
        {

            return View((List<ProductViewModel>)Session["cart"]);

        }
    }
}