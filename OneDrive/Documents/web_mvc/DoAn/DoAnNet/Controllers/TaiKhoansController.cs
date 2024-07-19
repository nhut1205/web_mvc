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
        public ActionResult DangKi()
        {
            return View();
        }


        [HttpPost]
        private void AddUserToDatabase(string userName, string password, string email, string tenHienThi)
        {
            // Đoạn mã kết nối đến cơ sở dữ liệu (thay thế chuỗi kết nối bằng chuỗi kết nối của bạn)
            string connectionString = ConfigurationManager.ConnectionStrings["connect1"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Tạo câu truy vấn SQL để chèn người dùng vào bảng tbl_User
                string insertQuery = "INSERT INTO TaiKhoan (TenTaiKhoan, MatKhau , Email , TenHienThi ) VALUES (@UserName, @Password , @Email , @TenHienThi)";

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    // Thêm các tham số cho câu truy vấn
                    command.Parameters.AddWithValue("@UserName", userName);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@TenHienThi", tenHienThi);
                    // Thực hiện truy vấn chèn dữ liệu
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public ActionResult DangKi(TaiKhoan model)
        {
            if (ModelState.IsValid)
            {
                // Gọi hàm AddUserToDatabase để thêm người dùng vào cơ sở dữ liệu
                AddUserToDatabase(model.TenTaiKhoan, model.MatKhau , model.Email , model.TenHienThi);

                // Đặt thông báo thành công
                TempData["SuccessMessage"] = "Đăng ký thành công";
            }

            // Nếu ModelState không hợp lệ, trả lại view với thông báo lỗi
            return View(model);
        }

        //public ActionResult DangNhap(TaiKhoan model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Đặt chuỗi kết nối cơ sở dữ liệu của bạn ở đây
        //        string connectionString = ConfigurationManager.ConnectionStrings["connect1"].ConnectionString;

        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            connection.Open();

        //            // Tạo câu truy vấn SQL để kiểm tra tên người dùng và mật khẩu
        //            string query = "SELECT TenTaiKhoan FROM TaiKhoan WHERE TenTaiKhoan = @UserName AND MatKhau = @Password";

        //            using (SqlCommand command = new SqlCommand(query, connection))
        //            {
        //                // Thêm tham số cho câu truy vấn
        //                command.Parameters.AddWithValue("@UserName", model.TenTaiKhoan);
        //                command.Parameters.AddWithValue("@Password", model.MatKhau);

        //                // Thực hiện truy vấn
        //                string userName = (string)command.ExecuteScalar();
        //                string password = (string)command.ExecuteScalar();
        //                if (userName != null && password != null)
        //                {
        //                    TempData["SuccessMessage"] = "Đăng nhập thành công";
        //                }
        //                else
        //                {
        //                    // Đăng nhập thất bại, thêm thông báo lỗi vào ModelState
        //                    TempData["ErorMessage"] = "Đăng nhập thất bại";
        //                    ModelState.AddModelError("UserName", "Tên người dùng hoặc mật khẩu không đúng.");
        //                }
        //            }
        //        }
        //    }

        //    // Nếu ModelState không hợp lệ hoặc đăng nhập thất bại, trả lại view đăng nhập với thông báo lỗi
        //    return View(model);
        //}


        public ActionResult DangNhap(TaiKhoan model)
        {
            if (ModelState.IsValid)
            {
                // Đặt chuỗi kết nối cơ sở dữ liệu của bạn ở đây
                string connectionString = ConfigurationManager.ConnectionStrings["connect1"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Tạo câu truy vấn SQL để kiểm tra tên người dùng, mật khẩu và quyền
                    string query = "SELECT TenTaiKhoan, Quyen FROM TaiKhoan WHERE TenTaiKhoan = @UserName AND MatKhau = @Password";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Thêm tham số cho câu truy vấn
                        command.Parameters.AddWithValue("@UserName", model.TenTaiKhoan);
                        command.Parameters.AddWithValue("@Password", model.MatKhau);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Lấy giá trị từ cột "Quyen" để kiểm tra quyền
                                string quyen = reader["Quyen"].ToString();

                                if (quyen == "Admin")
                                {
                                    // Người dùng có quyền admin, chuyển hướng tới trang admin
                                    TempData["SuccessMessage"] = "Đăng nhập thành công (quyền Admin)";
                                    return RedirectToAction("Create", "Products");
                                }
                                else
                                {
                                    // Người dùng không phải admin, chuyển hướng tới trang chính
                                    TempData["SuccessMessage"] = "Đăng nhập thành công";
                                    return RedirectToAction("Index", "Home");
                                }
                            }
                            else
                            {
                                // Đăng nhập thất bại, thêm thông báo lỗi vào ModelState
                                TempData["ErorMessage"] = "Đăng nhập thất bại";
                                ModelState.AddModelError("UserName", "Tên người dùng hoặc mật khẩu không đúng.");
                            }
                        }
                    }
                }
            }

            // Nếu ModelState không hợp lệ hoặc đăng nhập thất bại, trả lại view đăng nhập với thông báo lỗi
            return View(model);
        }



    }
}
