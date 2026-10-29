using _25DH110096_TH_BE_1_8.Models;
using _25DH110096_TH_BE_1_8.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace _25DH110096_TH_BE_1_8.Controllers
{
    public class AccountController : Controller
    {
        private TH_BE_2026Entities db = new TH_BE_2026Entities();

        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = db.Users.SingleOrDefault(u => u.Username == model.Username);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Username", "Tên đăng nhập này đã tồn tại!");
                    return View(model);
                }
                var user = new User
                {
                    Username = model.Username,
                    Password = model.Password,
                    UserRole = "Customer"
                };

                db.Users.Add(user);
                var customer = new Customer
                {
                    CustomerName = model.CustomerName,
                    CustomerEmail = model.CustomerEmail,
                    CustomerPhone = model.CustomerPhone,
                    CustomerAddress = model.CustomerAddress,
                    Username = model.Username,
                };
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid) {

                var user = db.Users.SingleOrDefault(u => u.Username == model.Username
                && u.Password == model.Password
                && u.UserRole == "Customer");
                if (user != null)
                {
                    Session["Username"] = user.Username;
                    Session["UserRole"] = user.UserRole;

                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập mật khẩu không đúng");
                }    
            }
            return View(model);

        }
    }
}