using System;
using System.Linq;
using System.Web.Mvc;
using Web.Middlewares;
using Web.Models;

namespace Web.Controllers
{
    public class BaoCaoHoTroController : Controller
    {
        private Context db = new Context();

        [CheckUserSession]
        public ActionResult Index(int page = 1, int limit = 10, string msg = "")
        {
            var baoCaoHoTroList = db.BAOCAO_HOTROs.ToList();
            ViewBag.I = 1; // Đảm bảo rằng ViewBag.I không null

            if (Session["isAdmin"] == null || !(bool)Session["isAdmin"])
            {
                int mahs = int.Parse(Session["mahs"].ToString());
                baoCaoHoTroList = baoCaoHoTroList.Where(b => b.mahs == mahs).ToList();
            }

            return View(baoCaoHoTroList);
        }

        // GET: BaoCaoHoTro/Create
        [CheckUserSession]
        public ActionResult Create()
        {
            ViewBag.LoaiBaoCao = new SelectList(new[]
            {
                new { Value = "Báo cáo", Text = "Báo cáo" },
                new { Value = "Phiếu hỗ trợ", Text = "Phiếu hỗ trợ" }
            }, "Value", "Text");
            return View();
        }

        // POST: BaoCaoHoTro/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BAOCAO_HOTRO baoCaoHoTro)
        {
            if (ModelState.IsValid)
            {
                if (Session["mahs"] == null)
                {
                    ModelState.AddModelError("", "Session không hợp lệ.");
                    ViewBag.LoaiBaoCao = new SelectList(new[]
                    {
                new { Value = "Báo cáo", Text = "Báo cáo" },
                new { Value = "Phiếu hỗ trợ", Text = "Phiếu hỗ trợ" }
            }, "Value", "Text", baoCaoHoTro.loai_bao_cao);
                    return View(baoCaoHoTro);
                }

                int mahs = int.Parse(Session["mahs"].ToString());
                var hocSinh = db.HOCSINHs.Find(mahs);
                if (hocSinh == null)
                {
                    ModelState.AddModelError("", "Học sinh không tồn tại.");
                    ViewBag.LoaiBaoCao = new SelectList(new[]
                    {
                new { Value = "Báo cáo", Text = "Báo cáo" },
                new { Value = "Phiếu hỗ trợ", Text = "Phiếu hỗ trợ" }
            }, "Value", "Text", baoCaoHoTro.loai_bao_cao);
                    return View(baoCaoHoTro);
                }

                baoCaoHoTro.mahs = mahs;
                baoCaoHoTro.ngay_tao = DateTime.Now;
                baoCaoHoTro.da_xu_ly = false;

                db.BAOCAO_HOTROs.Add(baoCaoHoTro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LoaiBaoCao = new SelectList(new[]
            {
        new { Value = "Báo cáo", Text = "Báo cáo" },
        new { Value = "Phiếu hỗ trợ", Text = "Phiếu hỗ trợ" }
    }, "Value", "Text", baoCaoHoTro.loai_bao_cao);
            return View(baoCaoHoTro);
        }

        // GET: BaoCaoHoTro/Details/5
        [CheckUserSession]
        public ActionResult Details(int id)
        {
            var baoCaoHoTro = db.BAOCAO_HOTROs.Find(id);
            if (baoCaoHoTro == null)
            {
                return HttpNotFound();
            }
            return View(baoCaoHoTro);
        }

        // GET: BaoCaoHoTro/Edit/5
        [CheckUserSession]
        public ActionResult Edit(int id)
        {
            var baoCaoHoTro = db.BAOCAO_HOTROs.Find(id);
            if (baoCaoHoTro == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoaiBaoCao = new SelectList(new[]
            {
                new { Value = "Báo cáo", Text = "Báo cáo" },
                new { Value = "Phiếu hỗ trợ", Text = "Phiếu hỗ trợ" }
            }, "Value", "Text", baoCaoHoTro.loai_bao_cao);
            return View(baoCaoHoTro);
        }

        // POST: BaoCaoHoTro/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BAOCAO_HOTRO baoCaoHoTro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(baoCaoHoTro).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoaiBaoCao = new SelectList(new[]
            {
                new { Value = "Báo cáo", Text = "Báo cáo" },
                new { Value = "Phiếu hỗ trợ", Text = "Phiếu hỗ trợ" }
            }, "Value", "Text", baoCaoHoTro.loai_bao_cao);
            return View(baoCaoHoTro);
        }

        // GET: BaoCaoHoTro/Delete/5
        [CheckUserSession]
        public ActionResult Delete(int id)
        {
            var baoCaoHoTro = db.BAOCAO_HOTROs.Find(id);
            if (baoCaoHoTro == null)
            {
                return HttpNotFound();
            }
            return View(baoCaoHoTro);
        }

        // POST: BaoCaoHoTro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var baoCaoHoTro = db.BAOCAO_HOTROs.Find(id);
            if (baoCaoHoTro == null)
            {
                return HttpNotFound();
            }

            db.BAOCAO_HOTROs.Remove(baoCaoHoTro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PhanHoi(int id, string phanHoi)
        {
            var baoCaoHoTro = db.BAOCAO_HOTROs.Find(id);
            if (baoCaoHoTro == null)
            {
                return HttpNotFound();
            }

            baoCaoHoTro.da_xu_ly = true;
            baoCaoHoTro.phan_hoi = phanHoi; // Thêm trường phản hồi vào model
            db.SaveChanges();

            return RedirectToAction("Details", new { id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaPhanHoi(int id, string phanHoi)
        {
            var baoCaoHoTro = db.BAOCAO_HOTROs.Find(id);
            if (baoCaoHoTro == null)
            {
                return HttpNotFound();
            }

            baoCaoHoTro.phan_hoi = phanHoi;
            db.SaveChanges();

            return RedirectToAction("Details", new { id = id });
        }
    }
}
