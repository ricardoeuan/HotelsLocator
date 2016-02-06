using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace HotelsApplication.Controllers
{
    public class HotelsAPIController : ApiController
    {
        private HotelsAppEntities db = new HotelsAppEntities();       

        // GET api/HotelsAPI
        public IEnumerable<Hotels> GetHotels()
        {
            return db.Hotels.AsEnumerable();
        }

        // GET api/HotelsAPI/5
        public Hotels GetHotels(int id)
        {
            Hotels hotels = db.Hotels.Find(id);
            if (hotels == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return hotels;
        }

        // PUT api/HotelsAPI/5
        public HttpResponseMessage PutHotels(int id, Hotels hotels)
        {
            if (ModelState.IsValid && id == hotels.HotelID)
            {
                db.Entry(hotels).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/HotelsAPI
        public HttpResponseMessage PostHotels(Hotels hotels)
        {
            if (ModelState.IsValid)
            {
                db.Hotels.Add(hotels);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, hotels);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = hotels.HotelID }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/HotelsAPI/5
        public HttpResponseMessage DeleteHotels(int id)
        {
            Hotels hotels = db.Hotels.Find(id);
            if (hotels == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Hotels.Remove(hotels);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, hotels);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}