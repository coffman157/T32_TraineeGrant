using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using T32_TraineeGrant;

namespace Admin.Controllers
{
    

    public class AdminController : Controller
    {
        private readonly BumcOrgContext _context;

        public AdminController(BumcOrgContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var context = new BumcOrgContext();
            DateTime DT = new DateTime();
            DT = Convert.ToDateTime("1/1/25").Date;

            //var results = from c in context.Users
            //              join cn in context.UsersStudents on c.UserId equals cn.UserId
            //              join ct in context.UsersStudentsEnrollments on cn.UserId equals ct.UserId
            //              join cl in context.IClerkships on ct.ClerkshipId equals cl.ClerkshipId
            //              where ct.EnrollmentStatus == 1 && ct.ClassGrade == null && ct.StartDate.Value.Month >= 1 && ct.StartDate.Value.Year >= 2025
            //              select new { userid = c.UserId, buid = cn.DemoEmployeeId, startdate = ct.StartDate, enddate = ct.EndDate, lastname = c.Lastname, firstname = c.Firstname, clerkshipid = ct.ClerkshipId, coursename = cl.ClerkshipName, enrollmentid = ct.EnrollmentId };
            return View();
        }
        public async Task<IActionResult> Abstracts()
        {
            //int proj1 = Convert.ToInt32(HttpContext.Session.GetString("project"));
            //string buid = HttpContext.Session.GetString("buid");
            //var trainee = _context.People.Where(a => a.Buid == buid).FirstOrDefault();
            //ViewBag.buid = trainee.Buid;
            //ViewBag.personid = trainee.Id;
            //ViewBag.firstname = trainee.Firstname;

            //ViewBag.lastname = trainee.Lastname;
            //var projname = _context.TrainingGrants.Where(a => a.Id == proj1).FirstOrDefault();
            //ViewBag.projname = projname.Title;
            return View(await _context.AdminReportsAbstracts.ToListAsync());
        }
        public async Task<IActionResult> Others()
        {
         
            return View(await _context.AdminReportsOthers.ToListAsync());
        }
        public async Task<IActionResult> Manuscripts()
        {
            //int proj1 = Convert.ToInt32(HttpContext.Session.GetString("project"));
            //string buid = HttpContext.Session.GetString("buid");
            //var trainee = _context.People.Where(a => a.Buid == buid).FirstOrDefault();
            //ViewBag.buid = trainee.Buid;
            //ViewBag.personid = trainee.Id;
            //ViewBag.firstname = trainee.Firstname;
           // var t1 = _context.AdminReportsManuscripts.ToListAsync();
            //ViewBag.lastname = trainee.Lastname;
            //var projname = _context.TrainingGrants.Where(a => a.Id == proj1).FirstOrDefault();
            //ViewBag.projname = projname.Title;
            return View(await _context.AdminReportsManuscripts.ToListAsync());
        }
        public async Task<IActionResult> Patents()
        {
            //int proj1 = Convert.ToInt32(HttpContext.Session.GetString("project"));
            //string buid = HttpContext.Session.GetString("buid");
            //var trainee = _context.People.Where(a => a.Buid == buid).FirstOrDefault();
            //ViewBag.buid = trainee.Buid;
            //ViewBag.personid = trainee.Id;
            //ViewBag.firstname = trainee.Firstname;
            // var t1 = _context.AdminReportsManuscripts.ToListAsync();
            //ViewBag.lastname = trainee.Lastname;
            //var projname = _context.TrainingGrants.Where(a => a.Id == proj1).FirstOrDefault();
            //ViewBag.projname = projname.Title;
            return View(await _context.AdminReportsPatents.ToListAsync());
        }
        public async Task<IActionResult> Videos()
        {
            //int proj1 = Convert.ToInt32(HttpContext.Session.GetString("project"));
            //string buid = HttpContext.Session.GetString("buid");
            //var trainee = _context.People.Where(a => a.Buid == buid).FirstOrDefault();
            //ViewBag.buid = trainee.Buid;
            //ViewBag.personid = trainee.Id;
            //ViewBag.firstname = trainee.Firstname;
            //var t1 = _context.AdminReportsManuscripts.ToListAsync();
            //ViewBag.lastname = trainee.Lastname;
            //var projname = _context.TrainingGrants.Where(a => a.Id == proj1).FirstOrDefault();
            //ViewBag.projname = projname.Title;
            return View(await _context.AdminReportsVideos.ToListAsync());

        }

    }
}
