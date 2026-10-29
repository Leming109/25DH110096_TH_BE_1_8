using _25DH110096_TH_BE_1_8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using _25DH110096_TH_BE_1_8.Models.ViewModel;


namespace _25DH110096_TH_BE_1_8.Controllers
{
    public class OrderController : Controller
    {
        private TH_BE_2026Entities db = new TH_BE_2026Entities();

        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Checkout()
        {

            var cart = Session["Cart"] as List<CartItem>;
            if (cart == null || !cart.Any())
            {
                return RedirectToAction("Index", "Home");
            }

            var user = db.Users.SingleOrDefault(u => u.Username == User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
                
            var customer = db.Customers.SingleOrDefault(c => c.Username == user.Username);
            if (customer == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var model = new CheckoutVM
            {
                CartItems = cart,
                //TotalAmount = cart.Sum(item => item.TotalPrice),
                OrderDate = DateTime.Now,
                ShippingAddress = customer.CustomerAddress,
                CustomerID = customer.CustomerID,
                Username = customer.Username
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        public ActionResult Checkout(CheckoutVM model)
        {
            if (ModelState.IsValid)
            {
                var cart = Session["Cart"] as List<CartItem>;
                if (cart == null || !cart.Any())
                {
                    return RedirectToAction("Index", "Home");
                }
                var user = db.Users.SingleOrDefault(u => u.Username == User.Identity.Name);
                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                var customer = db.Customers.SingleOrDefault(c => c.Username == user.Username);
                if (customer == null)
                {

                    return RedirectToAction("Login", "Account");
                }
                if (model.PaymentMethod == "Paypal")
                {
                    return RedirectToAction("PaymentWithPaypal", "Paypal", model);
                }

                string paymentStatus = "chưa thanh toán";
                switch (model.PaymentMethod)
                {
                    case "Tiền mặt": paymentStatus = "Thanh toán tiền mặt"; break;
                    case "Paypal": paymentStatus = "Thanh toán Paypal"; break;
                    case "Mua trước trả sau": paymentStatus = "Trả góp"; break;
                    default: paymentStatus = "Chưa thanh toán"; break;
                }

                //var order = new Order
                //{
                //    CustomerID = customer.CustomerID,
                //    OrderDate = model.OrderDate,
                //    TotalAmount = model.TotalAmount,
                //    PaymentStatus = paymentStatus,
                //    PaymentMethod = model.PaymentMethod,
                //    ShippingMethod = model.ShippingMethod,
                //    ShippingAddress = model.ShippingAddress,
                //    OrderDetails = cart.Select(item => new OrderDetail {
                //    {
                //        ProductID = item.ProductID,
                //        Quantity = item.Quantity,
                //        UnitPrice = item.UnitPrice,
                //        TotalPrice = item.TotalPrice
                //    })
                //};

                //db.Orders.Add(order);
                db.SaveChanges();

                Session["Cart"] = null;

                //return RedirectToAction("OrderSuccess", new { id = order.OrderID });
            }
            return View(model);
        }
    }
}