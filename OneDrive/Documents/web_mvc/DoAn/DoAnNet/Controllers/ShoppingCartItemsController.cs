using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoAnNet.Models;

namespace DoAnNet.Controllers
{
    public class ShoppingCartItemsController : Controller
    {
        private SmartphoneShopEntities db = new SmartphoneShopEntities();

        // GET: ShoppingCartItems
        public ActionResult Index()
        {
            var shoppingCartItems = db.ShoppingCartItems.Include(s => s.Product);
            return View(shoppingCartItems.ToList());
        }

        // GET: ShoppingCartItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCartItem shoppingCartItem = db.ShoppingCartItems.Find(id);
            if (shoppingCartItem == null)
            {
                return HttpNotFound();
            }
            return View(shoppingCartItem);
        }

        // GET: ShoppingCartItems/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name");
            return View();
        }

        // POST: ShoppingCartItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShoppingCartItemId,ProductId,Amount,ShoppingCartId")] ShoppingCartItem shoppingCartItem)
        {
            if (ModelState.IsValid)
            {
                db.ShoppingCartItems.Add(shoppingCartItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", shoppingCartItem.ProductId);
            return View(shoppingCartItem);
        }

        // GET: ShoppingCartItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCartItem shoppingCartItem = db.ShoppingCartItems.Find(id);
            if (shoppingCartItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", shoppingCartItem.ProductId);
            return View(shoppingCartItem);
        }

        // POST: ShoppingCartItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShoppingCartItemId,ProductId,Amount,ShoppingCartId")] ShoppingCartItem shoppingCartItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shoppingCartItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", shoppingCartItem.ProductId);
            return View(shoppingCartItem);
        }

        // GET: ShoppingCartItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCartItem shoppingCartItem = db.ShoppingCartItems.Find(id);
            if (shoppingCartItem == null)
            {
                return HttpNotFound();
            }
            return View(shoppingCartItem);
        }

        // POST: ShoppingCartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShoppingCartItem shoppingCartItem = db.ShoppingCartItems.Find(id);
            db.ShoppingCartItems.Remove(shoppingCartItem);
            db.SaveChanges();
            return RedirectToAction("Index");
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
