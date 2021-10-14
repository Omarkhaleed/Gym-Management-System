using OlympicGym.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OlympicGym.Controllers
{
    public class CoachController : Controller
    {
        CompanyContext _context = new CompanyContext();

    [HttpGet]
        public ActionResult ShowCoach()
        {
            if (Session["AdminId"] != null)
            {
              
                var coach = _context.Coaches.Include(pp => pp.Sport).OrderBy(pp => pp.SportId).ToList();
                return View(coach);
            }
            else
                return RedirectToAction("Login", "Admin");

           
        }

    [HttpGet]
        public ActionResult AddCoach()
        {
            if (Session["AdminId"] != null)
            {
                ViewBag.sport = new SelectList(_context.Sports.ToList(), "SportId", "SportName", 1);
                return View();
            }
            else
                return RedirectToAction("Login", "Admin");

        }

    [HttpPost]
        public ActionResult AddCoach(Coach newcoach)
        {
            if (ModelState.IsValid == false)
            {
                ViewBag.sport = new SelectList(_context.Sports.ToList(), "SportId", "SportName", 1);
                return View();
            }
            var sport = _context.Sports.Find(newcoach.SportId);
            sport.TotalCoaches++;
            var AllSports = _context.Sports.Find(13);
            AllSports.TotalCoaches++;
            _context.Coaches.Add(newcoach);
            _context.SaveChanges();
            return RedirectToAction("ShowCoach");

        }

    [HttpGet]
        public ActionResult EditCoach(int id)
        {
            if (Session["AdminId"] != null)
            {
                var coach = _context.Coaches.Find(id);
                ViewBag.sport = new SelectList(_context.Sports.ToList(), "SportId", "SportName", coach.SportId);

                return View(coach);
            }
            else
                return RedirectToAction("Login", "Admin");

        }

    [HttpPost]
        public ActionResult EditCoach(Coach newcoach)
        {
            if (ModelState.IsValid == false)
            {
                var coach = _context.Coaches.Find(newcoach.CoachId);
                ViewBag.sport = new SelectList(_context.Sports.ToList(), "SportId", "SportName", coach.SportId);

                return View(coach);
            }
            _context.Entry(newcoach).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("ShowCoach");
        }

    [HttpGet]
        public ActionResult DeleteCoach(int id)
        {
            if (Session["AdminId"] != null)
            {
                var coach = _context.Coaches.Find(id);
                var sport = _context.Sports.Find(coach.SportId);
                sport.TotalCoaches--;
                var AllSports = _context.Sports.Find(13);
                AllSports.TotalCoaches--;
                _context.Coaches.Remove(coach);
                _context.SaveChanges();
                return RedirectToAction("ShowCoach");
            }
            return RedirectToAction("Login", "Admin");

        }
    }
}


 
