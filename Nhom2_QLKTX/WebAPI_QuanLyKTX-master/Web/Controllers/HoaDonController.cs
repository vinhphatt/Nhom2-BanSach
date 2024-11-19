using OfficeOpenXml;
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
    public class HoaDonController : Controller
    {
        private Context db = new Context();

        public ActionResult DownloadExcel()
        {

            // If you use EPPlus in a noncommercial context
            // according to the Polyform Noncommercial license:
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                //Set some properties of the Excel document
                excelPackage.Workbook.Properties.Author = "Qawithexperts";
                excelPackage.Workbook.Properties.Title = "test Excel";
                excelPackage.Workbook.Properties.Subject = "Write in Excel";
                excelPackage.Workbook.Properties.Created = DateTime.Now;

                //Create the WorkSheet
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");

                var p_list = db.HOADONs.ToList();

                ExcelPackage Ep = new ExcelPackage();
                ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
                Sheet.Cells["A1"].Value = "Mã hóa đơn";
                Sheet.Cells["B1"].Value = "Mã phiếu điện nước";
                Sheet.Cells["C1"].Value = "Ngày tạo hóa đơn";
                Sheet.Cells["D1"].Value = "Tiền điện nước";
                Sheet.Cells["E1"].Value = "Tiền phòng";
                Sheet.Cells["C1"].Value = "Tổng tiền";
                Sheet.Cells["D1"].Value = "Mã Phòng";
                Sheet.Cells["E1"].Value = "Tên học sinh thanh toán hóa đơn";
                Sheet.Cells["E1"].Value = "Tên nhân viên";
                int row = 2;
                foreach (var item in p_list)
                {

                    Sheet.Cells[string.Format("A{0}", row)].Value = item.mahd;
                    Sheet.Cells[string.Format("B{0}", row)].Value = item.maphieudiennuoc;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.ngaytao;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.tiendiennuoc;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.tienphong;
                    Sheet.Cells[string.Format("C{0}", row)].Value = item.tongtien;
                    Sheet.Cells[string.Format("D{0}", row)].Value = item.maphong;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.tenhs;
                    Sheet.Cells[string.Format("E{0}", row)].Value = item.tennv;
                    row++;
                }


                Sheet.Cells["A:AZ"].AutoFitColumns();
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment: filename=" + "Report.xlsx");
                Response.BinaryWrite(Ep.GetAsByteArray());
                Response.End();

                return View("XuatExcel");
            }
        }

        // API trả về danh sách học sinh theo mã phòng

        public ActionResult GetHocSinhByPhong(string maphong)
        {
            // Kiểm tra maphong có được truyền đúng không
            if (string.IsNullOrEmpty(maphong))
            {
                return Json(new { success = false, message = "Không có mã phòng!" }, JsonRequestBehavior.AllowGet);
            }

            var listHocSinh = db.HOCSINHs
                                .Where(hs => hs.maphong.ToString() == maphong)
                                .Select(hs => new {
                                    hs.mahs,
                                    hs.hoten
                                })
                                .ToList();

            // Kiểm tra dữ liệu có trả về không
            if (listHocSinh.Count == 0)
            {
                return Json(new { success = false, message = "Không tìm thấy học sinh cho phòng này!" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true, data = listHocSinh }, JsonRequestBehavior.AllowGet);
        }


        [CheckUserSession]
        public ActionResult Index(int page = 1, int limit = 10, string tinhtrang = null)
        {
            using (var client = new HttpClient())
            {
                var _host = Request.Url.Scheme + "://" + Request.Url.Authority;
                var _api = Url.Action("get", "HOADON", new { httproute = "DefaultApi", limit = limit, page = page, tinhtrang });
                var _url = _host + _api;

                var responseTask = client.GetAsync(_url);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<HOADON>>(); // Lấy dữ liệu hóa đơn
                    readTask.Wait();
                    IEnumerable<HOADON> list = readTask.Result;

                    // Nếu là Admin, không giới hạn hóa đơn theo phòng
                    if (Session["isAdmin"] != null && (bool)Session["isAdmin"])
                    {
                        // Lọc theo tình trạng nếu có
                        if (!string.IsNullOrEmpty(tinhtrang))
                        {
                            list = list.Where(h => h.tinhtrang == tinhtrang);
                        }
                    }
                    // Nếu là Học sinh, chỉ hiển thị hóa đơn theo mã phòng của học sinh
                    else
                    {
                        // Lấy mã phòng từ Session (kiểu string)
                        var maphong = Session["maphong"]?.ToString();

                        if (!string.IsNullOrEmpty(maphong))
                        {
                            // Chuyển maphong từ string sang int
                            if (int.TryParse(maphong, out int maphongInt))
                            {
                                // Lọc danh sách hóa đơn theo mã phòng của học sinh
                                list = list.Where(h => h.PHONG.maphong == maphongInt);
                            }
                            else
                            {
                                // Trả về View thông báo "Bạn chưa được xếp phòng"
                                return View("NotAssignedRoom");
                            }
                        }
                        else
                        {
                            // Trả về View thông báo "Bạn chưa được xếp phòng"
                            return View("NotAssignedRoom");
                        }
                    }



                    // Pagination data
                    ViewBag.CurrentPage = page;
                    var o_list = db.HOADONs.ToList();
                    ViewBag.TotalPage = Math.Ceiling((float)o_list.Count / limit);
                    ViewBag.I = page > 1 ? (page - 1) * limit + 1 : 1;

                    return View(list.ToList()); // Trả về danh sách hóa đơn đã lọc
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


        // GET: HoaDon/Details/5
        [CheckUserSession]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOADON e = db.HOADONs.Find(id);
            if (e == null)
            {
                return HttpNotFound();
            }
            return View(e);
        }

        [CheckUserSession]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HOADON e)
        {
            if (string.IsNullOrEmpty(e.tinhtrang))
            {
                e.tinhtrang = "Chưa thanh toán"; // Giá trị mặc định
            }

            using (var client = new HttpClient())
            {
                var _host = Request.Url.Scheme + "://" + Request.Url.Authority;
                var _api = Url.Action("post", "HOADON", new { httproute = "DefaultApi" });
                var _url = _host + _api;

                var postTask = client.PostAsJsonAsync<HOADON>(_url, e);
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
                var _api = Url.Action("get", "HOADON", new { httproute = "DefaultApi", id = id });
                var _url = _host + _api;
                var responseTask = client.GetAsync(_url);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<HOADON>();
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
        // Action lấy học sinh theo ID phòng
        public JsonResult GetStudentsByRoom(int roomId)
        {
            // Lấy danh sách học sinh dựa vào maphong (ID phòng)
            var students = db.HOCSINHs
                             .Where(s => s.maphong == roomId)  // Kiểm tra maphong trong bảng HOCSINH
                             .Select(s => new { Id = s.mahs, Name = s.hoten })  // Lấy Id và Name
                             .ToList();

            return Json(students, JsonRequestBehavior.AllowGet); // Trả về dữ liệu dạng JSON
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HOADON e)
        {
            using (var client = new HttpClient())
            {
                var _host = Request.Url.Scheme + "://" + Request.Url.Authority;
                var _api = Url.Action("edit", "HOADON", new { httproute = "DefaultApi" });
                var _url = _host + _api;

                var responseTask = client.PutAsJsonAsync<HOADON>(_url, e);
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
                    return View("~/Views/Shared/Error.cshtml");
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
                var _api = Url.Action("delete", "HOADON", new { httproute = "DefaultApi", id = id });
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


        [HttpGet]
        public ActionResult ThanhToan(int id)
        {
            // Thêm logic kiểm tra id hợp lệ, ví dụ:
            var hoaDon = db.HOADONs.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound(); // Nếu không tìm thấy hóa đơn
            }

            // Nếu cần, thêm logic xử lý trước khi hiển thị giao diện
            return View(hoaDon);
        }


        [HttpPost]
        public ActionResult XacNhanThanhToan(int id)
        {
            var hoaDon = db.HOADONs.Find(id);

            if (hoaDon != null && hoaDon.PHONG.maphong == Convert.ToInt32(Session["maphong"]))
            {
                // Cập nhật tình trạng hóa đơn thành "Đã thanh toán"
                hoaDon.tinhtrang = "Đã thanh toán";
                db.SaveChanges();

                // Chuyển hướng đến danh sách hóa đơn sau khi thanh toán
                return RedirectToAction("Index", "HoaDon");
            }
            else
            {
                // Nếu không có quyền thanh toán, chuyển hướng về danh sách hóa đơn
                return RedirectToAction("Index", "HoaDon");
            }
        }


    }
}
