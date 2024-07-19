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
using System.Reflection;

namespace DoAnNet.Controllers
{
    public class TaiKhoansController : Controller
    {
        private SmartphoneShopEntities db = new SmartphoneShopEntities();

        // GET: TaiKhoans
        public ActionResult Index()
        {
            return View(db.TaiKhoans.ToList());
        }

        // GET: TaiKhoans/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // GET: TaiKhoans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaiKhoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TenTaiKhoan,MatKhau,Email,Quyen,TenHienThi")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.TaiKhoans.Add(taiKhoan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(taiKhoan);
        }

        // GET: TaiKhoans/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // POST: TaiKhoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TenTaiKhoan,MatKhau,Email,Quyen,TenHienThi")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taiKhoan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taiKhoan);
        }

        // GET: TaiKhoans/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // POST: TaiKhoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            db.TaiKhoans.Remove(taiKhoan);
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


        [HttpGet]
        public ActionResult Regiter()
        {
            return View();
        }
        public ActionResult Register(TaiKhoan user)
        {
            if (ModelState.IsValid)
            {
                var check = db.TaiKhoans.FirstOrDefault(s => s.TenTaiKhoan == user.TenTaiKhoan);
                if (check == null)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.TaiKhoans.Add(user);
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Đăng ký thành công";
                }
                else
                {
                    ViewBag.TB = "Tai khoan da ton tai";
                    return View();
                }
            }
            return View();
        }


        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult login(TaiKhoan user)
        {
            var taiKhoanForm = user.TenTaiKhoan;
            var matKhauForm = user.MatKhau;
            var userCheck = db.TaiKhoans.SingleOrDefault(x => x.TenTaiKhoan.Equals(taiKhoanForm) && x.MatKhau.Equals(matKhauForm));

            if (userCheck != null)
            {
                Session["TaiKhoan"] = userCheck;

                // Kiểm tra quyền của người dùng
                if (userCheck.Quyen == "Admin") // Giả sử có một trường là IsAdmin để xác định quyền admin
                {
                    
                    return RedirectToAction("Create", "Admin");
                }
                else
                {
                    return RedirectToAction("showProduct", "Products");
                }
            }
            else
            {
                ViewBag.LoginFail = "Đăng nhập thất bại";
                return View("login");
            }
        }


        //Logout
        public ActionResult logOut()
        {
            Session.Clear();
            return RedirectToAction("showProduct", "Products");
        }

        public ActionResult UserProfile()
        {
            // Lấy thông tin người dùng từ Session
            var userLoggedIn = Session["TaiKhoan"] as TaiKhoan;

            if (userLoggedIn != null)
            {
                return View(userLoggedIn);
            }

            return RedirectToAction("Login"); // Chuyển hướng đến trang đăng nhập nếu không có người dùng đăng nhập.
        }

        public ActionResult EditProfile()
        {
            var userLoggedIn = Session["TaiKhoan"] as TaiKhoan;

            if (userLoggedIn != null)
            {
                return View(userLoggedIn);
            }

            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult UpdateProfile(TaiKhoan updatedUser)
        {
            var userLoggedIn = Session["TaiKhoan"] as TaiKhoan;

            if (userLoggedIn != null)
            {
                // Update thông tin của userLoggedIn với thông tin mới từ updatedUser.
                userLoggedIn.TenHienThi = updatedUser.TenHienThi;
                userLoggedIn.MatKhau = updatedUser.MatKhau;


                // Sử dụng Entity Framework để lưu lại thông tin người dùng
                try
                {
                    using (var context = new SmartphoneShopEntities())
                    {
                        var userInDatabase = context.TaiKhoans.Find(userLoggedIn.TenTaiKhoan);
                        if (userInDatabase != null)
                        {
                            userInDatabase.TenHienThi = updatedUser.TenHienThi;
                            userInDatabase.MatKhau = updatedUser.MatKhau;
                            context.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Ghi ra lỗi để xác định vấn đề
                    Console.WriteLine(ex.Message);
                }
                db.SaveChanges();
                return RedirectToAction("login"); // Chuyển hướng đến trang thông tin người dùng sau khi cập nhật.
            }

            return RedirectToAction("Login");
        }



    }
}
