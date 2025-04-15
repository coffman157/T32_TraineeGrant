using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T32_TraineeGrant.Models;
using T32_TraineeGrant;
namespace T32_TraineeGrant.Controllers
{
    public class HomeController : Controller
    {
        private readonly BumcOrgContext _context;

        public HomeController(BumcOrgContext context)
        {
            _context = context;
        }
        

        

        public IActionResult Index()
        {
              
                     
           if (T32_TraineeGrant.Extensions.HttpRequestExtensions.IsLocal(HttpContext.Request))
            {

                //HttpContext.Session.SetString("buid", "U55555555");
                //HttpContext.Session.SetString("firstname", "Jim");
                //HttpContext.Session.SetString("lastname", "Vlachos");
                //HttpContext.Session.SetString("status", "students");
                //HttpContext.Session.SetString("email", "jvlachos@bu.edu");
                HttpContext.Session.SetString("buid", "U89087945");
                HttpContext.Session.SetString("firstname", "Peter");
                HttpContext.Session.SetString("lastname", "Flynn");
                HttpContext.Session.SetString("status", "students");
                HttpContext.Session.SetString("email", "pflynn@bu.edu");
                //ViewBag.buid = HttpContext.Session.GetString("buid");
                //ViewBag.firstname = HttpContext.Session.GetString("firstname");
                //ViewBag.lastname = HttpContext.Session.GetString("lastname");
                //ViewBag.status = HttpContext.Session.GetString("status");

            }
            return View();
            //else
            //{
            //    return BadRequest();
            //}

            //if (HttpContext.Session.GetString("status") == "students")
            //{
            //    return RedirectToAction("Index", "People");
            //}
            //else
            //{
            //    return BadRequest();
            //}
            // Response.Redirect("/People/Index");
        }
        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexConfirmed(int id)
        {
            string buid = HttpContext.Session.GetString("buid");
            var register = _context.People.Where(a => a.Buid == buid).FirstOrDefault();
            if (register == null)
            {
                return RedirectToAction("Index", "People");
            }
            else
            {
                return RedirectToAction("Main", "Home");
            }
        }
        public IActionResult Main()
        {return View(); 
        }
        private bool IsLocal()
        {
            throw new NotImplementedException();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSession();
        }
    }
}

// Move the extension method to a static class to fix CS1106
namespace T32_TraineeGrant.Extensions
{
    public static class HttpRequestExtensions
    {
        public static bool IsLocal(this HttpRequest request)
        {
            var connection = request.HttpContext.Connection;
            if (connection.RemoteIpAddress is null)
            {
                return true; // Local request
            }

            if (connection.LocalIpAddress is not null)
            {
                return connection.RemoteIpAddress.Equals(connection.LocalIpAddress);
            }

            return System.Net.IPAddress.IsLoopback(connection.RemoteIpAddress);
        }
    }
}
