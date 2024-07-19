    using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoAnNet.Models;

namespace DoAnNet.Controllers
{
    public class ProductsController : Controller
    {
        private SmartphoneShopEntities db = new SmartphoneShopEntities();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.Vendor);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,Name,Description,Price,Quantity,IsLatestProduct,IsTrendingProduct,IsSpecialProduct,Guarantee,CategoryId,VendorId,DiscountPercent,Img")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", product.CategoryId);
            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "Name", product.VendorId);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", product.CategoryId);
            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "Name", product.VendorId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,Name,Description,Price,Quantity,IsLatestProduct,IsTrendingProduct,IsSpecialProduct,Guarantee,CategoryId,VendorId,DiscountPercent,Img")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", product.CategoryId);
            ViewBag.VendorId = new SelectList(db.Vendors, "VendorId", "Name", product.VendorId);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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

        public ActionResult showProduct()
        {
            return View(db.Products.OrderByDescending(p => p.ProductId).Take(5).ToList());
        }

        public ActionResult Apple()
        {
            var listApple = db.Products.Where(s => s.Category.Name == "Apple").ToList();
            return View(listApple);
        }

        public ActionResult SamSung()
        {
            var listSS = db.Products.Where(s => s.Category.Name == "SamSung").ToList();
            return View(listSS);
        }


        public ActionResult HeadPhone()
        {
            var listSS = db.Products.Where(s => s.Category.Name == "HeadPhone").ToList();
            return View(listSS);
        }

        public ActionResult Search(string txt_search)
        {
            var listE = db.Products.Where(d => d.Name.Contains(txt_search)).ToList();

            // Đếm số sản phẩm tìm được
            int numberOfResults = listE.Count;

            // Gán giá trị numberOfResults vào ViewBag
            ViewBag.NumberOfResults = numberOfResults;

            return View(listE);
        }

    }
}
