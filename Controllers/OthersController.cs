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
    public class OthersController : Controller
    {
        private readonly BumcOrgContext _context;

        public OthersController(BumcOrgContext context)
        {
            _context = context;
        }

        // GET: Others
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
            return View(await _context.TrainingRecordOthers.ToListAsync());
        }

        // GET: Others/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingRecordOther = await _context.TrainingRecordOthers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingRecordOther == null)
            {
                return NotFound();
            }

            return View(trainingRecordOther);
        }

        // GET: Others/Create
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

        // POST: Others/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Trainingrecordid,Personid,Buid,Date,Description")] TrainingRecordOther trainingRecordOther)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainingRecordOther);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainingRecordOther);
        }

        // GET: Others/Edit/5
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

            var trainingRecordOther = await _context.TrainingRecordOthers.FindAsync(id);
            if (trainingRecordOther == null)
            {
                return NotFound();
            }
            return View(trainingRecordOther);
        }

        // POST: Others/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Trainingrecordid,Personid,Buid,Date,Description")] TrainingRecordOther trainingRecordOther)
        {
            if (id != trainingRecordOther.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingRecordOther);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingRecordOtherExists(trainingRecordOther.Id))
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
            return View(trainingRecordOther);
        }

        // GET: Others/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingRecordOther = await _context.TrainingRecordOthers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingRecordOther == null)
            {
                return NotFound();
            }

            return View(trainingRecordOther);
        }

        // POST: Others/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainingRecordOther = await _context.TrainingRecordOthers.FindAsync(id);
            if (trainingRecordOther != null)
            {
                _context.TrainingRecordOthers.Remove(trainingRecordOther);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingRecordOtherExists(int id)
        {
            return _context.TrainingRecordOthers.Any(e => e.Id == id);
        }
    }
}
