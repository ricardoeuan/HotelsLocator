using HotelsApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelsApplication.Controllers
{
    public class HotelsController : Controller
    {
        private HotelsAppEntities db = new HotelsAppEntities();
        private HotelsViewModel hotelsModel = new HotelsViewModel();


        public ActionResult Index()
        {
            hotelsModel.HotelsList = db.Hotels.ToList();
            hotelsModel.SelectedHotel = null;
            return View(hotelsModel);
        }


        public ActionResult Details(int id = 0)
        {
            hotelsModel.HotelsList = db.Hotels.ToList();
            hotelsModel.SelectedHotel = db.Hotels.Find(id);
            hotelsModel.DisplayMode = "displayHotel";            
            if (hotelsModel.SelectedHotel == null)
            {
                return HttpNotFound();
            }
            return PartialView("ShowHotel", hotelsModel.SelectedHotel);
        }
        
        public ActionResult Create()
        {
            hotelsModel.HotelsList = db.Hotels.ToList();
            hotelsModel.SelectedHotel = null;
            hotelsModel.DisplayMode = "addHotel";
            return PartialView("insertHotel", hotelsModel.SelectedHotel);
        }

        [HttpPost]
        public ActionResult Insert(Hotels hotels)
        {
            if (ModelState.IsValid)
            {
                db.Hotels.Add(hotels);
                db.SaveChanges();

                hotelsModel.HotelsList = db.Hotels.ToList();
                hotelsModel.SelectedHotel = db.Hotels.Find(hotels.HotelID);
//                hotelsModel.DisplayMode = "displayHotel";             
            }
            return View("Index", hotelsModel);
        }


        [HttpPost]    
        public ActionResult Edit(int id = 0)
        {
            hotelsModel.HotelsList = db.Hotels.ToList();
            hotelsModel.SelectedHotel = db.Hotels.Find(id);
            hotelsModel.DisplayMode = "updateHotel";
            if (hotelsModel.SelectedHotel == null)
            {
                return HttpNotFound();
            }
            return View("Index", hotelsModel);
        }

        [HttpPost]
        public ActionResult Update(Hotels hotels)
        {
            if (ModelState.IsValid)
            {
                Hotels currentHotel = db.Hotels.Find(hotels.HotelID);
                currentHotel.hotel_name = hotels.hotel_name;
                currentHotel.hotel_latitude = hotels.hotel_latitude;
                currentHotel.hotel_longitude = hotels.hotel_longitude;
                currentHotel.address = hotels.address;
                currentHotel.phone = hotels.phone;
                currentHotel.category = hotels.category;
                currentHotel.lowest_rate = hotels.lowest_rate;
                currentHotel.hotel_description = hotels.hotel_description;
                db.SaveChanges();

                hotelsModel.HotelsList = db.Hotels.ToList();
                hotelsModel.SelectedHotel = currentHotel;
                //hotelsModel.DisplayMode = "displayHotel";
            }
            return View("Index", hotelsModel);
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!id.Equals(null))
            {
                Hotels currentHotel = db.Hotels.Find(id);
                db.Hotels.Remove(currentHotel);
                db.SaveChanges();

                hotelsModel.HotelsList = db.Hotels.ToList();
                hotelsModel.SelectedHotel = null;
                hotelsModel.DisplayMode = "";
            }
            return JavaScript("location.reload(true)");
        }

        [HttpPost]
        public ActionResult Cancel(int id)
        {
            hotelsModel.HotelsList = db.Hotels.ToList();
            hotelsModel.SelectedHotel = db.Hotels.Find(id);
            hotelsModel.DisplayMode = "displayHotel";
            return View("Index", hotelsModel);

        }

        public ActionResult APIClient()
        {
            return View("APIClient");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}