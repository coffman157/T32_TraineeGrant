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
    public class AbstractsController : Controller
    {
        private readonly BumcOrgContext _context;

        public AbstractsController(BumcOrgContext context)
        {
            _context = context;
        }

        // GET: Abstracts
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
            return View(await _context.TrainingRecordAbstracts.ToListAsync());
        }

        // GET: Abstracts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingRecordAbstract = await _context.TrainingRecordAbstracts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingRecordAbstract == null)
            {
                return NotFound();
            }

            return View(trainingRecordAbstract);
        }

        // GET: Abstracts/Create
        public IActionResult Create()
        {
            ViewBag.project= HttpContext.Session.GetString("project");
            int proj1 = Convert.ToInt32(HttpContext.Session.GetString("project"));
            string buid = HttpContext.Session.GetString("buid");
            var trainee = _context.People.Where(a => a.Buid == buid).FirstOrDefault();
            ViewBag.buid = trainee.Buid;
            ViewBag.personid = trainee.Id;
            ViewBag.firstname = trainee.Firstname;

            ViewBag.lastname = trainee.Lastname;
            var projname = _context.TrainingGrants.Where(a => a.Id == proj1).FirstOrDefault();
            ViewBag.projname = projname.Title;
          //  var grant=_context.TrainingGrants.Where(a=>a.Id==IDataTokensMetadata)
            return View();
            
        }

        // POST: Abstracts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Trainingrecordid,Buid,Type,Authors,Title,Conference,City,Date,PosterOral,Presenter")] TrainingRecordAbstract trainingRecordAbstract)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainingRecordAbstract);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainingRecordAbstract);
        }

        // GET: Abstracts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingRecordAbstract = await _context.TrainingRecordAbstracts.FindAsync(id);
            if (trainingRecordAbstract == null)
            {
                return NotFound();
            }
            int proj1 = Convert.ToInt32(HttpContext.Session.GetString("project"));
            string buid = HttpContext.Session.GetString("buid");
            var trainee = _context.People.Where(a => a.Buid == buid).FirstOrDefault();
            ViewBag.buid = trainee.Buid;
            ViewBag.personid = trainee.Id;
            ViewBag.firstname = trainee.Firstname;

            ViewBag.lastname = trainee.Lastname;
            var projname = _context.TrainingGrants.Where(a => a.Id == proj1).FirstOrDefault();
            ViewBag.projname = projname.Title;
            return View(trainingRecordAbstract);
        }

        // POST: Abstracts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Trainingrecordid,Buid,Type,Authors,Title,Conference,City,Date,PosterOral,Presenter")] TrainingRecordAbstract trainingRecordAbstract)
        {
            if (id != trainingRecordAbstract.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingRecordAbstract);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingRecordAbstractExists(trainingRecordAbstract.Id))
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
            return View(trainingRecordAbstract);
        }

        // GET: Abstracts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingRecordAbstract = await _context.TrainingRecordAbstracts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingRecordAbstract == null)
            {
                return NotFound();
            }

            return View(trainingRecordAbstract);
        }

        // POST: Abstracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainingRecordAbstract = await _context.TrainingRecordAbstracts.FindAsync(id);
            if (trainingRecordAbstract != null)
            {
                _context.TrainingRecordAbstracts.Remove(trainingRecordAbstract);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingRecordAbstractExists(int id)
        {
            return _context.TrainingRecordAbstracts.Any(e => e.Id == id);
        }
    }
}
