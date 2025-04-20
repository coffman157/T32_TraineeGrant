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
    public class ManuscriptsController : Controller
    {
        private readonly BumcOrgContext _context;

        public ManuscriptsController(BumcOrgContext context)
        {
            _context = context;
        }

        // GET: Manuscripts
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
            return View(await _context.TrainingRecordManuscripts.ToListAsync());
        }

        // GET: Manuscripts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingRecordManuscript = await _context.TrainingRecordManuscripts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingRecordManuscript == null)
            {
                return NotFound();
            }

            return View(trainingRecordManuscript);
        }

        // GET: Manuscripts/Create
        public IActionResult Create()
        {
            ViewBag.project = HttpContext.Session.GetString("project");
            int proj1 = Convert.ToInt32(HttpContext.Session.GetString("project"));
            string buid = HttpContext.Session.GetString("buid");
            var trainee = _context.People.Where(a => a.Buid == buid).FirstOrDefault();
            ViewBag.buid = trainee.Buid;
            ViewBag.personid = trainee.Id;
            ViewBag.firstname = trainee.Firstname;

            ViewBag.lastname = trainee.Lastname;
            var projname = _context.TrainingGrants.Where(a => a.Id == proj1).FirstOrDefault();
            ViewBag.projname = projname.Title;
            return View();
        }

        // POST: Manuscripts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Buid,Trainingrecordid,Authors,Title,Journal,Year,Volume,Pages,DoiPmidPmcid,Status,Statusother")] TrainingRecordManuscript trainingRecordManuscript)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainingRecordManuscript);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainingRecordManuscript);
        }

        // GET: Manuscripts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingRecordManuscript = await _context.TrainingRecordManuscripts.FindAsync(id);
            if (trainingRecordManuscript == null)
            {
                return NotFound();
            }
            return View(trainingRecordManuscript);
        }

        // POST: Manuscripts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Buid,Trainingrecordid,Authors,Title,Journal,Year,Volume,Pages,DoiPmidPmcid,Status,Statusother")] TrainingRecordManuscript trainingRecordManuscript)
        {
            if (id != trainingRecordManuscript.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingRecordManuscript);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingRecordManuscriptExists(trainingRecordManuscript.Id))
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
            return View(trainingRecordManuscript);
        }

        // GET: Manuscripts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingRecordManuscript = await _context.TrainingRecordManuscripts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingRecordManuscript == null)
            {
                return NotFound();
            }

            return View(trainingRecordManuscript);
        }

        // POST: Manuscripts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainingRecordManuscript = await _context.TrainingRecordManuscripts.FindAsync(id);
            if (trainingRecordManuscript != null)
            {
                _context.TrainingRecordManuscripts.Remove(trainingRecordManuscript);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingRecordManuscriptExists(int id)
        {
            return _context.TrainingRecordManuscripts.Any(e => e.Id == id);
        }
    }
}
