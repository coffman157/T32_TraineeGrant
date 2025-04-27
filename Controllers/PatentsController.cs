using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using T32_TraineeGrant;

namespace T32_TraineeGrant.Controllers
{
    public class PatentsController : Controller
    {
        private readonly BumcOrgContext _context;

        public PatentsController(BumcOrgContext context)
        {
            _context = context;
        }

        // GET: Patents
        public async Task<IActionResult> Index()
        {
            int proj1 = Convert.ToInt32(HttpContext.Session.GetString("project"));
            string buid = HttpContext.Session.GetString("buid");
            var trainee = _context.People.Where(a => a.Buid == buid).FirstOrDefault();
            ViewBag.buid = trainee.Buid;
            ViewBag.personid = trainee.Id;
            ViewBag.firstname = trainee.Firstname;

            ViewBag.lastname = trainee.Lastname;
            var projname = _context.TrainingGrants.Where(a => a.Id == proj1).FirstOrDefault();
            ViewBag.projname = projname.Title;
            return View(await _context.TrainingRecordPatents.ToListAsync());
        }

        // GET: Patents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingRecordPatent = await _context.TrainingRecordPatents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingRecordPatent == null)
            {
                return NotFound();
            }

            return View(trainingRecordPatent);
        }

        // GET: Patents/Create
        public IActionResult Create()
        {
            string buid = HttpContext.Session.GetString("buid");
            var trainee = _context.People.Where(a => a.Buid == buid).FirstOrDefault();
            ViewBag.buid = trainee.Buid;
            ViewBag.personid = trainee.Id;
            ViewBag.firstname = trainee.Firstname;

            ViewBag.lastname = trainee.Lastname;
            ViewBag.project = HttpContext.Session.GetString("project");
            int proj1 = Convert.ToInt32(HttpContext.Session.GetString("project"));


            var projname = _context.TrainingGrants.Where(a => a.Id == proj1).FirstOrDefault();
            ViewBag.projname = projname.Title;
            return View();
        }

        // POST: Patents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Personid,Trainingrecordid,Buid,Inventors,Title,Dateissued")] TrainingRecordPatent trainingRecordPatent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainingRecordPatent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainingRecordPatent);
        }

        // GET: Patents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            string buid = HttpContext.Session.GetString("buid");
            var trainee = _context.People.Where(a => a.Buid == buid).FirstOrDefault();
            ViewBag.buid = trainee.Buid;
            ViewBag.personid = trainee.Id;
            ViewBag.firstname = trainee.Firstname;

            ViewBag.lastname = trainee.Lastname;
            ViewBag.project = HttpContext.Session.GetString("project");
            int proj1 = Convert.ToInt32(HttpContext.Session.GetString("project"));


            var projname = _context.TrainingGrants.Where(a => a.Id == proj1).FirstOrDefault();
            ViewBag.projname = projname.Title;
            if (id == null)
            {
                return NotFound();
            }

            var trainingRecordPatent = await _context.TrainingRecordPatents.FindAsync(id);
            if (trainingRecordPatent == null)
            {
                return NotFound();
            }
            return View(trainingRecordPatent);
        }

        // POST: Patents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Personid,Trainingrecordid,Buid,Inventors,Title,Dateissued")] TrainingRecordPatent trainingRecordPatent)
        {
            if (id != trainingRecordPatent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingRecordPatent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingRecordPatentExists(trainingRecordPatent.Id))
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
            return View(trainingRecordPatent);
        }

        // GET: Patents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingRecordPatent = await _context.TrainingRecordPatents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingRecordPatent == null)
            {
                return NotFound();
            }

            return View(trainingRecordPatent);
        }

        // POST: Patents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainingRecordPatent = await _context.TrainingRecordPatents.FindAsync(id);
            if (trainingRecordPatent != null)
            {
                _context.TrainingRecordPatents.Remove(trainingRecordPatent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingRecordPatentExists(int id)
        {
            return _context.TrainingRecordPatents.Any(e => e.Id == id);
        }
    }
}
