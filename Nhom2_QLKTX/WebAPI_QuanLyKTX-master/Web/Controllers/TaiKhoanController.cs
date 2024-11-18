using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Web.Middlewares;
using Web.Models;

namespace Web.Controllers
{
    public class TaiKhoanController : Controller
    {
        private Context db = new Context();

        [CheckUserSession]
        public ActionResult Index(int page = 1, int limit = 10, string msg = "")
        {
            using (var client = new HttpClient())
            {
                if (!string.IsNullOrEmpty(msg))
                {
                    ViewBag.Msg = msg;
                }
                var _host = Request.Url.Scheme + "://" + Request.Url.Authority;
                var _api = Url.Action("get", "taikhoan", new { httproute = "DefaultApi", limit = limit, page = page });
                var _url = _host + _api;
                var responseTask = client.GetAsync(_url);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<TAIKHOAN>>();
                    readTask.Wait();
                    IEnumerable<TAIKHOAN> list = null;
                    list = readTask.Result;
                    ViewBag.CurrentPage = page;
                    var o_list = new Context().TAIKHOANs.ToList();
                    ViewBag.TotalPage = Math.Ceiling((float)o_list.Count / 10);
                    ViewBag.TotalPage_List = o_list;
                    ViewBag.I = 1;
                    if (ViewBag.CurrentPage > 1)
                    {
                        ViewBag.I = (ViewBag.CurrentPage - 1) * 10 + 1; //số thứ tự tiếp theo 
                    }
                    return View("~/Views/Taikhoan/Index.cshtml", list.ToList());
                }
                else
                {
                    ViewBag.Msg = result.ReasonPhrase;
                    ViewBag.Url_Error = _url;
                    ViewBag.Code = (int)result.StatusCode;
                    return View("~/Views/Shared/Error.cshtml");
                }
            }
        }

        [CheckUserSession]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAIKHOAN tAIKHOAN = db.TAIKHOANs.Find(id);
            if (tAIKHOAN == null)
            {
                return HttpNotFound();
            }
            return View(tAIKHOAN);
        }

        [CheckUserSession]
 public ActionResult Create()
 {
     return View();
 }

 [HttpPost]
 [ValidateAntiForgeryToken]
 public ActionResult Create([Bind(Include = "matk,hoten,email,pass")] TAIKHOAN e, string hoten, DateTime? ngaysinh, string gioitinh, string confirmPass)
 {
     if (e.pass != confirmPass)
     {
         ViewBag.Msg = "Mật khẩu và xác thực mật khẩu không khớp!";
         return View();
     }

     var exist = db.TAIKHOANs.FirstOrDefault(x => x.email == e.email);
     if (exist != null)
     {
         ViewBag.Msg = "Email đã tồn tại, vui lòng chọn email khác!";
         return View();
     }

     // Gán mặc định cvu là "Học Sinh"
     e.cvu = "Học Sinh";

     using (var client = new HttpClient())
     {
         var _host = Request.Url.Scheme + "://" + Request.Url.Authority;
         var _api = Url.Action("post", "taikhoan", new { httproute = "DefaultApi" });
         var _url = _host + _api;

         var postTask = client.PostAsJsonAsync<TAIKHOAN>(_url, e);
         postTask.Wait();

         var result = postTask.Result;
         if (result.IsSuccessStatusCode)
         {
             // Lấy matk từ kết quả trả về
             var createdTaiKhoan = result.Content.ReadAsAsync<TAIKHOAN>().Result;
             var matk = createdTaiKhoan.matk;

             // Kiểm tra các trường không được null
             if (string.IsNullOrEmpty(hoten) || !ngaysinh.HasValue || string.IsNullOrEmpty(gioitinh))
             {
                 ViewBag.Msg = "Vui lòng điền đầy đủ thông tin học sinh!";
                 return View();
             }

             // Thêm dữ liệu vào bảng HOCSINH_NEW
             var newHocSinhNew = new HOCSINH_NEW
             {
                 hoten = hoten,
                 ngaysinh = ngaysinh,
                 gioitinh = gioitinh == "true", // Giả sử giới tính là "true" hoặc "false"
                 matk = matk // Thêm matk vào HOCSINH_NEW
             };

             db.HOCSINH_NEW.Add(newHocSinhNew);
             db.SaveChanges();

             return RedirectToAction("Index");
         }
         else
         {
             ViewBag.Msg = result.ReasonPhrase;
             ViewBag.Url_Error = _url;
             ViewBag.Code = (int)result.StatusCode;
             return View("~/Views/Shared/Error.cshtml");
         }
     }
 }

 [CheckUserSession]
 [HttpGet]
 public ActionResult Edit(int? id)
 {
     using (var client = new HttpClient())
     {
         var _host = Request.Url.Scheme + "://" + Request.Url.Authority;
         var _api = Url.Action("get", "taikhoan", new { httproute = "DefaultApi", id = id });
         var _url = _host + _api;
         var responseTask = client.GetAsync(_url);
         responseTask.Wait();

         var result = responseTask.Result;
         if (result.IsSuccessStatusCode)
         {
             var readTask = result.Content.ReadAsAsync<TAIKHOAN>();
             readTask.Wait();
             var e = readTask.Result;
             return View(e);
         }
         else
         {
             ViewBag.Msg = result.ReasonPhrase;
             ViewBag.Url_Error = _url;
             ViewBag.Code = (int)result.StatusCode;
             return View("~/Views/Shared/Error.cshtml");
         }
     }
 }

 [HttpPost]
 [ValidateAntiForgeryToken]
 public ActionResult Edit([Bind(Include = "matk,hoten,email,pass,cvu")] TAIKHOAN e, string hoten, DateTime? ngaysinh, string gioitinh, string confirmPass)
 {
     if (e.pass != confirmPass)
     {
         ViewBag.Msg = "Mật khẩu và xác thực mật khẩu không khớp!";
         return View(e);
     }

     var exist = db.TAIKHOANs.FirstOrDefault(x => x.email == e.email && x.matk != e.matk);
     if (exist != null)
     {
         ViewBag.Msg = "Email đã tồn tại, vui lòng chọn email khác!";
         return View(e);
     }
     e.cvu = "Học Sinh";

     // Cập nhật thông tin tài khoản
     db.Entry(e).State = EntityState.Modified;

     // Cập nhật thông tin học sinh
     var hocSinhNew = db.HOCSINH_NEW.FirstOrDefault(x => x.matk == e.matk);
     if (hocSinhNew != null)
     {
         hocSinhNew.hoten = hoten;
         hocSinhNew.ngaysinh = ngaysinh;
         hocSinhNew.gioitinh = gioitinh == "true"; // Giả sử giới tính là "true" hoặc "false"
     }

     var hocSinh = db.HOCSINHs.FirstOrDefault(x => x.matk == e.matk);
     if (hocSinh != null)
     {
         hocSinh.hoten = hoten;
         hocSinh.ngaysinh = ngaysinh;
         hocSinh.gioitinh = gioitinh == "true"; // Giả sử giới tính là "true" hoặc "false"
     }

     db.SaveChanges();

     return RedirectToAction("Index");
 }

        [CheckUserSession]
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            using (var client = new HttpClient())
            {
                var _host = Request.Url.Scheme + "://" + Request.Url.Authority;
                var _api = Url.Action("delete", "taikhoan", new { httproute = "DefaultApi", id = id });
                var _url = _host + _api;
                var deleteTask = client.DeleteAsync(_url);
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Msg = result.ReasonPhrase;
                    ViewBag.Url_Error = _url;
                    ViewBag.Code = (int)result.StatusCode;
                    return View("~/Views/Shared/Error.cshtml");
                }
            }

        }
    }
}
