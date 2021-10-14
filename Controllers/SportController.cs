using OlympicGym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace OlympicGym.Controllers
{   
     
    public class SportController : Controller
    {
        CompanyContext _context = new CompanyContext();

        [HttpGet]
        public ActionResult ShowSport()
        {
            if (Session["AdminId"] != null)
            {
                
                var Sport = _context.Sports.OrderBy(pp => pp.SportName).ToList();
                //ViewBag.Sport = _context.Trainees.Where(pp => pp.SportId == pp.Sport.SportId).Count();
                return View(Sport);
            }
            else
                return RedirectToAction("Login", "Admin");

        }
        [HttpGet]
        public ActionResult AddSport()
        {
            if (Session["AdminId"] != null)
            {
                return View();
            }
            else
                return RedirectToAction("Login", "Admin");

        }
        [HttpPost]
        public ActionResult AddSport( Sport newsport)
        {
            _context.Sports.Add(newsport);
            _context.SaveChanges();
            return RedirectToAction("ShowSport");
          
        }
        [HttpGet]
        public ActionResult EditSport(int id)
        {
            if (Session["AdminId"] != null)
            {

                var sport = _context.Sports.Find(id);
                return View(sport);
            }
            else
                return RedirectToAction("Login", "Admin");
        }
        [HttpPost]
        public ActionResult EditSport(Sport newsport)
        {
            _context.Entry(newsport).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("ShowSport");
        }
        [HttpGet]
        public ActionResult DeleteSport(int id)
        {
            if (Session["AdminId"] != null)
            {

                var delete = _context.Sports.Find(id);
                _context.Sports.Remove(delete);
                _context.SaveChanges();
                return RedirectToAction("ShowSport");
            }
            else
                return RedirectToAction("Login", "Admin");
        }
    }
}