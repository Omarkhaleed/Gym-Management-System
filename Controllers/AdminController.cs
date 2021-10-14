using OlympicGym.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace OlympicGym.Controllers
{
    public class AdminController : Controller
    {
        CompanyContext _context = new CompanyContext();

    [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

    [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Admin admin)
        {


            var login = _context.Admins.Where(pp => pp.FirstName.Equals(admin.FirstName) && pp.Password.Equals(admin.Password)).FirstOrDefault();
            if (login != null)
            {
                Session["AdminId"] = login.AdminId.ToString();
              
                Session["AdminName"] = login.FirstName.ToString();
                return RedirectToAction("Index");
            }

            return View(admin);

        }
        public ActionResult LogOut()
        {
            return RedirectToAction("Login");
        }


    [HttpGet]
        public ActionResult Index()
        {
            if (Session["AdminId"] != null)
            {

                ViewBag.AllTrainees = _context.Trainees.Include(num => num.TraineeId).Count();
                ViewBag.GymTrainees = _context.Trainees.Where(num => num.SportId == 1).Count();
                ViewBag.BoxingTrainees = _context.Trainees.Where(num => num.SportId == 2).Count();
                ViewBag.CrossfitTrainees = _context.Trainees.Where(num => num.SportId == 3).Count();
                return View();
            }
            else
                return RedirectToAction("Login");

        }

    [HttpGet]
        public ActionResult ShowAdmin()
        {
            if (Session["AdminId"] != null)
            {

                ViewBag.manager = Session["AdminId"].ToString();
               var Admin = _context.Admins.ToList();
                return View(Admin);
            }
            else
                return RedirectToAction("Login");
        }

    [HttpGet]
        public ActionResult AddAdmin()
        {
            if (Session["AdminId"] != null)
            {
                ViewBag.manager = Session["AdminId"].ToString();
                return View();
            }
            else
                return RedirectToAction("Login");
        }

    [HttpPost]
        public ActionResult AddAdmin(Admin newadmin)
        {
            if (ModelState.IsValid == false) { return View(); }
            _context.Admins.Add(newadmin);
            _context.SaveChanges();
            return RedirectToAction("ShowAdmin");
        }

    [HttpGet]
        public ActionResult EditAdmin(int id)
        {
            if (Session["AdminId"] != null)
            {
                ViewBag.manager = Session["AdminId"].ToString();
                var admin = _context.Admins.Find(id);
                return View(admin);

            }
            else
               return  RedirectToAction("Login");
          }

    [HttpPost]
        public ActionResult EditAdmin(Admin newadmin)
        {
            if (ModelState.IsValid == false) { return View(); }
            _context.Entry(newadmin).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("ShowAdmin");
        }

    [HttpGet]
        public ActionResult DeleteAdmin(int id)
        {
            if (Session["AdminId"] != null)
            {
                ViewBag.manager = Session["AdminId"].ToString();
                var delete = _context.Admins.Find(id);
                _context.Admins.Remove(delete);
                _context.SaveChanges();
                return RedirectToAction("ShowAdmin");

            }
            else
                return RedirectToAction("Login");
        }
    }
}
      

