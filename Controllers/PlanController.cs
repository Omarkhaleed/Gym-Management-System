using OlympicGym.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OlympicGym.Controllers
{
    public class PlanController : Controller
    {
        CompanyContext _context = new CompanyContext();


    [HttpGet]
        public ActionResult ShowPlan()
        {
            if (Session["AdminId"] != null)
            {
                var plan = _context.Plans.ToList();
                return View(plan);
            }
            else
                return RedirectToAction("Login", "Admin");
        }


    [HttpGet]
        public ActionResult AddPlan()
        {
            if (Session["AdminId"] != null)
            {
                return View();
            }
            else
                return RedirectToAction("Login", "Admin");

        }


    [HttpPost]
        public ActionResult AddPlan(Plan newplan)
        {
            _context.Plans.Add(newplan);
            _context.SaveChanges();
            return RedirectToAction("ShowPlan");

        }


    [HttpGet]
        public ActionResult EditPlan(int id)
        {
            if (Session["AdminId"] != null)
            {
                var plan = _context.Plans.Find(id);
                return View(plan);
            }
            else
                return RedirectToAction("Login", "Admin");
        }


    [HttpPost]
        public ActionResult EditPlan(Plan newplan)
        {
            _context.Entry(newplan).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("ShowPlan");
        }


    [HttpGet]
        public ActionResult DeletePlan(int id)
        {
            if (Session["AdminId"] != null)
            {
                var delete = _context.Plans.Find(id);
                _context.Plans.Remove(delete);
                _context.SaveChanges();
                return RedirectToAction("ShowPlan");
            }
            else
                return RedirectToAction("Login", "Admin");
        }
    }
}
  