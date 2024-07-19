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
            List<Product> listProduct = new List<Product>();
            string conStr = ConfigurationManager.ConnectionStrings["connect1"].ConnectionString;
            string sql = "SELECT TOP 3 P.*, C.Name AS CategoryName FROM Products P INNER JOIN Categories C ON P.CategoryId = C.CategoryId ORDER BY P.ProductId DESC;";

            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var pro = new Product();
                        pro.ProductId = (int)reader["ProductId"];
                        pro.Img = reader["Img"].ToString();
                        pro.Name = reader["Name"].ToString();
                        pro.CategoryId = (int)reader["CategoryId"];
                        pro.Description = reader["Description"].ToString();
                        pro.Price = (decimal)reader["Price"];

                        // Tạo một đối tượng Category và gán tên cho nó
                        pro.Category = new Category();
                        pro.Category.Name = reader["CategoryName"].ToString(); // Đọc tên từ cột Alias "CategoryName"

                        listProduct.Add(pro);
                    }
                    reader.Close();
                }
            }

            return View(listProduct);
        }

    }
}
