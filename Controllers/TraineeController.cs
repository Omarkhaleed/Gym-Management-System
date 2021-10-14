using OlympicGym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
namespace OlympicGym.Controllers
{
    public class TraineeController : Controller
    {

        CompanyContext _context = new CompanyContext();
         
    [HttpGet]
        public ActionResult ShowTrainees()
        {
            if (Session["AdminId"] != null){
                var Trainees = _context.Trainees.Include(pp => pp.Sport).Include(pp => pp.Plan)
                                                .Include(pp => pp.Admin).Include(pp => pp.Coach)
                                                .ToList();
                return View(Trainees);
            }
            else
                    return RedirectToAction("Login", "Admin");
        }
        


    [HttpGet]
        public ActionResult Show_Gym_Trainees()
        {
            if (Session["AdminId"] != null)

            {
                var Trainees = _context.Trainees.Where(pp => pp.SportId == 1).ToList();

                return View(Trainees);
            }
            else
                return RedirectToAction("Login", "Admin");
        }


    [HttpGet]
        public ActionResult Show_Boxing_Trainees()
        {
            var Trainees = _context.Trainees.Where(pp =>pp.SportId==2).ToList();
          
            return View(Trainees);
        }

        [HttpGet]
        public ActionResult Show_Trainees(int id)
        {
            if (Session["AdminId"] != null)
            {
                if (id ==13) {
                    var Trainees = _context.Trainees.Include(pp => pp.Sport).Include(pp => pp.Plan)
                                                   .Include(pp => pp.Admin).Include(pp => pp.Coach)
                                                   .ToList();
                    ViewBag.name = "All_Sports";
                    return View(Trainees);
                }
                else {
                    var Trainees = _context.Trainees.Where(pp => pp.SportId == id).ToList();
                    ViewBag.name = _context.Sports.Where(pp => pp.SportId == id).FirstOrDefault().SportName;
                    return View(Trainees);
                }

            }
            else
                return RedirectToAction("Login", "Admin");
        }

        [HttpGet]
        public ActionResult Show_Coaches(int id)
        {
            if (Session["AdminId"] != null)
            {
                if (id == 13)
                {
                    var Coaches = _context.Coaches.Include(pp => pp.Sport).ToList();
                    ViewBag.name = "All_Sports";
                    return View(Coaches);
                }
                else
                {
                    var Coaches = _context.Coaches.Where(pp => pp.SportId == id).ToList();
                    ViewBag.name = _context.Sports.Where(pp => pp.SportId == id).FirstOrDefault().SportName;
                    return View(Coaches);
                }

            }
            else
                return RedirectToAction("Login", "Admin");
        }


        [HttpGet]
        public ActionResult Show_Crossfit_Trainees()
        {
            if (Session["AdminId"] != null)

            {
                var Trainees = _context.Trainees.Where(pp => pp.SportId == 3).ToList();

                return View(Trainees);
            }
            else
                return RedirectToAction("Login", "Admin");
        }

    [HttpGet]
        public ActionResult TopTrainees()
        {
            if (Session["AdminId"] != null)
            {
               
                ViewBag.manager = Session["AdminId"].ToString();
                var n = _context.Sports.Count();
              
                    //var TopRecord = _context.Trainees.OrderByDescending(pp => pp.Record).OrderBy(pp => pp.SportId).Take(2);
               
                var TopRecord = _context.Trainees.GroupBy(pp => pp.SportId).
                   Select(pp => pp.OrderByDescending(p => p.Record).FirstOrDefault());


                return View(TopRecord);
            }
            else
                return RedirectToAction("Login", "Admin");
        }

    [HttpGet]
        public ActionResult RefreshTrainees()
        {
            if (Session["AdminId"] != null)
            {
                var Refresh = _context.Trainees.Where(pp => pp.Record > 0);
               
                foreach ( var  refresh  in Refresh)
                {
                    refresh.Record = 0;
                    
                }
                _context.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }
            else
                return RedirectToAction("Login", "Admin");
        }


        [HttpGet]
        public ActionResult FindTrainees(int id)
        {
            if (Session["AdminId"] != null) {

                var trainee = _context.Trainees.Where(pp => pp.TraineeId == id)
                       .Include(pp => pp.Plan).Include(pp => pp.Admin).ToList();
                return View(trainee);
            }
            else
                return RedirectToAction("Login", "Admin");

        }


    [HttpGet]
        public ActionResult FindTrainees2(string phone)
        {
            if (Session["AdminId"] != null)

            {

                var trainee = _context.Trainees.Where(pp => pp.PhoneNumber == phone)
                           .Include(pp => pp.Plan).Include(pp => pp.Admin).ToList();

                return View(trainee);
            }
            else
                return RedirectToAction("Login", "Admin");
        }


    [HttpGet]
        public ActionResult AttendTrainees(int id)
        {
            if (Session["AdminId"] != null)

            {

                var attend = _context.Trainees.Find(id);
                attend.classes--;
                _context.SaveChanges();
                return RedirectToAction("Index", "Admin");
            
        }
            else
                return RedirectToAction("Login", "Admin");
        }

    [HttpGet]
        public ActionResult BonusTrainees(int id)
        {
            if (Session["AdminId"] != null)

            {

                var Bonus = _context.Trainees.Find(id);
                Bonus.Record+=3;
                _context.SaveChanges();
                return RedirectToAction("Index", "Admin");

            }
            else
                return RedirectToAction("Login", "Admin");
        }

        [HttpGet]
        public ActionResult RenewTrainees(int id)
        {

            if (Session["AdminId"] != null)

            {
                // renew current plan with the same classes
                var renew = _context.Trainees.Find(id);
                var plan = _context.Plans.Find(renew.PlanId);
                renew.classes = plan.PlanClasses;
                //renew new date 
                renew.DateOfRegister = DateTime.Now;
                _context.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }
            else
                return RedirectToAction("Login", "Admin");
        }


    [HttpGet]
        public ActionResult AddTrainees()
        {
            if (Session["AdminId"] != null) {

                SelectBags();
                return View();

            }
            else
                return RedirectToAction("Login", "Admin");
        }

       
    [HttpPost]
        public ActionResult AddTrainees( Trainee newtrainee)
        {
            if (ModelState.IsValid == false) { SelectBags(); return View(); }
            var coach = _context.Coaches.Find(newtrainee.CoachId);
            coach.TotalTrainees++;
            var sport = _context.Sports.Find(newtrainee.SportId);
            sport.TotalTrainees++;
            var AllSports = _context.Sports.Find(13);
            AllSports.TotalTrainees++;
              
            _context.Trainees.Add(newtrainee);
            _context.SaveChanges();
            return RedirectToAction("ShowTrainees");
         }        


    [HttpGet]
        public ActionResult EditTrainees(int id)
        {
            if (Session["AdminId"] != null)

            {
                var trainee = _context.Trainees.Find(id);
                SelectBags(trainee);
                return View(trainee);
            }
            else
                return RedirectToAction("Login", "Admin");
        }


    [HttpPost]
        public ActionResult EditTrainees(Trainee newtrainee)
        {

            if (ModelState.IsValid == false)
            {
                var trainee = _context.Trainees.Find(newtrainee.TraineeId);
                SelectBags(trainee);
                return View();
            }
            
            _context.Entry(newtrainee).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("ShowTrainees");
        }


    [HttpGet]
        public ActionResult DeleteTrainees(int id)
        {
            if (Session["AdminId"] != null)
            {
                var delete = _context.Trainees.Find(id);
                var sport = _context.Sports.Find(delete.SportId);
                sport.TotalTrainees--;
                var AllSports = _context.Sports.Find(13);
                AllSports.TotalTrainees--;

                _context.Trainees.Remove(delete);
                
                //Another way
                //_context.Entry(delete).State = EntityState.Deleted;
                _context.SaveChanges();
                return RedirectToAction("ShowTrainees");
            }
            else
                return RedirectToAction("Login", "Admin");
        }

        public  void SelectBags()
        {
            ViewBag.sport = new SelectList(_context.Sports.ToList(), "SportId", "SportName", 1);
            ViewBag.coach = new SelectList(_context.Coaches.ToList(), "CoachId", "FirstName", 1);
            ViewBag.plan = new SelectList(_context.Plans.ToList(), "PlanId", "PlanName", 1);
            ViewBag.classes = new SelectList(_context.Plans.ToList(), "PlanId", "PlanClasses", 1);
            ViewBag.admin = new SelectList(_context.Admins.ToList(), "AdminId", "FirstName", 1);
        }
        public void SelectBags(Trainee trainee)
        {

            ViewBag.sport = new SelectList(_context.Sports.ToList(), "SportId", "SportName", trainee.SportId);
            ViewBag.coach = new SelectList(_context.Coaches.ToList(), "CoachId", "FirstName", trainee.CoachId);
            ViewBag.plan = new SelectList(_context.Plans.ToList(), "PlanId", "PlanName", trainee.PlanId);
            ViewBag.classes = new SelectList(_context.Plans.ToList(), "PlanId", "PlanClasses", trainee.PlanId);
            ViewBag.admin = new SelectList(_context.Admins.ToList(), "AdminId", "FirstName", trainee.AdminId);
        }
    }
}