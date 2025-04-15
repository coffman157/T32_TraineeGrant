using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using T32_TraineeGrant;

namespace T32_TraineeGrant.Controllers
{
    public class PeopleController : Controller
    {
        private readonly BumcOrgContext _context;

        public PeopleController(BumcOrgContext context)
        {
            _context = context;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            //HttpContext.Session.SetString("buid", "U32220128");
            //HttpContext.Session.SetString("firstname", "Jerry");
            //HttpContext.Session.SetString("lastname", "Coffman");
            //HttpContext.Session.SetString("email", "coffman@bu.edu");
            //HttpContext.Session.SetString("status", "student");


            string buid = HttpContext.Session.GetString("buid");
            var person = _context.People.Where(a => a.Buid == buid).FirstOrDefault();
            if (person != null)
            { return RedirectToAction("Edit", "people"); }
            else
            { return RedirectToAction("Create", "people"); }
            return View();

        }
        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexNext()
        {
            HttpContext.Session.SetString("buid", "U32220128");
            HttpContext.Session.SetString("firstname", "Jerry");
            HttpContext.Session.SetString("lastname", "Coffman");
            HttpContext.Session.SetString("email", "coffman@bu.edu");
            HttpContext.Session.SetString("status", "student");

            string buid = HttpContext.Session.GetString("buid");
            var person = _context.People.Where(a => a.Buid == buid).FirstOrDefault();
            if (person != null)
            {
              //  return await Task.FromResult(RedirectToAction("Edit", "people"));
            }
            else
            {
               //return await Task.FromResult(RedirectToAction("Create", "people"));
            }
            return View();
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            string buid = HttpContext.Session.GetString("Buid");
            var person = _context.People.Where(a => a.Buid == buid).FirstOrDefault();
           
            ViewBag.firstname= HttpContext.Session.GetString("firstname");
            ViewBag.lastname = HttpContext.Session.GetString("lastname");
            ViewBag.email = HttpContext.Session.GetString("email");
            ViewBag.buid = HttpContext.Session.GetString("buid");

            // return View(person);
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Buid,Lastname,Firstname,Email,Orcidid,Citizenship,Position,Positionother,Acceptedpredoc,Leaveofabsence,Minority,Disability,Woman,Facultymentor,Yearsposition")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                Response.Redirect("/Home/Main");
            }
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit()
        {
            ViewBag.buid = HttpContext.Session.GetString("buid");
            ViewBag.firstname = HttpContext.Session.GetString("firstname");
            ViewBag.lastname = HttpContext.Session.GetString("lastname");
            ViewBag.status = HttpContext.Session.GetString("status");
            string buid = HttpContext.Session.GetString("buid");
            var person =  _context.People.Where(a => a.Buid == buid).FirstOrDefault();
            if (person == null)
            {
                return NotFound();
            }
          
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Buid,Lastname,Firstname,Email,Orcidid,Citizenship,Position,Positionother,Acceptedpredoc,Leaveofabsence,Minority,Disability,Woman,Facultymentor,Yearsposition")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }
            string s1=person.Citizenship;
           
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.People.FindAsync(id);
            if (person != null)
            {
                _context.People.Remove(person);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.Id == id);
        }
    }
}
