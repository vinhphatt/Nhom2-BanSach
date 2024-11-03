using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Web.Models;
using PagedList;

namespace Web.APIs
{
    [RoutePrefix("api/phong")]
    public class PHONG_APIController : ApiController
    {

        private Context db = new Context();


        [Route("get")]
        public List<PHONG> GetList(int? page=1, int? limit = 0)
        {
            var links = db.PHONGs.OrderBy(x => x.maphong).ToList();
            int pageNumber = (page ?? 1);
            var result = new List<PHONG>();

            if(limit<=0)
            {
                result = db.PHONGs.ToList();
            }
            else
                result = links.ToPagedList(pageNumber, (int)limit).ToList();               

            return result;
        }

        [Route("get/{id}")]
        public PHONG GetSingle(int id)
        {
            var result = db.PHONGs.FirstOrDefault(x=>x.maphong==id);
            return result;
        }


        [Route("edit/{id}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult Edit(PHONG e)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(e).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PHONGExists(e.maphong))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        [Route("post")]
        [ResponseType(typeof(PHONG))]
        public IHttpActionResult Create(PHONG e)
        {
            try
            {
                if (!ModelState.IsValid || e == null)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    db.PHONGs.Add(e);
                    db.SaveChanges();
                }
            }
            catch (DbUpdateException ex)
            {
                throw ex;
                if (PHONGExists(e.maphong))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = e.maphong }, e);
        }

        [Route("delete/{id}")]
        [HttpDelete]
        [ResponseType(typeof(PHONG))]
        public IHttpActionResult Delete(int id)
        {
            PHONG PHONG = db.PHONGs.Find(id);
            if (PHONG == null)
            {
                return NotFound();
            }

            db.PHONGs.Remove(PHONG);
            db.SaveChanges();

            return Ok(PHONG);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PHONGExists(int id)
        {
            return db.PHONGs.Count(e => e.maphong == id) > 0;
        }
    }
}
