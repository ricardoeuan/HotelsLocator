using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelsApplication.Controllers
{
    public class UsersController : Controller
    {
        private HotelsAppEntities db = new HotelsAppEntities();

        //
        // GET: /Users/
        // RETURNS THE LOGIN VIEW

        public ActionResult Login()
        {
            return View();
        }

        //
        // POST : /Users/Login
        // PERFORMS CREDENTIALS VALIDATION
        // TRUE : RETURNS REDIRECT TO HotelsLocator ACTION
        // FALSE : RETURNS Login VIEW

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users u)
        {
            if (ModelState.IsValid)
            {
                using (db)
                {
                    var validateCredentials = db.Users.Where(a => a.username.Equals(u.username) && a.password.Equals(u.password)).FirstOrDefault();
                    if (validateCredentials != null)
                    {
                        Session["LogedUserID"] = validateCredentials.UserID.ToString();
                        Session["LogedUserFullName"] = validateCredentials.full_name.ToString();
                        return RedirectToAction("HotelsLocator");
                    }
                }              
            }
            return View(u);
        }

        //
        // GET : /Users/HotelsLocator
        // PERFORMS SESSION VALIDATION
        // TRUE : RETURNS HotelsLocator VIEW
        // FALSE : RETURNS REDIRECT TO Login ACTION

        public ActionResult HotelsLocator()
        {
            if (Session["LogedUserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //
        // GET : /Users/AdminUsers
        // RETURNS : AdminUsers VIEW
        // PARAM : Users
        // PARAM TYPE : List

        public ActionResult AdminUsers()
        {
            return View(db.Users.ToList());
        }
    }
}
