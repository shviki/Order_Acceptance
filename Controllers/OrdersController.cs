using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Order_Acceptance.Models;

namespace Order_Acceptance.Controllers
{
    public class OrdersController : Controller
    {
        private OrdersEntities db = new OrdersEntities();

        // GET: Orders
        public ActionResult Index(string searchId)
        {
            var orders = db.Orders.AsQueryable();

            if (!string.IsNullOrEmpty(searchId))
            {
                if (int.TryParse(searchId, out int id))
                {
                    orders = orders.Where(o => o.Id == id);
                }
                else
                {
                    orders = orders.Where(o => false);
                }
                ViewBag.CurrentSearch = searchId;
            }

            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SenderCity,SenderAddress,RecipientCity,RecipientAddress,PickupDate")] Order order, string Weight)
        {
            if (!string.IsNullOrEmpty(Weight))
            {
                Weight = Weight.Replace(",", ".");

                if (decimal.TryParse(Weight, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal weightValue))
                {
                    order.Weight = weightValue;
                }
                else
                {
                    ModelState.AddModelError("Weight", "Введите корректный вес (например: 15.5)");
                }
            }
            else
            {
                ModelState.AddModelError("Weight", "Пожалуйста, укажите вес груза");
            }

            if (ModelState.IsValid)
            {
                order.CreatedAt = DateTime.Now;
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}