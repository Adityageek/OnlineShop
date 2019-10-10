using OnlineShop.DataModel;
using OnlineShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineShop.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountController : Controller
    {
        ///GET: Register
        [HttpGet]
        public ActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            model.States = GetState();
            ViewBag.States = GetState();
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                OnlineMartEntities onlineMartEntities = new OnlineMartEntities();

                User user = new User
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = model.Password,
                    Gender = model.Gender,
                    DateOfBirth = model.DateOfBirth,
                    CityId = model.CityId,
                    StateId = model.StateId,
                    Address = model.Address,
                    Contact = model.Contact
                };

                onlineMartEntities.Users.Add(user);
                onlineMartEntities.SaveChanges();

                return View("Welcome");
            }
            else
            {
                model.States = GetState();
                return View(model);
            }
        }

        public ActionResult Welcome()
        {

            return View();
        }

        public IEnumerable<State> GetState()
        {
            OnlineMartEntities ome = new OnlineMartEntities();
            ome.Configuration.ProxyCreationEnabled = false;
            return ome.States.ToList();
        }

        public JsonResult getCity(int id)
        {
            OnlineMartEntities ome = new OnlineMartEntities();
            var ddlCity = ome.Cities.Where(x => x.StateId == id).ToList();

            List<SelectListItem> selectListItems = new List<SelectListItem>();

            if (ddlCity != null)
            {
                foreach (var x in ddlCity)
                {
                    selectListItems.Add(new SelectListItem { Text = x.City1, Value = x.CityId.ToString() });
                }
            }
            return Json(new { city = new SelectList(selectListItems, "Value", "Text") }, JsonRequestBehavior.AllowGet);
        }


        //Login Method
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {

            LoginViewModel loginModel = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(loginModel);
        }

        [HttpPost]
        [AllowAnonymous]
        //[Authorize]

        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                OnlineMartEntities onlineMartEntities = new OnlineMartEntities();

                //Todo:: Need to make password check case sensitive
                var user = (from userlist in onlineMartEntities.Users
                            where userlist.UserName == model.UserName && userlist.Password == model.Password
                            select new
                            {
                                userlist.UserId,
                                userlist.UserName
                            }).ToList();


                if (user.FirstOrDefault() != null)
                {
                    //Session["UserName"] = user.FirstOrDefault().UserName.ToString();
                    //Session["UserId"] = user.FirstOrDefault().UserId.ToString();
                    Session["UserId"] = Guid.NewGuid(); //Setting User Session 
                    Session["UserName"] = Guid.NewGuid();
                    FormsAuthentication.SetAuthCookie(model.UserName, false);

                    if (!string.IsNullOrEmpty(model.ReturnUrl))
                    {
                        if(Url.IsLocalUrl(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("UserDashBoard");
                        }
                    }
                    else
                    {
                        return RedirectToAction("UserDashBoard");
                    }
                }

                else
                {
                    ModelState.AddModelError("", "Invalid Login Credentials");

                }

            }
            return View(model);


        }

        //[UserAuthenticationFilter]
        [Authorize]
        public ActionResult UserDashBoard()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            return RedirectToAction("Index", "Index");
        }

    }
}