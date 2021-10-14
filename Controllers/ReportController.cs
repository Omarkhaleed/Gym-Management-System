using OlympicGym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace OlympicGym.Controllers
{

    public class ReportController : Controller
    {

        CompanyContext _context = new CompanyContext();
        
     [HttpGet]
        public ActionResult ShowReports()
        {

            if (Session["AdminId"] != null)
            {
                ViewBag.manager = Session["AdminId"].ToString();
                var report = _context.Reports.Include(pp => pp.Sport).Include(pp => pp.Coach).Include(pp => pp.Admin).ToList();
                return View(report);
            }
            else
                return RedirectToAction("Login", "Admin");
        }


    [HttpGet]
        public ActionResult AddReports()
        {
            // viewbag for ForeignKey
            if (Session["AdminId"] != null)
            {
                ViewBag.manager = Session["AdminId"].ToString();
                SelectBags();
                return View();
            }
            else
                return RedirectToAction("Login", "Admin");
        }

    [HttpPost]
        public ActionResult AddReports(Report newreport)
        {
            if (ModelState.IsValid == false)
            {
                //send again viewbag
                SelectBags();
                return View();

            }

            int NumTrainees;
           
            if (newreport.CoachId == 8 && newreport.SportId == 13)
            {
                NumTrainees = _context.Trainees.Where(num => num.DateOfRegister >= newreport.DateOfStarting
                                                 && num.DateOfRegister <= newreport.DateOfEnding).Count();
            }
            else if (newreport.CoachId == 8)
            {
                NumTrainees = _context.Trainees.Where(num => num.SportId == newreport.SportId
                                                 && num.DateOfRegister >= newreport.DateOfStarting
                                                 && num.DateOfRegister <= newreport.DateOfEnding).Count();
               var Trainees = _context.Trainees.Where(num => num.SportId == newreport.SportId
                                                && num.DateOfRegister >= newreport.DateOfStarting
                                                && num.DateOfRegister <= newreport.DateOfEnding).ToList();
              
            }

            else
            {
                NumTrainees = _context.Trainees.Where(num => num.SportId == newreport.SportId
                                                 && num.CoachId == newreport.CoachId
                                                 && num.DateOfRegister >= newreport.DateOfStarting
                                                 && num.DateOfRegister <= newreport.DateOfEnding).ToList().Count();
            }
            newreport.Num_Of_Trainees = NumTrainees;
            var price = 0;
            if (newreport.CoachId == 8 && newreport.SportId == 13)
            {
                 var Trainees = _context.Trainees.Where(num => num.DateOfRegister >= newreport.DateOfStarting
                                                && num.DateOfRegister <= newreport.DateOfEnding).ToList();
                foreach (var n in Trainees)
                {
                    int a = n.PlanId;
                    var plan = _context.Plans.Find(a).PlanPrice;
                    price += plan;
                }
                //var PlanPrice = Plan.PlanPrice;
                newreport.TotalPrice = price;
            }
            else if (newreport.CoachId == 8) {
                var Trainees = _context.Trainees.Where(num => num.SportId == newreport.SportId
                                               && num.DateOfRegister >= newreport.DateOfStarting
                                               && num.DateOfRegister <= newreport.DateOfEnding).ToList();
                foreach (var n in Trainees)
                {
                    int a = n.PlanId;
                    var plan = _context.Plans.Find(a).PlanPrice;
                    price += plan;
                }
                //var PlanPrice = Plan.PlanPrice;
                newreport.TotalPrice = price;
            }
            else
                newreport.TotalPrice = newreport.Price * newreport.Num_Of_Trainees;

            _context.Reports.Add(newreport);
            _context.SaveChanges();
            return RedirectToAction("ShowReports");
        }

    [HttpGet]
        public ActionResult EditReport(int id)
        {
            if (Session["AdminId"] != null)
            {
                ViewBag.manager = Session["AdminId"].ToString();
                var report = _context.Reports.Find(id);
                SelectBags(report);
                return View(report);
            }
            else
                return RedirectToAction("Login", "Admin");
        }

    [HttpPost]
        public ActionResult EditReport(Report newreport)
        {
            if (ModelState.IsValid == false)
            {
                var report = _context.Reports.Find(newreport.ReportId);
                SelectBags(report);
                return View(report);
            }
            _context.Entry(newreport).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("ShowReports");
        }

    [HttpGet]
        public ActionResult DeleteReport(int id)
        {
            if (Session["AdminId"] != null)
            {
                ViewBag.manager = Session["AdminId"].ToString();
                var report = _context.Reports.Find(id);
                _context.Reports.Remove(report);
                _context.SaveChanges();
                return RedirectToAction("ShowReports");
            }
            else
                return RedirectToAction("Login", "Admin");

        }

    [HttpGet]
        public ActionResult PrintReport(int id)
        {
            if (Session["AdminId"] != null)
            {
                ViewBag.manager = Session["AdminId"].ToString();
                var report = _context.Reports.Find(id);


                return View(report);
            }
            else
                return RedirectToAction("Login", "Admin");

        }
        public void SelectBags()
        {
            ViewBag.sport = new SelectList(_context.Sports.ToList(), "SportId", "SportName", 1);
            ViewBag.coach = new SelectList(_context.Coaches.ToList(), "CoachId", "FirstName", 1);
            ViewBag.admin = new SelectList(_context.Admins.ToList(), "AdminId", "FirstName", 1);
        }
        public void SelectBags(Report report)
        {
            ViewBag.sport = new SelectList(_context.Sports.ToList(), "SportId", "SportName", report.SportId);
            ViewBag.coach = new SelectList(_context.Coaches.ToList(), "CoachId", "FirstName", report.CoachId);
            ViewBag.admin = new SelectList(_context.Admins.ToList(), "AdminId", "FirstName", report.AdminId);
        }
    }
}