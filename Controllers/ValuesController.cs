using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using OlympicGym.Models;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web;
using System.Data.Entity;
using System.IO;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Threading.Tasks;

namespace OlympicGym.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
  
    public class ValuesController : ApiController
    {

        //// To get Coaches
        //[System.Web.Http.HttpGet]
        //public IEnumerable<Coach>GetTrainers()
        //{
          
        //    using (CompanyContext ss = new CompanyContext())
        //    {
        //        ss.Configuration.LazyLoadingEnabled = false;
        //        return ss.Coaches.ToList();

        //    }
        //    //return new string[] { "value1", "value2" };


        //}

        // To get member info
        [ResponseType(typeof(Service))]
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> Data(int id, string phoneNumber)
        {

            using (CompanyContext ss = new CompanyContext())
            {
                ss.Configuration.LazyLoadingEnabled = false;

                var book = await ss.Trainees.Where(pp=>pp.TraineeId==id && pp.PhoneNumber==phoneNumber).Select(b =>
                new Service()
                {
                    FirstName  =  b.FirstName,
                    SecondName = b.SecondName,
                    DateOfRegister = b.DateOfRegister,
                    Record = b.Record,
                    Classes = b.classes,
                    Sport = b.Sport.SportName,
                    Coach = b.Coach.FirstName,
                    Plan = b.Plan.PlanName

                }).SingleOrDefaultAsync();
                if (book == null)
                {
                    return NotFound();
                }

                return Ok(book);

            }
        }
        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> TopTrainees()
        {

            using (CompanyContext ss = new CompanyContext())
            {
                ss.Configuration.LazyLoadingEnabled = false;
                var vv= ss.Trainees.OrderByDescending(pp => pp.Record).Take(3).Select(pp=> new Service()
                {
                    FirstName=pp.FirstName,
                   SecondName= pp.SecondName,
                   Sport=pp.Sport.SportName,
                     Record= pp.Record 

                }).ToList();

                if (vv == null)
                {
                    return NotFound();
                }
                
                return Ok(vv);
            }
            //return new string[] { "value1", "value2" };


        }

        [System.Web.Http.HttpGet]
        public async Task<IHttpActionResult> GetCoaches()
        {

            using (CompanyContext ss = new CompanyContext())
            {
                ss.Configuration.LazyLoadingEnabled = false;
                var vv = ss.Coaches.Where(pp=>pp.CoachId!=8).Select(pp => new Service()
                {
                    FirstName = pp.FirstName,
                    SecondName = pp.SecondName,
                    Sport = pp.Sport.SportName,
                    FaceBook=pp.FaceBook,
                    Instagram=pp.Instagram,
                    WhatsApp=pp.PhoneNumber

                }).ToList();

                if (vv == null)
                {
                    return NotFound();
                }

                return Ok(vv);
            }
            //return new string[] { "value1", "value2" };


        }

        //  }

        //// GET: api/values
        //public IEnumerable<string> Get()
        //{
        //     return new string[] { "value1", "value2" };
        //    //var Trainees = _context.Trainees.Where(pp=>pp.TraineeId==57).ToList();
        //    //return Trainees;
        //}

        //// GET: api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/values
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/values/5
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/values/5
        //public void Delete(int id)
        //{
        //}
    }
}