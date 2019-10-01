using OnlineShop.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public JsonResult GetCategory()
        {
            OnlineMartEntities onlineMartEntities = new OnlineMartEntities();
            List<Category> categories=  onlineMartEntities.Categories.ToList();
            return Json(categories, JsonRequestBehavior.AllowGet);
        }
    }
}