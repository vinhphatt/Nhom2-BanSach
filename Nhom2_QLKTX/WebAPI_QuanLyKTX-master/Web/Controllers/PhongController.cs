using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Web.Middlewares;
using Web.Models;

namespace Web.Controllers
{
    public class PhongController : Controller
    {
        // GET: Phong
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
                var _api = Url.Action("get", "phong", new { httproute = "DefaultApi", limit = limit, page = page });
                var _url = _host + _api;

                var responseTask = client.GetAsync(_url);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<PHONG>>();
                    readTask.Wait();
                    IEnumerable<PHONG> list = null;
                    list = readTask.Result;

                    // Cập nhật trạng thái phòng nếu số lượng học sinh bằng 0
                    using (var db = new Context())
                    {
                        foreach (var phong in list)
                        {
                            if (phong.HOCSINHs.Count == 0)
                            {
                                var phongToUpdate = db.PHONGs.Find(phong.maphong);
                                if (phongToUpdate != null)
                                {
                                    phongToUpdate.tinhtrang = false;
                                    db.SaveChanges();
                                }
                            }
                        }
                    }

                    // Sắp xếp danh sách phòng sao cho các phòng hoạt động được ưu tiên lên đầu
                    list = list.OrderByDescending(p => p.tinhtrang).ToList();

                    ViewBag.CurrentPage = page;
                    var o_list = new Context().PHONGs.ToList();
                    ViewBag.TotalPage = Math.Ceiling((float)o_list.Count / 10);
                    ViewBag.TotalPage_List = o_list;
                    ViewBag.I = 1;
                    if (ViewBag.CurrentPage > 1)
                    {
                        ViewBag.I = (ViewBag.CurrentPage - 1) * 10 + 1; //số thứ tự tiếp theo 
                    }
                    return View(list.ToList());
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

            PHONG phong = db.PHONGs.Find(id);
            if (phong == null)
            {
                return HttpNotFound();
            }

            return View(phong);
        }

        [CheckUserSession]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PHONG e)
        {
            // Thiết lập giá trị mặc định cho tinhtrang là false (Chưa hoạt động)
            e.tinhtrang = false;

            // Kiểm tra xem tên phòng đã tồn tại hay chưa
            using (var db = new Context())
            {
                if (db.PHONGs.Any(p => p.tenphong == e.tenphong))
                {
                    ModelState.AddModelError("tenphong", "Tên phòng đã tồn tại.");
                    return View(e);
                }
            }

            using (var client = new HttpClient())
            {
                var _host = Request.Url.Scheme + "://" + Request.Url.Authority;
                var _api = Url.Action("post", "PHONG", new { httproute = "DefaultApi" });
                var _url = _host + _api;

                var postTask = client.PostAsJsonAsync<PHONG>(_url, e);
                postTask.Wait();

                var result = postTask.Result;
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


        [CheckUserSession]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            using (var client = new HttpClient())
            {
                var _host = Request.Url.Scheme + "://" + Request.Url.Authority;
                var _api = Url.Action("get", "PHONG", new { httproute = "DefaultApi", id = id });
                var _url = _host + _api;
                var responseTask = client.GetAsync(_url);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<PHONG>();
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
        public ActionResult Edit(PHONG e)
        {
            using (var client = new HttpClient())
            {
                var _host = Request.Url.Scheme + "://" + Request.Url.Authority;
                var _api = Url.Action("edit", "PHONG", new { httproute = "DefaultApi" });
                var _url = _host + _api;
                var responseTask = client.PutAsJsonAsync<PHONG>(_url, e);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Msg = result.ReasonPhrase;
                    ViewBag.Url_Error = _url;
                    ViewBag.Code = (int)result.StatusCode;
                    return RedirectToAction("Index", new { msg = ViewBag.Msg });
                }
            }
        }

        [CheckUserSession]
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            using (var client = new HttpClient())
            {
                var _host = Request.Url.Scheme + "://" + Request.Url.Authority;
                var _api = Url.Action("delete", "PHONG", new { httproute = "DefaultApi", id = id });
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
