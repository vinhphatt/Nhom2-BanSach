using System;
using System.Linq;
using System.Web.Http;
using Web.Models;

namespace Web.Controllers.Api
{
    public class BaoCaoHoTroApiController : ApiController
    {
        private Context db = new Context();

        // GET: api/BaoCaoHoTroApi
        public IHttpActionResult Get()
        {
            var baoCaoHoTroList = db.BAOCAO_HOTROs.ToList();
            return Ok(baoCaoHoTroList);
        }

        // GET: api/BaoCaoHoTroApi/5
        public IHttpActionResult Get(int id)
        {
            var baoCaoHoTro = db.BAOCAO_HOTROs.Find(id);
            if (baoCaoHoTro == null)
            {
                return NotFound();
            }
            return Ok(baoCaoHoTro);
        }

        // POST: api/BaoCaoHoTroApi
        public IHttpActionResult Post([FromBody] BAOCAO_HOTRO baoCaoHoTro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hocSinh = db.HOCSINHs.Find(baoCaoHoTro.mahs);
            if (hocSinh == null)
            {
                return BadRequest("Học sinh không tồn tại.");
            }

            baoCaoHoTro.ngay_tao = DateTime.Now;
            baoCaoHoTro.da_xu_ly = false;

            db.BAOCAO_HOTROs.Add(baoCaoHoTro);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = baoCaoHoTro.mabao_cao }, baoCaoHoTro);
        }

        // PUT: api/BaoCaoHoTroApi/5
        public IHttpActionResult Put(int id, [FromBody] BAOCAO_HOTRO baoCaoHoTro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != baoCaoHoTro.mabao_cao)
            {
                return BadRequest();
            }

            db.Entry(baoCaoHoTro).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }

        // DELETE: api/BaoCaoHoTroApi/5
        public IHttpActionResult Delete(int id)
        {
            var baoCaoHoTro = db.BAOCAO_HOTROs.Find(id);
            if (baoCaoHoTro == null)
            {
                return NotFound();
            }

            db.BAOCAO_HOTROs.Remove(baoCaoHoTro);
            db.SaveChanges();

            return Ok(baoCaoHoTro);
        }

    }
}
